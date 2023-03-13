namespace PB.MeetUp.AITools.Mongo;

public interface IEntity<TId>
{
    TId Id { get; set; }
}