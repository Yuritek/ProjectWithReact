using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectWithReact.DAL.EF;
using ProjectWithReact.DAL.Entities;
using ProjectWithReact.DAL.Interface;
using ProjectWithReact.DAL.Repositories;
using SampleReact.Service;

namespace SampleReact
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

			IMapper mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);

			services.AddDbContext<DirectoryContext>(options =>
				options.UseNpgsql(Configuration.GetConnectionString("PG")));


			services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddTransient<DbContext, DirectoryContext>();
			services.AddTransient<IRepository<Contacts>, Repository<Contacts>>();
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
				{
					HotModuleReplacement = true,
					ReactHotModuleReplacement = true
				});
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");

				routes.MapSpaFallbackRoute(
					name: "spa-fallback",
					defaults: new {controller = "Home", action = "Index"});
			});
		}
	}
}