using CommonTestUltilities.Request;
using FluentAssertions;

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
        result.Errors.Should().Contain(e => e.ErrorMessage == "Name cannot be empty.");
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
        result.Errors.Should().Contain(e => e.ErrorMessage == "Email format is invalid.");
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
        result.Errors.Should().Contain(e => e.ErrorMessage == "Email cannot be empty.");
    }
}
