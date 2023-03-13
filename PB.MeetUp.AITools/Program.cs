using MongoDB.Driver;
using PB.MeetUp.AITools.Models;
using PB.MeetUp.AITools.Mongo;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddSingleton<MongoSettings>();
builder.Services.AddSingleton<IMongoClient>(opt =>
{
    var mongoSettings = opt.GetRequiredService<MongoSettings>();
    var mongoClient = new MongoClient(mongoSettings.ConnectionString);
    
    return mongoClient;
});

builder.Services.AddSingleton<IMongoProvider<Book, string>>(provider =>
{
    var settings = provider.GetRequiredService<MongoSettings>();
    var client = provider.GetRequiredService<IMongoClient>();
    return new MongoProvider<Book>(client, settings.DatabaseName);
});

    builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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