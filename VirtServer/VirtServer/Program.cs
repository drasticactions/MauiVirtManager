using IDNT.AppBasics.Virtualization.Libvirt;
using IDNT.AppBasics.Virtualization.Libvirt.Events;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Timers;
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

app.MapPost("/domain", async context => {
    try
    {
        var domainState = await context.Request.ReadFromJsonAsync<DomainStateUpdate>();
        var domain = connection.GetDomainByUniqueId(domainState.DomainId);
        switch (domainState.State)
        {
            case DomainState.Shutdown:
                domain.Shutdown();
                break;
            case DomainState.Suspend:
                domain.Suspend();
                break;
            case DomainState.Reset:
                domain.Reset();
                break;
            case DomainState.Resume:
                if (domain.State == VirDomainState.VIR_DOMAIN_PAUSED)
                    domain.Resume();
                else
                    domain.Create();
                break;
        }
        await context.Response.WriteAsJsonAsync(domain, jsonSerializerOptions);
    }
    catch (global::System.Exception ex)
    {
        // TODO: Capture and show to user.
        Console.WriteLine(ex);
        throw;
    }
});

app.MapGet("/domainimage", async context => {
    // TODO: Cheap hack to verify this works
    var id = context.Request.Query["uniqueid"];
    var domain = connection.GetDomainByUniqueId(new Guid(id));
    using var memoryStream = new MemoryStream();
    domain.GetScreenshot(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
    context.Response.ContentType = "image/jpeg";
    await context.Response.Body.WriteAsync(memoryStream.ToArray());
});

var connectionTimer = new System.Timers.Timer(1000);
connectionTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
// Timer to post connection events that don't have an event handle.
// Like CPU usage. 
connectionTimer.Start();

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

void OnTimedEvent(object? sender, ElapsedEventArgs e)
{
    if (connection == null || !connection.IsAlive)
    {
        return;
    }

    foreach (var domain in connection.Domains.Where(n => n.IsActive))
    {
        var json = JsonSerializer.Serialize(new DomainEventCommand() { Domain = domain }, jsonSerializerOptions);
        app.Services.GetService<IHubContext<LibvirtConnectionHub>>().Clients.All.SendAsync("DomainEventReceived", json);
    }
}


public class LibvirtConnectionHub : Hub
{
}