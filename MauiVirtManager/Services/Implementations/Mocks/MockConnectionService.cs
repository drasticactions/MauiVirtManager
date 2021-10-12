// <copyright file="MockConnectionService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IDNT.AppBasics.Virtualization.Libvirt;
using MauiVirtManager.Tools.Utilities;
using Microsoft.AspNetCore.SignalR.Client;
using VirtServer.Common;

namespace MauiVirtManager.Services
{
    /// <summary>
    /// Mock Connection Service. Used for Testing.
    /// </summary>
    public class MockConnectionService : IConnectionService
    {
        /// <inheritdoc/>
        public event EventHandler<ConnEventArgs> ConnectionEventHandler;

        /// <inheritdoc/>
        public HubConnectionState State => HubConnectionState.Connected;

        /// <inheritdoc/>
        public Task<Connection> GetConnectionAsync()
        {
            return Task.FromResult(JsonSerializer.Deserialize<Connection>("{\"IsAlive\":true,\"Configuration\":{\"QemuDomainRunPath\":\"/var/run/libvirt/qemu\",\"QemuDomainLogPath\":\"/var/log/libvirt/qemu\",\"QemuDomainEtcPath\":\"/etc/libvirt/qemu\",\"EventsEnabled\":true,\"MetricsEnabled\":true,\"KeepaliveInterval\":6,\"KeepaliveCount\":5,\"Credentials\":null,\"MetricsIntervalSeconds\":1},\"Node\":{\"Hostname\":\"drastic-nuc\",\"MemFreeBytes\":65410891776,\"CpuModelName\":\"x86_64\",\"CpuFrequencyMhz\":1800,\"CpuNumaNodes\":1,\"CpuSocketsPerNode\":1,\"CpuCoresPerSocket\":2,\"CpuThreadsPerCore\":2,\"MemoryKBytes\":65682676},\"Domains\":[{\"UniqueId\":\"2a0a193c-7a96-48d4-9175-376789f3157b\",\"IsActive\":false,\"Id\":-1,\"Name\":\"TestVM\",\"OSType\":\"hvm\",\"CpuTimeUsed\":\"00:00:00\",\"MemoryUsedKbyte\":1048576,\"MemoryMaxKbyte\":1048576,\"CpuCount\":1,\"Connection\":null,\"State\":5,\"DriverType\":\"kvm\",\"ModifiedAt\":\"2021-10-08T04:14:00.0372062Z\",\"UptimeSeconds\":0,\"GraphicsDevices\":[{\"Type\":1,\"Listen\":null,\"Port\":0,\"IsAutoPort\":false}],\"NetworkInterfaces\":[{\"Type\":0,\"MAC\":{\"Address\":\"52:54:00:b2:0b:ef\"},\"Source\":{\"Network\":\"default\",\"Bridge\":null},\"Target\":null,\"Model\":{\"Type\":\"e1000\"},\"Address\":{\"Type\":0,\"Domain\":\"0x0000\",\"Bus\":\"0x02\",\"Slot\":\"0x01\",\"Function\":\"0x0\",\"Controller\":null,\"Target\":null,\"Unit\":null}}],\"DiskDevices\":[],\"MachineType\":\"pc-q35-6.1\",\"MachineArch\":\"x86_64\",\"OsInfoId\":null,\"CpuUtilization\":{\"LastSecond\":0,\"LastMinute\":0,\"PerSecondValues\":[0,0,0,0],\"PerMinuteValues\":[0,0]}},{\"UniqueId\":\"2aca0dd6-cec9-4717-9ab2-0b7b13d111c3\",\"IsActive\":false,\"Id\":-1,\"Name\":\"macOS\",\"OSType\":\"hvm\",\"CpuTimeUsed\":\"00:00:00\",\"MemoryUsedKbyte\":8290304,\"MemoryMaxKbyte\":8290304,\"CpuCount\":4,\"Connection\":null,\"State\":5,\"DriverType\":\"kvm\",\"ModifiedAt\":\"2021-10-11T14:20:18.0660428Z\",\"UptimeSeconds\":0,\"GraphicsDevices\":[{\"Type\":1,\"Listen\":null,\"Port\":0,\"IsAutoPort\":false}],\"NetworkInterfaces\":[],\"DiskDevices\":[{\"Volume\":null,\"Type\":0,\"Device\":0,\"Driver\":{\"Name\":\"qemu\",\"Type\":\"qcow2\"},\"Source\":{\"Name\":null,\"Protocol\":null,\"File\":\"/opt/OSX-KVM/OpenCore-Catalina/OpenCore-NoPicker-Intel.qcow2\",\"Host\":null},\"Target\":{\"Device\":\"sda\",\"Bus\":1},\"Address\":{\"Type\":1,\"Domain\":null,\"Bus\":\"0\",\"Slot\":null,\"Function\":null,\"Controller\":\"0\",\"Target\":\"0\",\"Unit\":\"0\"},\"IsReadOnly\":false},{\"Volume\":null,\"Type\":0,\"Device\":0,\"Driver\":{\"Name\":\"qemu\",\"Type\":\"qcow2\"},\"Source\":{\"Name\":null,\"Protocol\":null,\"File\":\"/var/lib/libvirt/images/macos-bigsur-base.qcow2\",\"Host\":null},\"Target\":{\"Device\":\"sdb\",\"Bus\":1},\"Address\":{\"Type\":1,\"Domain\":null,\"Bus\":\"0\",\"Slot\":null,\"Function\":null,\"Controller\":\"0\",\"Target\":\"0\",\"Unit\":\"1\"},\"IsReadOnly\":false},{\"Volume\":null,\"Type\":0,\"Device\":0,\"Driver\":{\"Name\":\"qemu\",\"Type\":\"raw\"},\"Source\":{\"Name\":null,\"Protocol\":null,\"File\":\"/opt/OSX-KVM/BaseSystem.img\",\"Host\":null},\"Target\":{\"Device\":\"sdc\",\"Bus\":1},\"Address\":{\"Type\":1,\"Domain\":null,\"Bus\":\"0\",\"Slot\":null,\"Function\":null,\"Controller\":\"0\",\"Target\":\"0\",\"Unit\":\"2\"},\"IsReadOnly\":false}],\"MachineType\":\"pc-q35-4.2\",\"MachineArch\":\"x86_64\",\"OsInfoId\":null,\"CpuUtilization\":{\"LastSecond\":0,\"LastMinute\":0,\"PerSecondValues\":[0,0,0,0],\"PerMinuteValues\":[0,0]}}],\"StoragePools\":[{\"UniqueId\":\"d0f6c71c-bac9-4d54-be9c-d6c244415084\",\"IsActive\":true,\"Name\":\"default\",\"State\":2,\"CapacityInByte\":1997713510400,\"ByteAvailable\":1951045537792,\"ByteAllocated\":46667972608,\"DriverType\":\"dir\",\"Volumes\":[{\"Key\":\"/var/lib/libvirt/images/macos-bigsur-base.qcow2\",\"Path\":\"/var/lib/libvirt/images/macos-bigsur-base.qcow2\",\"Name\":\"macos-bigsur-base.qcow2\",\"VolumeType\":0,\"CapacityInByte\":274877906944,\"ByteAllocated\":30348349440,\"StoragePool\":null,\"Format\":\"qcow2\",\"CreatedAt\":\"2021-10-11T14:20:18\",\"ModifiedAt\":\"2021-10-11T14:20:14\"}]}],\"StorageVolumes\":[{\"Key\":\"/var/lib/libvirt/images/macos-bigsur-base.qcow2\",\"Path\":\"/var/lib/libvirt/images/macos-bigsur-base.qcow2\",\"Name\":\"macos-bigsur-base.qcow2\",\"VolumeType\":0,\"CapacityInByte\":274877906944,\"ByteAllocated\":30348349440,\"StoragePool\":{\"UniqueId\":\"d0f6c71c-bac9-4d54-be9c-d6c244415084\",\"IsActive\":true,\"Name\":\"default\",\"State\":2,\"CapacityInByte\":1997713510400,\"ByteAvailable\":1951045537792,\"ByteAllocated\":46667972608,\"DriverType\":\"dir\",\"Volumes\":[null]},\"Format\":\"qcow2\",\"CreatedAt\":\"2021-10-11T14:20:18\",\"ModifiedAt\":\"2021-10-11T14:20:14\"}]}"));
        }

