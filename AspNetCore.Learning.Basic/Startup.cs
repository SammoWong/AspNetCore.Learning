using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Learning.Basic.Services;
using AspNetCore.Learning.Basic.Services.Interfaces;
using AspNetCore.Learning.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace AspNetCore.Learning.Basic
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// <summary>
        /// 把services(各种服务, 例如identity, ef, mvc等等包括第三方的, 或者自己写的)加入到container(asp.net core的容器)中, 并配置这些services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddMvcOptions(options =>
                    {
                        options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());//asp.net core默认只实现了json返回格式，可以修改Mvc的配置来添加xml格式（需要在请求头Header中添加Accept：application/xml）
                    });
            
            services.AddTransient<IMailService, LocalMailService>();
            //services.AddTransient<IMailService, CloudMailService>();
            var connectionString = Configuration["connectionStrings:coreLearningDbConnectionString"];
            services.AddDbContext<MyContext>(o => o.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 用来具体指定如何处理每个http请求
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, MyContext myContext)//参数app、env都是已经注入的services
        {
            //loggerFactory.AddProvider(new NLogLoggerProvider());
            loggerFactory.AddNLog();//添加NLog日志服务
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();//在Development环境下调用异常处理程序
            }
            else
            {
                app.UseExceptionHandler();//异常处理中间件
            }
            myContext.EnsureSeedDataForContext();//添加种子数据
            app.UseStatusCodePages();
            //使用mvc来处理http请求:注意顺序, 应该在处理异常的中间件后边调用app.UseMvc(), 
            //所以处理异常的middleware可以在把request交给mvc之间就处理异常, 更重要的是它还可以捕获并处理MVC返回的相关代码中的异常.
            app.UseMvc();

        }
    }
}
