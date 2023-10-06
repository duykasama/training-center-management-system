using FAMS.V0.Services.SyllabusService.Entities;
using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Extensions;
using FAMS.V0.Shared.Interfaces;
using FAMS.V0.Shared.Repositories;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(_ => new MongoClient(builder.Configuration.GetConnectionString(Connection.MongoDbConnection)).GetDatabase(Database.FAMS_DB).GetCollection<Syllabus>(Collection.Syllabus))
    .AddSingleton<IRepository<Syllabus>>(services =>
    {
        var database = services.GetService<IMongoDatabase>();
        return new MongoRepository<Syllabus>(database, Collection.Syllabus);
    });

builder.Services
    .AddMongo()
    .AddMongoRepository<Syllabus>(Collection.Syllabus);

builder.Services.AddRabbitMq(Service.SyllabusService);


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