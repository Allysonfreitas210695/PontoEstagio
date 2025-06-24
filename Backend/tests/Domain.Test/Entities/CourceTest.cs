using CommonTestUltilities.Entities;
using FluentAssertions;
using PontoEstagio.Domain.Entities;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace Domain.Test.Entities;

public class CourseTest
{
    [Fact]
    public void Success()
    {
        var course = CourceBuilder.Build();
        course.Should().NotBeNull();
    }

    [Fact]
    public void Error_When_Name_Is_Empty()
    {
        var action = () => CourceBuilder.Build(name: "");

        action.Should().Throw<ErrorOnValidationException>()
            .Where(e => e.Errors.Contains(ErrorMessages.InvalidCourseName));
    }

    [Fact]
    public void Error_When_WorkloadHours_Is_Zero()
    {
        var action = () => CourceBuilder.Build(workloadHours: 0);

        action.Should().Throw<ErrorOnValidationException>()
            .Where(e => e.Errors.Contains(ErrorMessages.InvalidWorkloadHours));
    }

    [Fact]
    public void Error_When_UniversityId_Is_Empty()
    {
        var action = () => CourceBuilder.Build(universityId: Guid.Empty);

        action.Should().Throw<ErrorOnValidationException>()
            .Where(e => e.Errors.Contains(ErrorMessages.InvalidUniversityId));
    }

    [Fact]
    public void Error_When_UpdateName_With_Empty()
    {
        var course = CourceBuilder.Build();

        var action = () => course.UpdateName(" ");

        action.Should().Throw<ErrorOnValidationException>()
            .Where(e => e.Errors.Contains(ErrorMessages.InvalidCourseName));
    }

    [Fact]
    public void Error_When_UpdateWorkloadHours_With_Zero()
    {
        var course = CourceBuilder.Build();

        var action = () => course.UpdateWorkloadHours(0);

        action.Should().Throw<ErrorOnValidationException>()
            .Where(e => e.Errors.Contains(ErrorMessages.InvalidWorkloadHours));
    }

    [Fact]
    public void Error_When_UpdateUniversityId_With_Empty()
    {
        var course = CourceBuilder.Build();

        var action = () => course.UpdateUniversityId(Guid.Empty);

        action.Should().Throw<ErrorOnValidationException>()
            .Where(e => e.Errors.Contains(ErrorMessages.InvalidUniversityId));
    }

    [Fact]
    public void Success_When_UpdateName()
    {
        var course = CourceBuilder.Build();
        var newName = "Engenharia Civil";

        course.UpdateName(newName);

        course.Name.Should().Be(newName);
    }

    [Fact]
    public void Success_When_UpdateWorkloadHours()
    {
        var course = CourceBuilder.Build();
        var newWorkload = 180;

        course.UpdateWorkloadHours(newWorkload);

        course.WorkloadHours.Should().Be(newWorkload);
    }

    [Fact]
    public void Success_When_UpdateUniversityId()
    {
        var course = CourceBuilder.Build();
        var newUniversityId = Guid.NewGuid();

        course.UpdateUniversityId(newUniversityId);

        course.UniversityId.Should().Be(newUniversityId);
    }
}
