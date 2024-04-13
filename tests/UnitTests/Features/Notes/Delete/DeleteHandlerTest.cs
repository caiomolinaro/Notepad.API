using Notepad.API.Features.Notes;
using Notepad.API.Features.Notes.Delete;

namespace UnitTests.Features.Notes.Delete;

public class DeleteHandlerTest
{
    private readonly Mock<INoteData> _noteDataMock;
    private readonly Mock<IValidator<DeleteCommand>> _validatorMock;
    private readonly DeleteHandler _handler;

    public DeleteHandlerTest()
    {
        _noteDataMock = new Mock<INoteData>();
        _validatorMock = new Mock<IValidator<DeleteCommand>>();
        _handler = new DeleteHandler(_noteDataMock.Object, _validatorMock.Object);
    }

    [Fact(DisplayName = "Handle With Valid Request Return Result With Guid")]
    public async Task Handle_WithValidRequest_ReturnResultWithGuid()
    {
        // Arrange
        var request = new DeleteCommand
        {
            Id = Guid.NewGuid()
        };
        var expectedResultId = request.Id;
        var validationResult = new ValidationResult();

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        _noteDataMock.Setup(expression => expression.GetNoteByIdAsync(request.Id, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new NoteEntity { Id = request.Id });

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data.Should().Be(expectedResultId);

        _noteDataMock.Verify(expression => expression.DeleteNoteAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Handle_WithInvalidRequest_ReturnsResultWithError")]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange
        var request = new DeleteCommand();
        var validationErrorMessage = "Validation error message";
        var validationResult = new ValidationResult(new List<ValidationFailure> { new(default, validationErrorMessage, default) });

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(Errors.ReturnInvalidEntriesError(validationErrorMessage));

        _noteDataMock.Verify(expression => expression.DeleteNoteAsync(Guid.Empty, It.IsAny<CancellationToken>()), Times.Never);
    }
}