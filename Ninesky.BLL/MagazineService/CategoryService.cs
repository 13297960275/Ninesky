using Ninesky.DAL;
using Ninesky.IBLL;
using Ninesky.Models;

namespace Ninesky.BLL
{
    /// <summary>
    /// 栏目服务
    /// <remarks>
    /// 创建:2014.02.27
    /// </remarks>
    /// </summary>
    public class CategoryService : BaseService<Category>, InterfaceCategoryService
    {
        public CategoryService() : base(RepositoryFactory.CategoryRepository) { }
    }
}