using Notepad.API.Shared.Models;

namespace Notepad.API.Features.Notepad;

internal static class Errors
{
    internal static Error ReturnInvalidEntriesError(string errorDetails) => new(errorCode: "NOTE001",
        errorMessage: "Invalid entries", errorDetails);

    internal static Error ReturnNoteNotFoundError() => new(errorCode: "NOTE002",
        errorMessage: "Note not found");
}