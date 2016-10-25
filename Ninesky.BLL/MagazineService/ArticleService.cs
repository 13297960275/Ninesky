using Ninesky.DAL;
using Ninesky.IBLL;
using Ninesky.IDAL;
using Ninesky.Models;

namespace Ninesky.BLL
{
    /// <summary>
    /// 文章服务
    /// </summary>
    public class ArticleService : BaseService<Article>, InterfaceArticleService
    {
        public ArticleService() : base(RepositoryFactory.ArticleRepository) { }
    }
}
