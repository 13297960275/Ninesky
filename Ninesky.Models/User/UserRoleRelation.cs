using System.ComponentModel.DataAnnotations;

namespace Ninesky.Models
{
    /// <summary>
    /// 用户角色关系
    /// <remarks>
    /// 创建：2014.02.16
    /// </remarks>
    /// </summary>
    public class UserRoleRelation
    {
        [Key]
        public int RelationID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required()]
        public int UserID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Required()]
        public int RoelID { get; set; }
    }
}