        /// <inheritdoc/>
        public Task<Domain> GetDomainAsync(Guid domainId)
        {
            return Task.FromResult(JsonSerializer.Deserialize<Domain>("{\"UniqueId\":\"2aca0dd6-cec9-4717-9ab2-0b7b13d111c3\",\"IsActive\":false,\"Id\":-1,\"Name\":\"macOS\",\"OSType\":\"hvm\",\"CpuTimeUsed\":\"00:00:00\",\"MemoryUsedKbyte\":8290304,\"MemoryMaxKbyte\":8290304,\"CpuCount\":4,\"Connection\":null,\"State\":5,\"DriverType\":\"kvm\",\"ModifiedAt\":\"2021-10-11T14:20:18.0660428Z\",\"UptimeSeconds\":0,\"GraphicsDevices\":[{\"Type\":1,\"Listen\":null,\"Port\":0,\"IsAutoPort\":false}],\"NetworkInterfaces\":[],\"DiskDevices\":[{\"Volume\":null,\"Type\":0,\"Device\":0,\"Driver\":{\"Name\":\"qemu\",\"Type\":\"qcow2\"},\"Source\":{\"Name\":null,\"Protocol\":null,\"File\":\"/opt/OSX-KVM/OpenCore-Catalina/OpenCore-NoPicker-Intel.qcow2\",\"Host\":null},\"Target\":{\"Device\":\"sda\",\"Bus\":1},\"Address\":{\"Type\":1,\"Domain\":null,\"Bus\":\"0\",\"Slot\":null,\"Function\":null,\"Controller\":\"0\",\"Target\":\"0\",\"Unit\":\"0\"},\"IsReadOnly\":false},{\"Volume\":null,\"Type\":0,\"Device\":0,\"Driver\":{\"Name\":\"qemu\",\"Type\":\"qcow2\"},\"Source\":{\"Name\":null,\"Protocol\":null,\"File\":\"/var/lib/libvirt/images/macos-bigsur-base.qcow2\",\"Host\":null},\"Target\":{\"Device\":\"sdb\",\"Bus\":1},\"Address\":{\"Type\":1,\"Domain\":null,\"Bus\":\"0\",\"Slot\":null,\"Function\":null,\"Controller\":\"0\",\"Target\":\"0\",\"Unit\":\"1\"},\"IsReadOnly\":false},{\"Volume\":null,\"Type\":0,\"Device\":0,\"Driver\":{\"Name\":\"qemu\",\"Type\":\"raw\"},\"Source\":{\"Name\":null,\"Protocol\":null,\"File\":\"/opt/OSX-KVM/BaseSystem.img\",\"Host\":null},\"Target\":{\"Device\":\"sdc\",\"Bus\":1},\"Address\":{\"Type\":1,\"Domain\":null,\"Bus\":\"0\",\"Slot\":null,\"Function\":null,\"Controller\":\"0\",\"Target\":\"0\",\"Unit\":\"2\"},\"IsReadOnly\":false}],\"MachineType\":\"pc-q35-4.2\",\"MachineArch\":\"x86_64\",\"OsInfoId\":null,\"CpuUtilization\":{\"LastSecond\":0,\"LastMinute\":0,\"PerSecondValues\":[0,0,0,0],\"PerMinuteValues\":[0,0]}}"));
        }

