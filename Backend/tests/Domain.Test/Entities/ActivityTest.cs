using CommonTestUltilities.Entities;
using FluentAssertions;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace Domain.Test.Entities;

public class ActivityTest
{
    [Fact]
    public void Success()
    {
        // Arrange & Act
        var attendance = ActivityBuilder.Build();
        // Assert
        attendance.Should().NotBeNull();
    }

    [Fact]
    public void CreateActivity_WithEmptyAttendanceId_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => ActivityBuilder.Build(attendanceId: Guid.Empty);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
            .Which.Errors.Should().Contain(ErrorMessages.invalidAttendanceId);
    }

    [Fact]
    public void CreateActivity_WithEmptyUserId_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => ActivityBuilder.Build(userId: Guid.Empty);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
            .Which.Errors.Should().Contain(ErrorMessages.invalidUserId);
    }

    [Fact]
    public void CreateActivity_WithEmptyProjectId_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => ActivityBuilder.Build(projectId: Guid.Empty);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
            .Which.Errors.Should().Contain(ErrorMessages.invalidProjectId);
    }

    [Fact]
    public void CreateActivity_WithRecordedAtInFuture_ShouldThrowException()
    {
        // Arrange & Act
        var futureDate = DateTime.Now.AddDays(1);
        Action act = () => ActivityBuilder.Build(recordedAt: futureDate);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
            .Which.Errors.Should().Contain(ErrorMessages.invalidRecordedAtDate);
    }

    [Fact]
    public void CreateActivity_WithEmptyProofFilePath_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => ActivityBuilder.Build(proofFilePath: "");

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
            .Which.Errors.Should().Contain(ErrorMessages.invalidProofFilePath);
    }

    [Fact]
    public void CreateActivity_WithNullProofFilePath_ShouldNotThrowException()
    {
        // Arrange & Act
        var activity = ActivityBuilder.Build(proofFilePath: null);

        // Assert
        activity.Should().NotBeNull();
        activity.ProofFilePath.Should().BeNull();
    }
}
