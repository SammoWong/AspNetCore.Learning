using AspNetCore.Learning.EntityFrameworkCore.Configurations;
using AspNetCore.Learning.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Learning.EntityFrameworkCore
{
    /// <summary>
    /// 直接运行不会创建数据库，因为没有创建MyContext的实例，也就不会调用Constructor，所以建立一个TestController，注入MyContext，
    /// 访问TestController下的Action后也就调用了MyContext的Constructor
    /// </summary>
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            //Database.EnsureCreated();//如果数据库还没创建，就会创建一个数据库，如已创建则不发生作用
            Database.Migrate();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
