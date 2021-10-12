using IDNT.AppBasics.Virtualization.Libvirt;
using IDNT.AppBasics.Virtualization.Libvirt.Events;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Text.Json.Serialization;
using VirtServer.Common;

var connection = LibvirtConnection.Connect("qemu:///system");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddSingleton(typeof(LibvirtConnection), connection);
// Some of the objects have circular references. Ignore it.
var jsonSerializerOptions = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles };

var app = builder.Build();
app.MapHub<LibvirtConnectionHub>("/libvirt");
app.MapGet("/", () => "VirtServer");

connection.DomainEventReceived += Connection_DomainEventReceived;
connection.StoragePoolLifecycleEventReceived += Connection_StoragePoolLifecycleEventReceived;
connection.StoragePoolRefreshEventReceived += Connection_StoragePoolRefreshEventReceived;

app.MapGet("/domains", async context => {
    await context.Response.WriteAsJsonAsync(connection.Domains, jsonSerializerOptions);
});

app.MapGet("/connection", async context => {
    await context.Response.WriteAsJsonAsync(connection, jsonSerializerOptions);
});


app.MapGet("/domain", async context =>
{
    if (context.Request.Query.ContainsKey("uniqueid"))
    {
        var id = context.Request.Query["uniqueid"];
        await context.Response.WriteAsJsonAsync(connection.GetDomainByUniqueId(new Guid(id)), jsonSerializerOptions);
    }
});

app.MapGet("/storagepools", async context => {
    await context.Response.WriteAsJsonAsync(connection.StoragePools, jsonSerializerOptions);
});

app.MapGet("/storagepool", async context => {
    if (context.Request.Query.ContainsKey("uniqueid"))
    {
        var id = context.Request.Query["uniqueid"];
        await context.Response.WriteAsJsonAsync(connection.GetStoragePoolByUniqueId(new Guid(id)), jsonSerializerOptions);
    }
});

app.MapGet("/storagevolumes", async context => {
    await context.Response.WriteAsJsonAsync(connection.StorageVolumes, jsonSerializerOptions);
});


app.Run();

void Connection_StoragePoolRefreshEventReceived(object? sender, VirStoragePoolRefreshEventArgs e)
{
    var json = JsonSerializer.Serialize(new StoragePoolRefreshEventCommand() { StoragePool = sender as LibvirtStoragePool, EventArgs = e }, jsonSerializerOptions);
    app.Services.GetService<IHubContext<LibvirtConnectionHub>>().Clients.All.SendAsync("StoragePoolRefreshEventReceived", json);
}

void Connection_DomainEventReceived(object sender, VirDomainEventArgs e)
{
    var json = JsonSerializer.Serialize(new DomainEventCommand() { Domain = sender as LibvirtDomain, EventArgs = e }, jsonSerializerOptions);
    app.Services.GetService<IHubContext<LibvirtConnectionHub>>().Clients.All.SendAsync("DomainEventReceived", json);
}

void Connection_StoragePoolLifecycleEventReceived(object? sender, VirStoragePoolLifecycleEventArgs e)
{
    var json = JsonSerializer.Serialize(new StoragePoolLifecycleEventCommand() { StoragePool = sender as LibvirtStoragePool, EventArgs = e }, jsonSerializerOptions);
    app.Services.GetService<IHubContext<LibvirtConnectionHub>>().Clients.All.SendAsync("StoragePoolLifecycleEventReceived", json);
}


public class LibvirtConnectionHub : Hub
{
}