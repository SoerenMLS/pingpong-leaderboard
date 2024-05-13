using Microsoft.Data.Sqlite;
using PING_PONG_API.Domain.Misc;
using PING_PONG_API.Domain.Repositories;
using PING_PONG_API.Domain.Services;
using PING_PONG_API.Interfaces;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var databaseConnectionString = builder.Configuration.GetConnectionString("SQLiteConnection");

if(databaseConnectionString == null)
    Environment.Exit(-2);

SQLiteInitializer.InitDb(databaseConnectionString);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IDbConnection>(sp => new SqliteConnection(databaseConnectionString));
builder.Services.AddSingleton<IMatchRepository, MatchRepository>();
builder.Services.AddSingleton<IPlayerRepository, PlayerRepository>();
builder.Services.AddSingleton<ILeaderBoardService, LeaderboardService>();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy(name: "WildcardPolicy", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("WildcardPolicy");


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
