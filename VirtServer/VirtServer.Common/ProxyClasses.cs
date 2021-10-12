using IDNT.AppBasics.Virtualization.Libvirt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace VirtServer.Common
{
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
        public long KeepaliveInterval { get; set; }

        [JsonPropertyName("KeepaliveCount")]
        public long KeepaliveCount { get; set; }

        [JsonPropertyName("Credentials")]
        public object Credentials { get; set; }

        [JsonPropertyName("MetricsIntervalSeconds")]
        public long MetricsIntervalSeconds { get; set; }
    }

    public partial class Domain
    {
        [JsonPropertyName("UniqueId")]
        public Guid UniqueId { get; set; }

        [JsonPropertyName("IsActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("OSType")]
        public string OsType { get; set; }

        [JsonPropertyName("CpuTimeUsed")]
        [JsonIgnore]
        public DateTimeOffset CpuTimeUsed { get; set; }

        [JsonPropertyName("MemoryUsedKbyte")]
        public long MemoryUsedKbyte { get; set; }

        [JsonPropertyName("MemoryMaxKbyte")]
        public long MemoryMaxKbyte { get; set; }

        [JsonPropertyName("CpuCount")]
        public long CpuCount { get; set; }

        [JsonPropertyName("Connection")]
        public object Connection { get; set; }

        [JsonPropertyName("State")]
        public long State { get; set; }

        [JsonPropertyName("DriverType")]
        public string DriverType { get; set; }

        [JsonPropertyName("ModifiedAt")]
        public DateTimeOffset ModifiedAt { get; set; }

        [JsonPropertyName("UptimeSeconds")]
        public long UptimeSeconds { get; set; }

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
        public long LastSecond { get; set; }

        [JsonPropertyName("LastMinute")]
        public long LastMinute { get; set; }

        [JsonPropertyName("PerSecondValues")]
        public long[]? PerSecondValues { get; set; }

        [JsonPropertyName("PerMinuteValues")]
        public long[]? PerMinuteValues { get; set; }
    }

    public partial class DiskDevice
    {
        [JsonPropertyName("Volume")]
        public object Volume { get; set; }

        [JsonPropertyName("Type")]
        public long Type { get; set; }

        [JsonPropertyName("Device")]
        public long Device { get; set; }

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
        public long Type { get; set; }

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
        public long? Controller { get; set; }

        [JsonPropertyName("Target")]
        [JsonIgnore]
        public long? Target { get; set; }

        [JsonPropertyName("Unit")]
        [JsonIgnore]
        public long? Unit { get; set; }
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
        public long Bus { get; set; }
    }

    public partial class GraphicsDevice
    {
        [JsonPropertyName("Type")]
        public long Type { get; set; }

        [JsonPropertyName("Listen")]
        public object Listen { get; set; }

        [JsonPropertyName("Port")]
        public long Port { get; set; }

        [JsonPropertyName("IsAutoPort")]
        public bool IsAutoPort { get; set; }
    }

    public partial class NetworkInterface
    {
        [JsonPropertyName("Type")]
        public long Type { get; set; }

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
        public long MemFreeBytes { get; set; }

        [JsonPropertyName("CpuModelName")]
        public string CpuModelName { get; set; }

        [JsonPropertyName("CpuFrequencyMhz")]
        public long CpuFrequencyMhz { get; set; }

        [JsonPropertyName("CpuNumaNodes")]
        public long CpuNumaNodes { get; set; }

        [JsonPropertyName("CpuSocketsPerNode")]
        public long CpuSocketsPerNode { get; set; }

        [JsonPropertyName("CpuCoresPerSocket")]
        public long CpuCoresPerSocket { get; set; }

        [JsonPropertyName("CpuThreadsPerCore")]
        public long CpuThreadsPerCore { get; set; }

        [JsonPropertyName("MemoryKBytes")]
        public long MemoryKBytes { get; set; }
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
        public long State { get; set; }

        [JsonPropertyName("CapacityInByte")]
        public long CapacityInByte { get; set; }

        [JsonPropertyName("ByteAvailable")]
        public long ByteAvailable { get; set; }

        [JsonPropertyName("ByteAllocated")]
        public long ByteAllocated { get; set; }

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
        public long VolumeType { get; set; }

        [JsonPropertyName("CapacityInByte")]
        public long CapacityInByte { get; set; }

        [JsonPropertyName("ByteAllocated")]
        public long ByteAllocated { get; set; }

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
        public long State { get; set; }

        [JsonPropertyName("CapacityInByte")]
        public long CapacityInByte { get; set; }

        [JsonPropertyName("ByteAvailable")]
        public long ByteAvailable { get; set; }

        [JsonPropertyName("ByteAllocated")]
        public long ByteAllocated { get; set; }

        [JsonPropertyName("DriverType")]
        public string DriverType { get; set; }

        [JsonPropertyName("Volumes")]
        public object[]? Volumes { get; set; }
    }
}