        /// <inheritdoc/>
        public Task<List<Domain>> GetDomainsAsync()
        {
            return Task.FromResult(JsonSerializer.Deserialize<List<Domain>>("[{\"UniqueId\":\"2a0a193c-7a96-48d4-9175-376789f3157b\",\"IsActive\":false,\"Id\":-1,\"Name\":\"TestVM\",\"OSType\":\"hvm\",\"CpuTimeUsed\":\"00:00:00\",\"MemoryUsedKbyte\":1048576,\"MemoryMaxKbyte\":1048576,\"CpuCount\":1,\"Connection\":null,\"State\":5,\"DriverType\":\"kvm\",\"ModifiedAt\":\"2021-10-08T04:14:00.0372062Z\",\"UptimeSeconds\":0,\"GraphicsDevices\":[{\"Type\":1,\"Listen\":null,\"Port\":0,\"IsAutoPort\":false}],\"NetworkInterfaces\":[{\"Type\":0,\"MAC\":{\"Address\":\"52:54:00:b2:0b:ef\"},\"Source\":{\"Network\":\"default\",\"Bridge\":null},\"Target\":null,\"Model\":{\"Type\":\"e1000\"},\"Address\":{\"Type\":0,\"Domain\":\"0x0000\",\"Bus\":\"0x02\",\"Slot\":\"0x01\",\"Function\":\"0x0\",\"Controller\":null,\"Target\":null,\"Unit\":null}}],\"DiskDevices\":[],\"MachineType\":\"pc-q35-6.1\",\"MachineArch\":\"x86_64\",\"OsInfoId\":null,\"CpuUtilization\":{\"LastSecond\":0,\"LastMinute\":0,\"PerSecondValues\":[0,0,0,0],\"PerMinuteValues\":[0,0]}},{\"UniqueId\":\"2aca0dd6-cec9-4717-9ab2-0b7b13d111c3\",\"IsActive\":false,\"Id\":-1,\"Name\":\"macOS\",\"OSType\":\"hvm\",\"CpuTimeUsed\":\"00:00:00\",\"MemoryUsedKbyte\":8290304,\"MemoryMaxKbyte\":8290304,\"CpuCount\":4,\"Connection\":null,\"State\":5,\"DriverType\":\"kvm\",\"ModifiedAt\":\"2021-10-11T14:20:18.0660428Z\",\"UptimeSeconds\":0,\"GraphicsDevices\":[{\"Type\":1,\"Listen\":null,\"Port\":0,\"IsAutoPort\":false}],\"NetworkInterfaces\":[],\"DiskDevices\":[{\"Volume\":null,\"Type\":0,\"Device\":0,\"Driver\":{\"Name\":\"qemu\",\"Type\":\"qcow2\"},\"Source\":{\"Name\":null,\"Protocol\":null,\"File\":\"/opt/OSX-KVM/OpenCore-Catalina/OpenCore-NoPicker-Intel.qcow2\",\"Host\":null},\"Target\":{\"Device\":\"sda\",\"Bus\":1},\"Address\":{\"Type\":1,\"Domain\":null,\"Bus\":\"0\",\"Slot\":null,\"Function\":null,\"Controller\":\"0\",\"Target\":\"0\",\"Unit\":\"0\"},\"IsReadOnly\":false},{\"Volume\":null,\"Type\":0,\"Device\":0,\"Driver\":{\"Name\":\"qemu\",\"Type\":\"qcow2\"},\"Source\":{\"Name\":null,\"Protocol\":null,\"File\":\"/var/lib/libvirt/images/macos-bigsur-base.qcow2\",\"Host\":null},\"Target\":{\"Device\":\"sdb\",\"Bus\":1},\"Address\":{\"Type\":1,\"Domain\":null,\"Bus\":\"0\",\"Slot\":null,\"Function\":null,\"Controller\":\"0\",\"Target\":\"0\",\"Unit\":\"1\"},\"IsReadOnly\":false},{\"Volume\":null,\"Type\":0,\"Device\":0,\"Driver\":{\"Name\":\"qemu\",\"Type\":\"raw\"},\"Source\":{\"Name\":null,\"Protocol\":null,\"File\":\"/opt/OSX-KVM/BaseSystem.img\",\"Host\":null},\"Target\":{\"Device\":\"sdc\",\"Bus\":1},\"Address\":{\"Type\":1,\"Domain\":null,\"Bus\":\"0\",\"Slot\":null,\"Function\":null,\"Controller\":\"0\",\"Target\":\"0\",\"Unit\":\"2\"},\"IsReadOnly\":false}],\"MachineType\":\"pc-q35-4.2\",\"MachineArch\":\"x86_64\",\"OsInfoId\":null,\"CpuUtilization\":{\"LastSecond\":0,\"LastMinute\":0,\"PerSecondValues\":[0,0,0,0],\"PerMinuteValues\":[0,0]}}]"));
        }

