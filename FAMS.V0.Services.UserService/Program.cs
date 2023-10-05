using FAMS.V0.Services.UserService.Entities;
using FAMS.V0.Services.UserService.Repositories;
using FAMS.V0.Shared.DbClient;
using FAMS.V0.Shared.Enums;
using FAMS.V0.Shared.Interfaces;
using FAMS.V0.Shared.Repositories;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddSingleton(_ => new MongoClient(builder.Configuration.GetConnectionString(Connection.MongoDbConnection)).GetDatabase(Database.FAMS_DB))
    .AddScoped<IRepository<User>>(serviceProvider =>
    {
        var database = serviceProvider.GetService<IMongoDatabase>();
        return new MongoRepository<User>(database, Collection.User);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();