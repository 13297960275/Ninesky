using Ninesky.IDAL;
using Ninesky.Models;

namespace Ninesky.DAL
{
    /// <summary>
    /// 简单工厂
    /// <remarks>创建：2014.02.03</remarks>
    /// </summary>
    public static class RepositoryFactory
    {
        /// <summary>
        /// 文章仓储
        /// </summary>
        public static InterfaceBaseRepository<Article> ArticleRepository { get; set; }

        /// <summary>
        /// 附件仓储
        /// </summary>
        public static InterfaceBaseRepository<Attachment> AttachmentRepository { get; set; }

        /// <summary>
        /// 栏目仓储
        /// </summary>
        public static InterfaceBaseRepository<Category> CategoryRepository { get; set; }

        /// <summary>
        /// 公共模型仓储
        /// </summary>
        public static InterfaceBaseRepository<CommonModel> CommonModelRepository { get; set; }

        /// <summary>
        /// 用户仓储
        /// </summary>
        public static InterfaceUserRepository UserRepository { get { return new UserRepository(); } }
    }
}