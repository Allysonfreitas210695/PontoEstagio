using CommonTestUtilities.Entities;
using FluentAssertions;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace Domain.Test.Entities;

public class ProjectTest
{
    [Fact]
    public void Success()
    {
        // Arrange & Act
        var project = ProjectBuilder.Build();

        // Assert
        project.Should().NotBeNull();
        project.Name.Should().NotBeNullOrEmpty();
        project.TotalHours.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Should_Throw_When_Name_Is_Null_Or_Empty()
    {
        // Act
        Action act = () => ProjectBuilder.Build(name: "");

        // Assert
        act.Should()
           .Throw<ErrorOnValidationException>()
           .Where(e => e.Errors.Contains(ErrorMessages.InvalidProjectName));
    }

    [Fact]
    public void Should_Throw_When_Name_Is_Shorter_Than_3_Characters()
    {
        // Act
        Action act = () => ProjectBuilder.Build(name: "AB");

        // Assert
        act.Should()
           .Throw<ErrorOnValidationException>()
           .Where(e => e.Errors.Contains(ErrorMessages.InvalidProjectNameLength));
    }

    [Fact]
    public void Should_Throw_When_CompanyId_Is_Empty()
    {
        // Act
        Action act = () => ProjectBuilder.Build(companyId: Guid.Empty);

        // Assert
        act.Should()
           .Throw<ErrorOnValidationException>()
           .Where(e => e.Errors.Contains(ErrorMessages.InvalidCompanyId));
    }

    [Fact]
    public void Should_Throw_When_TotalHours_Is_Less_Than_Or_Equal_To_Zero()
    {
        // Act
        Action act = () => ProjectBuilder.Build(totalHours: 0);

        // Assert
        act.Should()
           .Throw<ArgumentException>()
           .WithMessage(ErrorMessages.InvalidTotalHours);
    }
}
