using CommonTestUltilities.Entities;
using FluentAssertions;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace Domain.Test.Entities;

public class AttendanceTest
{
    [Fact]
    public void Success()
    {
        // Arrange & Act
        var attendance = AttendanceBuilder.Build();
        // Assert
        attendance.Should().NotBeNull();
    }

    [Fact] 
    public void CreateAttendance_WithEmptyUserId_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => AttendanceBuilder.Build(userId: Guid.Empty, date: DateTime.Now);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
        .Which.Errors.Should().Contain(ErrorMessages.InvalidUserId);
    }

    [Fact]
    public void CreateAttendance_WithFutureDate_ShouldThrowException()
    {
        // Arrange
        var futureDate = DateTime.Now.AddDays(1);
        
        // Act
        Action act = () => AttendanceBuilder.Build(date: futureDate);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
            .Which.Errors.Should().Contain(ErrorMessages.InvalidAttendanceDate);
    }


    [Fact]
    public void CreateActivity_WithEmptyProjectId_ShouldThrowException()
    {
        // Arrange & Act
        Action act = () => AttendanceBuilder.Build(projectId: Guid.Empty, date: DateTime.Now);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
            .Which.Errors.Should().Contain(ErrorMessages.InvalidProjectId);
    }

    [Fact]
    public void CreateAttendance_WithCheckOutBeforeCheckIn_ShouldThrowException()
    {
        // Arrange
        var checkIn = new TimeSpan(10, 0, 0);
        var checkOut = new TimeSpan(9, 0, 0);  

        // Act
        Action act = () => AttendanceBuilder.Build(checkIn: checkIn, checkOut: checkOut, date: DateTime.Now);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
            .Which.Errors.Should().Contain(ErrorMessages.InvalidCheckOutTime);
    }

    [Fact]
    public void CreateAttendance_WithCheckOutEqualToCheckIn_ShouldThrowException()
    {
        // Arrange
        var time = new TimeSpan(9, 0, 0); 

        // Act
        Action act = () => AttendanceBuilder.Build(checkIn: time, checkOut: time, date: DateTime.Now);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
            .Which.Errors.Should().Contain(ErrorMessages.InvalidCheckOutTime);
    }
}