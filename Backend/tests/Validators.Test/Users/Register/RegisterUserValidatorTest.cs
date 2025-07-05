using CommonTestUltilities.Request;
using FluentAssertions;
using PontoEstagio.Communication.Enum;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace Validators.Test.Users.Register;

public class RegisterUserValidatorTest
{
    private readonly RegisterUserValidator _validator = new();

    [Fact]
    public void Success()
    {
        // Arrange
        var request = RequestRegisterUserJsonBuilder.Build();

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenNameIsEmpty()
    {
        // Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.InvalidUserName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validate_ShouldReturnError_WhenEmailIsEmpty(string invalidEmail)
    {
        // Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = invalidEmail;

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.EmailCannotBeEmpty);
    }

 
    [Fact]
    public void Validate_ShouldReturnError_WhenUniversityIdIsEmpty()
    {
        // Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        request.UniversityId = Guid.Empty;

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.InvalidUniversityId);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenCoordinatorHasEmptyCourseId()
    {
        // Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Type = UserType.Coordinator;
        request.CourseId = null;

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.InvalidCourseIdForUserType);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenInternHasEmptyRegistration()
    {
        // Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Type = UserType.Intern;
        request.Registration = string.Empty;

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.InvalidRegistration);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenInternHasEmptyPhone()
    {
        // Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Type = UserType.Intern;
        request.Phone = string.Empty;

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.PhoneIsRequired);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenInternHasLongPhone()
    {
        // Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Type = UserType.Intern;
        request.Phone = new string('1', 21); 

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.PhoneMaxLength);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenAdvisorHasEmptyCpf()
    {
        // Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Type = UserType.Advisor;
        request.CPF = string.Empty;

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.InvalidCpf);
    }

    [Theory]
    [InlineData("1234567890")]   
    [InlineData("123456789012")] 
    [InlineData("A2345678901")] 
    public void Validate_ShouldReturnError_WhenAdvisorHasInvalidCpfFormat(string invalidCpf)
    {
        // Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Type = UserType.Advisor;
        request.CPF = invalidCpf;

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.UserInvalidCpfFormat);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenAdvisorHasEmptyDepartment()
    {
        // Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Type = UserType.Advisor;
        request.Department = string.Empty;

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.InvalidDepartment);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenPasswordIsEmpty()
    {
        // Arrange
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = string.Empty;

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.InvalidPassword);
    }
}