        /// <inheritdoc/>
        public Task<List<StoragePoolElement>> GetStoragePoolsAsync()
        {
            return Task.FromResult(JsonSerializer.Deserialize<List<StoragePoolElement>>("[{\"UniqueId\":\"d0f6c71c-bac9-4d54-be9c-d6c244415084\",\"IsActive\":true,\"Name\":\"default\",\"State\":2,\"CapacityInByte\":1997713510400,\"ByteAvailable\":1951045537792,\"ByteAllocated\":46667972608,\"DriverType\":\"dir\",\"Volumes\":[{\"Key\":\"/var/lib/libvirt/images/macos-bigsur-base.qcow2\",\"Path\":\"/var/lib/libvirt/images/macos-bigsur-base.qcow2\",\"Name\":\"macos-bigsur-base.qcow2\",\"VolumeType\":0,\"CapacityInByte\":274877906944,\"ByteAllocated\":30348349440,\"StoragePool\":null,\"Format\":\"qcow2\",\"CreatedAt\":\"2021-10-11T14:20:18\",\"ModifiedAt\":\"2021-10-11T14:20:14\"}]}]"));
        }

        /// <inheritdoc/>
        public Task<List<StorageVolumeStoragePool>> GetStorageVolumesAsync()
        {
            return Task.FromResult(JsonSerializer.Deserialize<List<StorageVolumeStoragePool>>("[{\"Key\":\"/var/lib/libvirt/images/macos-bigsur-base.qcow2\",\"Path\":\"/var/lib/libvirt/images/macos-bigsur-base.qcow2\",\"Name\":\"macos-bigsur-base.qcow2\",\"VolumeType\":0,\"CapacityInByte\":274877906944,\"ByteAllocated\":30348349440,\"StoragePool\":{\"UniqueId\":\"d0f6c71c-bac9-4d54-be9c-d6c244415084\",\"IsActive\":true,\"Name\":\"default\",\"State\":2,\"CapacityInByte\":1997713510400,\"ByteAvailable\":1951045537792,\"ByteAllocated\":46667972608,\"DriverType\":\"dir\",\"Volumes\":[null]},\"Format\":\"qcow2\",\"CreatedAt\":\"2021-10-11T14:20:18\",\"ModifiedAt\":\"2021-10-11T14:20:14\"}]"));
        }

        /// <inheritdoc/>
        public Task StartConnectionAsync()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task StopConnectionAsync()
        {
            return Task.CompletedTask;
        }
    }
}
