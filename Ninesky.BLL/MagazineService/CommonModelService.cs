using Ninesky.DAL;
using Ninesky.IBLL;
using Ninesky.Models;

namespace Ninesky.BLL
{
    /// <summary>
    /// 公共模型服务
    /// </summary>
    public class CommonModelService : BaseService<CommonModel>, InterfaceCommonModelService
    {
        public CommonModelService() : base(RepositoryFactory.CommonModelRepository) { }
    }
}
