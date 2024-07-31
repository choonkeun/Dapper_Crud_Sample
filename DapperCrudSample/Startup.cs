
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

namespace DapperCrudSample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddSingleton<SqlConnection>(sp =>
            //{
            //    var configuration = sp.GetRequiredService<IConfiguration>();
            //    return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            //});
        }

    }
}
