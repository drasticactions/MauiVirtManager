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


public class LibvirtConnectionHub : Hub
{
    public LibvirtConnectionHub(LibvirtConnection connection)
    {
        connection.DomainEventReceived += Connection_DomainEventReceived;
        connection.StoragePoolLifecycleEventReceived += Connection_StoragePoolLifecycleEventReceived;
        connection.StoragePoolRefreshEventReceived += Connection_StoragePoolRefreshEventReceived;
    }

    private void Connection_StoragePoolRefreshEventReceived(object? sender, VirStoragePoolRefreshEventArgs e)
    {
        this.Clients.All.SendAsync("StoragePoolRefreshEventReceived", new StoragePoolRefreshEventCommand() { StoragePool = sender as LibvirtStoragePool, EventArgs = e });
    }

    private void Connection_DomainEventReceived(object sender, VirDomainEventArgs e)
    {
        this.Clients.All.SendAsync("DomainEventReceived", new DomainEventCommand() { Domain = sender as LibvirtDomain, EventArgs = e });
    }

    private void Connection_StoragePoolLifecycleEventReceived(object? sender, VirStoragePoolLifecycleEventArgs e)
    {
        this.Clients.All.SendAsync("StoragePoolLifecycleEventReceived", new StoragePoolLifecycleEventCommand() { StoragePool = sender as LibvirtStoragePool, EventArgs = e });
    }
}