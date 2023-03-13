namespace PB.MeetUp.AITools.Mongo;

public class MongoSettings
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }

    public MongoSettings(IConfiguration configuration)
    {
        DatabaseName = configuration.GetSection("Mongo").GetValue<string>("DatabaseName");
        ConnectionString = configuration.GetSection("Mongo").GetValue<string>("ConnectionString");
    }
}