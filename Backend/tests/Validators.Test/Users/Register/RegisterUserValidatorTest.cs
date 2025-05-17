using CommonTestUltilities.Request;
using FluentAssertions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace Validators.Test.Users.Register;
public class RegisterUserValidatorTest
{
    [Fact]
    public void Sucess()
    {
        //ARRANGE
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();

        //ACT
        var result = validator.Validate(request);

        //ASSERT
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenNameIsEmpty()
    {
        // ARRANGE
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty; 
        // ACT
        var result = validator.Validate(request);
        // ASSERT
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.NameCannotBeEmpty);
    }
 
    [Fact]
    public void Validate_ShouldReturnError_WhenEmailIsInvalid()
    {
        // ARRANGE
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = "invalid-email";
        // ACT
        var result = validator.Validate(request);
        // ASSERT
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.InvalidEmailFormat);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenEmailIsEmpty()
    {
        // ARRANGE
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;
        // ACT
        var result = validator.Validate(request);
        // ASSERT
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.EmailCannotBeEmpty);
    }
}
