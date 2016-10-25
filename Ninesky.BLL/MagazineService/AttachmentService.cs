using Ninesky.DAL;
using Ninesky.IBLL;
using Ninesky.Models;

namespace Ninesky.BLL
{
    /// <summary>
    /// 附件服务
    /// </summary>
    class AttachmentService : BaseService<Attachment>, InterfaceAttachmentService
    {
        public AttachmentService() : base(RepositoryFactory.AttachmentRepository) { }
    }
}
