
// https://github.com/patrickgod/DapperCrudTutorial.git


using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

//--- Add services to the container ---
builder.Services.AddControllers();

//-- you can separate this services to Startup.cs
//-- Add SqlConnection as a singleton
//-- 1. 'SqlConnection' is used on HomeController,
//   2. DI(dependency injection system) will search any 'SqlConnection' registration on program.cs
//   3. if there is then .NET DI use it on HomeController

builder.Services.AddSingleton<SqlConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//--- Configure the HTTP request pipeline ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
