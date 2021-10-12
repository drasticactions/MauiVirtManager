using IDNT.AppBasics.Virtualization.Libvirt;
using IDNT.AppBasics.Virtualization.Libvirt.Events;

namespace VirtServer.Common
{
    public class StoragePoolLifecycleEventCommand
    {
        public LibvirtStoragePool StoragePool { get; set; }

        public VirStoragePoolLifecycleEventArgs EventArgs { get; set; }
    }
}
