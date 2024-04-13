using Notepad.API.Features.Notes;
using Notepad.API.Features.Notes.Create;

namespace UnitTests.Features.Notes.Create;

public class CreateHandlerTest
{
    private readonly Mock<INoteData> _noteDataMock;
    private readonly Mock<IValidator<CreateCommand>> _validatorMock;
    private readonly CreateHandler _handler;

    public CreateHandlerTest()
    {
        _noteDataMock = new Mock<INoteData>();
        _validatorMock = new Mock<IValidator<CreateCommand>>();
        _handler = new CreateHandler(_noteDataMock.Object, _validatorMock.Object);
    }

    [Fact(DisplayName = "Handle With Valid Request Return Result With Guid")]
    public async Task Handle_WithValidRequest_ReturnResultWithGuid()
    {
        // Arrange
        var request = new CreateCommand();
        var expectedId = Guid.NewGuid();
        var validationResult = new ValidationResult();

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        _noteDataMock.Setup(expression => expression.CreateNoteAsync(It.IsAny<NoteEntity>(), It.IsAny<CancellationToken>()))
            .Callback<NoteEntity, CancellationToken>((entity, _) => entity.Id = expectedId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data.Should().Be(expectedId);

        _noteDataMock.Verify(expression => expression.CreateNoteAsync(It.IsAny<NoteEntity>(),
                    It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Handle With Valid Request Return Result With Error")]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange
        var request = new CreateCommand();
        var validationErrorMessage = "Validation error message";
        var validationResult = new ValidationResult(new List<ValidationFailure> { new(default, validationErrorMessage, default) });

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(Errors.ReturnInvalidEntriesError(validationErrorMessage));

        _noteDataMock.Verify(expression => expression.CreateNoteAsync(It.IsAny<NoteEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}