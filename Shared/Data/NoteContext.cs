using MongoDB.Driver;
using Notepad.API.Features.Notepad;

namespace Notepad.API.Shared.Data;

public class NoteContext : INoteContext
{
    private readonly IMongoCollection<NoteEntity> _notes;

    public NoteContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        _notes = database.GetCollection<NoteEntity>("Notes");
    }

    public IMongoCollection<NoteEntity> Notes => _notes;
}