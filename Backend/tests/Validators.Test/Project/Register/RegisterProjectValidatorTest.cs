using CommonTestUltilities.Request;
using FluentAssertions;
using PontoEstagio.Application.UseCases.Projects;
using PontoEstagio.Exceptions.ResourcesErrors;
using System;

namespace Validators.Test.Project.Register;

public class RegisterProjectValidatorTest
{
    [Fact]
    public void Sucess()
    {
        // ARRANGE
        var validator = new RegisterProjectValidator();
        var request = RequestRegisterProjectJsonBuilder.Build();

        // ACT
        var result = validator.Validate(request);

        // ASSERT
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_When_Name_Is_Empty()
    {
        // ARRANGE
        var validator = new RegisterProjectValidator();
        var request = RequestRegisterProjectJsonBuilder.Build();
        request.Name = string.Empty;

        // ACT
        var result = validator.Validate(request);

        // ASSERT
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == ErrorMessages.ProjectNameRequired);
    }

    [Fact]
    public void Error_When_Name_Is_Too_Short()
    {
        // ARRANGE
        var validator = new RegisterProjectValidator();
        var request = RequestRegisterProjectJsonBuilder.Build();
        request.Name = "ab"; 

        // ACT
        var result = validator.Validate(request);

        // ASSERT
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == ErrorMessages.InvalidProjectNameLength);
    }

    [Fact]
    public void Error_When_StartDate_Is_In_The_Past()
    {
        // ARRANGE
        var validator = new RegisterProjectValidator();
        var request = RequestRegisterProjectJsonBuilder.Build();
        request.StartDate = DateTime.UtcNow.AddDays(-1);

        // ACT
        var result = validator.Validate(request);

        // ASSERT
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == ErrorMessages.StartDateMustBeTodayOrFuture);
    }

    [Fact]
    public void Error_When_EndDate_Is_Before_Or_Equal_To_StartDate()
    {
        // ARRANGE
        var validator = new RegisterProjectValidator();
        var request = RequestRegisterProjectJsonBuilder.Build();
        request.StartDate = DateTime.UtcNow.AddDays(5);
        request.EndDate = request.StartDate; // mesma data → inválido

        // ACT
        var result = validator.Validate(request);

        // ASSERT
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == ErrorMessages.EndDateMustBeLaterThanStartDate);
    }

    [Fact]
    public void Error_When_TotalHours_Is_Less_Than_Or_Equal_To_Zero()
    {
        // ARRANGE
        var validator = new RegisterProjectValidator();
        var request = RequestRegisterProjectJsonBuilder.Build();
        request.TotalHours = 0; // Total de horas menor ou igual a zero

        // ACT
        var result = validator.Validate(request);

        // ASSERT
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == ErrorMessages.InvalidTotalHours);
    }

    [Fact]
    public void Sucess_When_EndDate_Is_Null()
    {
        // ARRANGE
        var validator = new RegisterProjectValidator();
        var request = RequestRegisterProjectJsonBuilder.Build();
        request.EndDate = null;

        // ACT
        var result = validator.Validate(request);

        // ASSERT
        result.IsValid.Should().BeTrue(); // end date é opcional
    }

    [Fact]
    public void Sucess_When_TotalHours_Is_Valid()
    {
        // ARRANGE
        var validator = new RegisterProjectValidator();
        var request = RequestRegisterProjectJsonBuilder.Build();
        request.TotalHours = 100; // Total de horas válido

        // ACT
        var result = validator.Validate(request);

        // ASSERT
        result.IsValid.Should().BeTrue();
    }
}
