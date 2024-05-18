using APBD_EF.Context;
using APBD_EF.Services;
using Microsoft.EntityFrameworkCore;

namespace APBD_EF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApbdEfContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<ITripService, TripService>();
            builder.Services.AddScoped<IClientService, ClientService>();

           

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}