using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sqli.Services;

namespace Sqli
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IEmployeeService employeeService)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Employees}/{action=Index}/{id?}");
            });
            employeeService.ResetDatabase();
        }
    }
}