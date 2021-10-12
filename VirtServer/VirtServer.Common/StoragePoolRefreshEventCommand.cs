using IDNT.AppBasics.Virtualization.Libvirt;
using IDNT.AppBasics.Virtualization.Libvirt.Events;

namespace VirtServer.Common
{
    public class StoragePoolRefreshEventCommand
    {
        public LibvirtStoragePool StoragePool { get; set; }

        public VirStoragePoolRefreshEventArgs EventArgs { get; set; }
    }
}