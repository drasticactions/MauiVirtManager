using IDNT.AppBasics.Virtualization.Libvirt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace VirtServer.Common
{

    public class DomainStateUpdate
    {
        public Guid DomainId { get; set; }

        public DomainState State { get; set; }
    }

    public enum DomainState
    {
        Empty,
        Shutdown,
        Suspend,
        Reset,
        Resume
    }

    public partial class DomainEventCommandProxy
    {
        [JsonPropertyName("Domain")]
        public Domain Domain { get; set; }

        [JsonPropertyName("EventArgs")]
        public ConnectionEventArgs EventArgs { get; set; }
    }

    public partial class StoragePoolEventCommandProxy
    {
        [JsonPropertyName("StoragePool")]
        public StoragePoolElement StoragePool { get; set; }

        [JsonPropertyName("EventArgs")]
        public ConnectionEventArgs EventArgs { get; set; }
    }

    public partial class ConnectionEventArgs
    {
        [JsonPropertyName("UniqueId")]
        public Guid UniqueId { get; set; }

        [JsonPropertyName("EventType")]
        public long EventType { get; set; }

        [JsonPropertyName("Detail")]
        public long Detail { get; set; }
    }

    public partial class Connection
    {
        [JsonPropertyName("IsAlive")]
        public bool IsAlive { get; set; }

        [JsonPropertyName("Configuration")]
        public Configuration Configuration { get; set; }

        [JsonPropertyName("Node")]
        public Node Node { get; set; }

        [JsonPropertyName("Domains")]
        public Domain[]? Domains { get; set; }

        [JsonPropertyName("StoragePools")]
        public StoragePoolElement[]? StoragePools { get; set; }

        [JsonPropertyName("StorageVolumes")]
        public Volume[]? StorageVolumes { get; set; }
    }

    public partial class Configuration
    {
        [JsonPropertyName("QemuDomainRunPath")]
        public string QemuDomainRunPath { get; set; }

        [JsonPropertyName("QemuDomainLogPath")]
        public string QemuDomainLogPath { get; set; }

        [JsonPropertyName("QemuDomainEtcPath")]
        public string QemuDomainEtcPath { get; set; }

        [JsonPropertyName("EventsEnabled")]
        public bool EventsEnabled { get; set; }

        [JsonPropertyName("MetricsEnabled")]
        public bool MetricsEnabled { get; set; }

        [JsonPropertyName("KeepaliveInterval")]
        public double KeepaliveInterval { get; set; }

        [JsonPropertyName("KeepaliveCount")]
        public double KeepaliveCount { get; set; }

        [JsonPropertyName("Credentials")]
        public object Credentials { get; set; }

        [JsonPropertyName("MetricsIntervalSeconds")]
        public double MetricsIntervalSeconds { get; set; }
    }

    public partial class Domain
    {
        [JsonPropertyName("UniqueId")]
        public Guid UniqueId { get; set; }

        [JsonPropertyName("IsActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("Id")]
        public double Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("OSType")]
        public string OsType { get; set; }

        [JsonPropertyName("CpuTimeUsed")]
        [JsonIgnore]
        public DateTimeOffset CpuTimeUsed { get; set; }

        [JsonPropertyName("MemoryUsedKbyte")]
        public double MemoryUsedKbyte { get; set; }

        [JsonPropertyName("MemoryMaxKbyte")]
        public double MemoryMaxKbyte { get; set; }

        [JsonPropertyName("CpuCount")]
        public double CpuCount { get; set; }

        [JsonPropertyName("Connection")]
        public object Connection { get; set; }

        [JsonPropertyName("State")]
        public double State { get; set; }

        [JsonPropertyName("DriverType")]
        public string DriverType { get; set; }

        [JsonPropertyName("ModifiedAt")]
        public DateTimeOffset ModifiedAt { get; set; }

        [JsonPropertyName("UptimeSeconds")]
        public double UptimeSeconds { get; set; }

        [JsonPropertyName("GraphicsDevices")]
        public GraphicsDevice[]? GraphicsDevices { get; set; }

        [JsonPropertyName("NetworkInterfaces")]
        public NetworkInterface[]? NetworkInterfaces { get; set; }

        [JsonPropertyName("DiskDevices")]
        public DiskDevice[]? DiskDevices { get; set; }

        [JsonPropertyName("MachineType")]
        public string MachineType { get; set; }

        [JsonPropertyName("MachineArch")]
        public string MachineArch { get; set; }

        [JsonPropertyName("OsInfoId")]
        public object OsInfoId { get; set; }

        [JsonPropertyName("CpuUtilization")]
        public CpuUtilization CpuUtilization { get; set; }
    }

    public partial class CpuUtilization
    {
        [JsonPropertyName("LastSecond")]
        public double LastSecond { get; set; }

        [JsonPropertyName("LastMinute")]
        public double LastMinute { get; set; }

        [JsonPropertyName("PerSecondValues")]
        public double[]? PerSecondValues { get; set; }

        [JsonPropertyName("PerMinuteValues")]
        public double[]? PerMinuteValues { get; set; }
    }

    public partial class DiskDevice
    {
        [JsonPropertyName("Volume")]
        public object Volume { get; set; }

        [JsonPropertyName("Type")]
        public double Type { get; set; }

        [JsonPropertyName("Device")]
        public double Device { get; set; }

        [JsonPropertyName("Driver")]
        public Driver Driver { get; set; }

        [JsonPropertyName("Source")]
        public DiskDeviceSource Source { get; set; }

        [JsonPropertyName("Target")]
        public Target Target { get; set; }

        [JsonPropertyName("Address")]
        public Address Address { get; set; }

        [JsonPropertyName("IsReadOnly")]
        public bool IsReadOnly { get; set; }
    }

    public partial class Address
    {
        [JsonPropertyName("Type")]
        public double Type { get; set; }

        [JsonPropertyName("Domain")]
        public string Domain { get; set; }

        [JsonPropertyName("Bus")]
        public string Bus { get; set; }

        [JsonPropertyName("Slot")]
        public string Slot { get; set; }

        [JsonPropertyName("Function")]
        public string Function { get; set; }

        [JsonPropertyName("Controller")]
        [JsonIgnore]
        public double? Controller { get; set; }

        [JsonPropertyName("Target")]
        [JsonIgnore]
        public double? Target { get; set; }

        [JsonPropertyName("Unit")]
        [JsonIgnore]
        public double? Unit { get; set; }
    }

    public partial class Driver
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }
    }

    public partial class DiskDeviceSource
    {
        [JsonPropertyName("Name")]
        public object Name { get; set; }

        [JsonPropertyName("Protocol")]
        public object Protocol { get; set; }

        [JsonPropertyName("File")]
        public string File { get; set; }

        [JsonPropertyName("Host")]
        public object Host { get; set; }
    }

    public partial class Target
    {
        [JsonPropertyName("Device")]
        public string Device { get; set; }

        [JsonPropertyName("Bus")]
        public double Bus { get; set; }
    }

    public partial class GraphicsDevice
    {
        [JsonPropertyName("Type")]
        public double Type { get; set; }

        [JsonPropertyName("Listen")]
        public object Listen { get; set; }

        [JsonPropertyName("Port")]
        public double Port { get; set; }

        [JsonPropertyName("IsAutoPort")]
        public bool IsAutoPort { get; set; }
    }

    public partial class NetworkInterface
    {
        [JsonPropertyName("Type")]
        public double Type { get; set; }

        [JsonPropertyName("MAC")]
        public Mac Mac { get; set; }

        [JsonPropertyName("Source")]
        public NetworkInterfaceSource Source { get; set; }

        [JsonPropertyName("Target")]
        public object Target { get; set; }

        [JsonPropertyName("Model")]
        public Model Model { get; set; }

        [JsonPropertyName("Address")]
        public Address Address { get; set; }
    }

    public partial class Mac
    {
        [JsonPropertyName("Address")]
        public string Address { get; set; }
    }

    public partial class Model
    {
        [JsonPropertyName("Type")]
        public string Type { get; set; }
    }

    public partial class NetworkInterfaceSource
    {
        [JsonPropertyName("Network")]
        public string Network { get; set; }

        [JsonPropertyName("Bridge")]
        public object Bridge { get; set; }
    }

    public partial class Node
    {
        [JsonPropertyName("Hostname")]
        public string Hostname { get; set; }

        [JsonPropertyName("MemFreeBytes")]
        public double MemFreeBytes { get; set; }

        [JsonPropertyName("CpuModelName")]
        public string CpuModelName { get; set; }

        [JsonPropertyName("CpuFrequencyMhz")]
        public double CpuFrequencyMhz { get; set; }

        [JsonPropertyName("CpuNumaNodes")]
        public double CpuNumaNodes { get; set; }

        [JsonPropertyName("CpuSocketsPerNode")]
        public double CpuSocketsPerNode { get; set; }

        [JsonPropertyName("CpuCoresPerSocket")]
        public double CpuCoresPerSocket { get; set; }

        [JsonPropertyName("CpuThreadsPerCore")]
        public double CpuThreadsPerCore { get; set; }

        [JsonPropertyName("MemoryKBytes")]
        public double MemoryKBytes { get; set; }
    }

    public partial class StoragePoolElement
    {
        [JsonPropertyName("UniqueId")]
        public Guid UniqueId { get; set; }

        [JsonPropertyName("IsActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("State")]
        public double State { get; set; }

        [JsonPropertyName("CapacityInByte")]
        public double CapacityInByte { get; set; }

        [JsonPropertyName("ByteAvailable")]
        public double ByteAvailable { get; set; }

        [JsonPropertyName("ByteAllocated")]
        public double ByteAllocated { get; set; }

        [JsonPropertyName("DriverType")]
        public string DriverType { get; set; }

        [JsonPropertyName("Volumes")]
        public Volume[]? Volumes { get; set; }
    }

    public partial class Volume
    {
        [JsonPropertyName("Key")]
        public string Key { get; set; }

        [JsonPropertyName("Path")]
        public string Path { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("VolumeType")]
        public double VolumeType { get; set; }

        [JsonPropertyName("CapacityInByte")]
        public double CapacityInByte { get; set; }

        [JsonPropertyName("ByteAllocated")]
        public double ByteAllocated { get; set; }

        [JsonPropertyName("StoragePool")]
        public StorageVolumeStoragePool StoragePool { get; set; }

        [JsonPropertyName("Format")]
        public string Format { get; set; }

        [JsonPropertyName("CreatedAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("ModifiedAt")]
        public DateTimeOffset ModifiedAt { get; set; }
    }

    public partial class StorageVolumeStoragePool
    {
        [JsonPropertyName("UniqueId")]
        public Guid UniqueId { get; set; }

        [JsonPropertyName("IsActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("State")]
        public double State { get; set; }

        [JsonPropertyName("CapacityInByte")]
        public double CapacityInByte { get; set; }

        [JsonPropertyName("ByteAvailable")]
        public double ByteAvailable { get; set; }

        [JsonPropertyName("ByteAllocated")]
        public double ByteAllocated { get; set; }

        [JsonPropertyName("DriverType")]
        public string DriverType { get; set; }

        [JsonPropertyName("Volumes")]
        public object[]? Volumes { get; set; }
    }
}
