using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Notepad.API.Features.Notes;

public class NoteEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    [BsonElement("Title")]
    public string? Title { get; set; }

    [BsonElement("Creation_Date")]
    public DateTime CreationDate { get; set; }

    [BsonElement("Body")]
    public string? Body { get; set; }
}