using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Domain.Entities;
using FAMS.V0.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders().AddConsole();

builder.Services.AddMongo();
builder.Services.AddMongoRepository<User>(DbCollection.User);

builder.Services.AddRabbitMq(Service.UserService);
builder.Services.AddJwtAuthentication();
builder.Services.AddCorsDefault();
builder.Services.AddMongoRepository<UserPermission>(DbCollection.UserPermission);
builder.Services.AddRedis();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("fams-cors-policy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();