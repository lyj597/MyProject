using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyConsole
{
    public  class CoreDbContext: DbContext
    {

        public static string ConnectString = "Data Source=.;Initial Catalog=Test;User ID=sa;Password=sa1234";

        private static List<string> readConnectList = new List<string>()
        {
            "Data Source=.;Initial Catalog=TestCopy1;User ID=sa;Password=sa1234",
            "Data Source=.;Initial Catalog=TestCopy2;User ID=sa;Password=sa1234",
            "Data Source=.;Initial Catalog=TestCopy3;User ID=sa;Password=sa1234"
        };

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<TestAA> TestAAs { get; set; }

        public DbSet<TaskTest1> taskTest1s { get; set; }

        public DbSet<TaskTest2> taskTest2s { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }

        /// <summary>
        /// 定义带参数标量值函数方法，并将其映射到数据库函数
        /// </summary>
        /// <param name="BlogId"></param>
        /// <returns></returns>
        public int ActivePostCountForBlog(int BlogId) => throw new NotSupportedException();

        /// <summary>
        /// 定义不带参数的表值函数
        /// </summary>
        /// <returns></returns>
        public IQueryable<Blog> getPostCountForBlog()
                  => FromExpression(() => getPostCountForBlog());

        public CoreDbContext() { 
        
        }

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {

        }

        public CoreDbContext(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static string GetDbConnectString(ConnectDbType dbType) {
            if (dbType == ConnectDbType.Write) {
                return ConnectString;
            }
            var num= new Random(DateTime.Now.Millisecond).Next(0, readConnectList.Count-1);
            return readConnectList[num];
        }

        public static CoreDbContext GetContext(ConnectDbType dbType)
        {
            var connectStr= GetDbConnectString(dbType);
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            var option = optionsBuilder.UseSqlServer(connectStr).Options;
            return new CoreDbContext(option);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectString);
            //.LogTo(Console.WriteLine); //将日志输出到控制台
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            /////设置必填项
            //modelBuilder.Entity<Blog>()
            //     .HasIndex(a => a.BlogId);

            //modelBuilder.Entity<Blog>().ToFunction("getPostCountForBlog");

            ///映射带参数的标量值函数
            modelBuilder.HasDbFunction(typeof(CoreDbContext).GetMethod(nameof(ActivePostCountForBlog), new[] { typeof(int) }))
            .HasName("CommentedPostCountForBlog");

            ///映射不带参数的标量值函数
            //modelBuilder.HasDbFunction(typeof(CoreDbContext).GetMethod(nameof(ActivePostCountForBlog)))
            //.HasName("CommentedPostCountForBlog");

            //映射不带参数的表值函数
            modelBuilder.HasDbFunction(typeof(CoreDbContext).GetMethod(nameof(getPostCountForBlog)))
                .HasName("getPostCountForBlog");


            //modelBuilder.HasDbFunction(typeof(CoreDbContext).GetMethod(nameof(getBlogs)))
            //.HasName("getPostCountForBlog");

            //modelBuilder.Entity<Person>()
            //.HasDiscriminator<string>("BlogType") //设置标识列的数据类型string,列名为：BlogType. 数据类型可以是string、int
            //.HasValue<Person>("Person") //指定数据库插入一条数据是，不同的数据类型标识不同的值。 插入Blog类型的数据时，BlogType列值为：BlogBase
            //.HasValue<Student>("Student");

            //modelBuilder.Entity<Person>()
            //.HasDiscriminator<int>("BlogType") //设置BlogType为int类型
            //  .HasValue<Person>(0) //插入Blog类型数据时，BlogType对应的值时0
            //  .HasValue<Student>(1);

        }

    }
}
