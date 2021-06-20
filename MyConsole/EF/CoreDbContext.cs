using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyConsole
{
    public  class CoreDbContext: DbContext
    {

        public static string ConnectString = "Data Source=.;Initial Catalog=Test;User ID=sa;Password=sa1234";

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }

        public CoreDbContext() { 
        
        }

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {

        }

        public CoreDbContext(DbContextOptions options) : base(options)
        {

        }


        public static CoreDbContext GetContext(string ConnectString){
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            var option = optionsBuilder.UseSqlServer(ConnectString).Options;
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
