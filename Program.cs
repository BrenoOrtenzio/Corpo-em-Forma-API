using ModuleAPI.Context;
using Microsoft.EntityFrameworkCore;
using ModuleAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PrincipalContext> (options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoLocal")));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(
    options => options.Filters.Add(new RequestFilter())
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();