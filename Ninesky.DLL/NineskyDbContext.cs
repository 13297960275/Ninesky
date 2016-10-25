using Ninesky.Models;
using System.Data.Entity;

namespace Ninesky.DAL
{
    /// <summary>
    /// 数据上下文
    /// <remarks>创建：2014.02.03</remarks>
    /// </summary>
    public class NineskyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleRelation> UserRoleRelations { get; set; }
        public DbSet<UserConfig> UserConfig { get; set; }
        public NineskyDbContext()
            : base("NineskyDB")
        {
            Database.CreateIfNotExists();
        }
    }
}