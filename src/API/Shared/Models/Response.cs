namespace Notepad.API.Shared.Models;
public record Response<T>(T? Data = default, object? Errors = default);