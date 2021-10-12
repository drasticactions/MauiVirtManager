using IDNT.AppBasics.Virtualization.Libvirt;
using IDNT.AppBasics.Virtualization.Libvirt.Events;

namespace VirtServer.Common
{
    public class DomainEventCommand
    {
        public LibvirtDomain Domain { get; set; }

        public VirDomainEventArgs EventArgs { get; set; }
    }
}
