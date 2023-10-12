using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Entities;
using FAMS.V0.Shared.Extensions;
using Polly;
using Polly.Timeout;
using Policy = Polly.Policy;

var builder = WebApplication.CreateBuilder(args);
var random = new Random();

// Add services to the container.




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddMongo()
    .AddMongoRepository<User>(DbCollection.User);

builder.Services
    .AddHttpClient<HttpClient>(client =>
    {
        client.BaseAddress = new Uri("http://localhost:5000");
    })
    .AddTransientHttpErrorPolicy(policy => policy.Or<TimeoutRejectedException>().WaitAndRetryAsync(
        5,
        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromSeconds(random.Next(0, 1000))
    ))
    .AddTransientHttpErrorPolicy(policy => policy.Or<TimeoutRejectedException>().CircuitBreakerAsync(3, TimeSpan.FromSeconds(15)))
    .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));

builder.Services.AddRabbitMq(Service.UserService);

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