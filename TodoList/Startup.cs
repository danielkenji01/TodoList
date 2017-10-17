﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using TodoList.Infraestructure;

namespace TodoList
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            DatabaseConfigurationAction = GetDatabaseConfiguration();
        }

        public IConfiguration Configuration { get; }
        public Action<DbContextOptionsBuilder> DatabaseConfigurationAction { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<Db>(DatabaseConfigurationAction)
                    .AddAutoMapper(typeof(Startup));

            services.AddMediatR(typeof(Startup));
            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        public virtual Action<DbContextOptionsBuilder> GetDatabaseConfiguration()
        {
            return options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}