using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Domain.Entities;
using FAMS.V0.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddMongo()
    .AddMongoRepository<Syllabus>(DbCollection.Syllabus);

builder.Services.AddRabbitMq(Service.SyllabusService);

builder.Services.AddJwtAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();