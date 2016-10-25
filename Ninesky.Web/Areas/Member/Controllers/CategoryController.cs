using Ninesky.BLL;
using Ninesky.IBLL;
using System.Web.Mvc;

namespace Ninesky.Web.Areas.Member.Controllers
{
    /// <summary>
    /// 栏目控制器
    /// </summary>
    [Authorize]
    public class CategoryController : Controller
    {
        private InterfaceCategoryService categoryRepository;
        public CategoryController() { categoryRepository = new CategoryService(); }


    }
}