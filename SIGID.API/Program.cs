using SIGID.Core.Application;
using SIGID.Infrastructure.Identity;
using SIGID.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//set CORS settings
var allowOrigins = builder.Configuration.GetValue<string>("allowOrigins")!.Split(",");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(optionsCORS =>
    {
        optionsCORS.WithOrigins(allowOrigins)
                    .AllowAnyMethod()           // allow any HTTP method (GET, POST, PUT, DELETE, etc.)
                    .AllowAnyHeader()           // allow any header
                    .AllowAnyHeader();
    });
});

//inject dependencies extensions of services
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer();



var app = builder.Build();

// run default seed data
await app.Services.RunIdentitySeeds();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
