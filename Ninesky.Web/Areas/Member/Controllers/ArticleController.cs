using Ninesky.BLL;
using Ninesky.IBLL;
using System.Web.Mvc;

namespace Ninesky.Web.Areas.Member.Controllers
{
    /// <summary>
    /// 文章控制器
    /// </summary>
    public class ArticleController : Controller
    {
        private InterfaceArticleService articleService;
        private InterfaceCommonModelService commonModelService;
        public ArticleController() { articleService = new ArticleService(); commonModelService = new CommonModelService(); }
    }
}