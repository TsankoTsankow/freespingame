using FreeSpinGame.Api.Middleware;
using FreeSpinGame.Application;
using FreeSpinGame.Domain.Entities;
using FreeSpinGame.Infrastructure;
using FreeSpinGame.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddMediatr();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    db.Database.EnsureCreated();

    if (!db.Campaigns.Any())
    {
        db.Campaigns.Add(new Campaign("1", 2));
        db.SaveChanges();
    }
}

app.Run();
