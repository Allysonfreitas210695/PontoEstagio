using CommonTestUltilities.Entities;
using FluentAssertions;
using PontoEstagio.Domain.Enum;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace Domain.Test.Entities;

public class UserTest
{
    [Fact]
    public void Success()
    {
        // Arrange & Act
        var user = UserBuilder.Build();

        // Assert
        user.Should().NotBeNull();
    }

    [Fact]
    public void Activate_ShouldSetIsActiveTrue()
    {
        // Arrange
        var user = UserBuilder.Build();

        // Assert
        user.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Inactivate_ShouldSetIsActiveFalse()
    {
        // Arrange
        var user = UserBuilder.Build();
        var lastUpdate = user.UpdatedAt;

        // Act
        user.Inactivate();

        // Assert
        user.IsActive.Should().BeFalse();
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdateType_ShouldUpdateValue()
    {
        // Arrange
        var user = UserBuilder.Build(type: UserType.Intern);
        var newType = UserType.Supervisor;
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdateType(newType);

        // Assert
        user.Type.Should().Be(newType);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdateName_ShouldUpdateValue()
    {
        // Arrange
        var user = UserBuilder.Build();
        var newName = "Novo Nome";
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdateName(newName);

        // Assert
        user.Name.Should().Be(newName);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdateEmail_ShouldChangeEmail()
    {
        // Arrange
        var user = UserBuilder.Build();
        var newEmail = "teste@email.com";
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdateEmail(newEmail);

        // Assert
        user.Email.Endereco.Should().Be(newEmail);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdatePassword_ShouldChangePassword()
    {
        // Arrange
        var user = UserBuilder.Build();
        var newPassword = "novaSenha123";
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdatePassword(newPassword);

        // Assert
        user.Password.Should().Be(newPassword);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdateRegistration_ShouldChangeRegistration()
    {
        // Arrange
        var user = UserBuilder.Build();
        var newRegistration = "20250001";
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdateRegistration(newRegistration);

        // Assert
        user.Registration.Should().Be(newRegistration);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdateRegistration_WithEmptyValue_ShouldThrowException()
    {
        // Arrange
        var user = UserBuilder.Build();

        // Act
        var act = () => user.UpdateRegistration("");

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
           .Which.Errors.Should().Contain(ErrorMessages.InvalidRegistration);
    }

    [Fact]
    public void UpdateUniversityId_ShouldChangeUniversityId()
    {
        // Arrange
        var user = UserBuilder.Build();
        var newUniversityId = Guid.NewGuid();
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdateUniversityId(newUniversityId);

        // Assert
        user.UniversityId.Should().Be(newUniversityId);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdateUniversityId_WithEmptyGuid_ShouldThrowException()
    {
        // Arrange
        var user = UserBuilder.Build();

        // Act
        var act = () => user.UpdateUniversityId(Guid.Empty);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
           .Which.Errors.Should().Contain(ErrorMessages.InvalidUniversityId);
    }

    [Fact]
    public void UpdateCourseId_ShouldChangeCourseId()
    {
        // Arrange
        var user = UserBuilder.Build(type: UserType.Intern);
        var newCourseId = Guid.NewGuid();
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdateCourseId(newCourseId);

        // Assert
        user.CourseId.Should().Be(newCourseId);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Theory]
    [InlineData(UserType.Intern)]
    [InlineData(UserType.Coordinator)]
    [InlineData(UserType.Advisor)]
    public void UpdateCourseId_WithEmptyGuid_AndRestrictedType_ShouldThrowException(UserType type)
    {
        // Arrange
        var user = UserBuilder.Build(type: type);

        // Act
        var act = () => user.UpdateCourseId(Guid.Empty);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
           .Which.Errors.Should().Contain(ErrorMessages.InvalidCourseIdForUserType);
    }

    [Fact]
    public void UpdatePhone_ShouldChangePhone()
    {
        // Arrange
        var user = UserBuilder.Build();
        var newPhone = "11987654321";
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdatePhone(newPhone);

        // Assert
        user.Phone.Should().Be(newPhone);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdatePhone_WithEmptyValue_ShouldThrowException()
    {
        // Arrange
        var user = UserBuilder.Build();

        // Act
        var act = () => user.UpdatePhone("");

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
           .Which.Errors.Should().Contain(ErrorMessages.PhoneIsRequired);
    }

    [Fact]
    public void UpdatePhone_WithLongValue_ShouldThrowException()
    {
        // Arrange
        var user = UserBuilder.Build();
        var longPhone = new string('1', 21); // 21 characters

        // Act
        var act = () => user.UpdatePhone(longPhone);

        // Assert
        act.Should().Throw<ErrorOnValidationException>()
           .Which.Errors.Should().Contain(ErrorMessages.PhoneMaxLength);
    }

    [Fact]
    public void UpdateCpf_ForAdvisor_ShouldChangeCpf()
    {
        // Arrange
        var user = UserBuilder.Build(type: UserType.Advisor);
        var newCpf = "12345678901";
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdateCpf(newCpf);

        // Assert
        user.Cpf.Should().Be(newCpf);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdateCpf_ForNonAdvisor_ShouldNotChangeCpf()
    {
        // Arrange
        var originalCpf = "12345678901";
        var user = UserBuilder.Build(type: UserType.Intern, cpf: originalCpf);
        var newCpf = "98765432109";

        // Act
        user.UpdateCpf(newCpf);

        // Assert
        user.Cpf.Should().Be(originalCpf);
    }

    [Fact]
    public void UpdateDepartment_ForAdvisor_ShouldChangeDepartment()
    {
        // Arrange
        var user = UserBuilder.Build(type: UserType.Advisor);
        var newDepartment = "Novo Departamento";
        var lastUpdate = user.UpdatedAt;

        // Act
        user.UpdateDepartment(newDepartment);

        // Assert
        user.Department.Should().Be(newDepartment);
        user.UpdatedAt.Should().BeAfter(lastUpdate);
    }

    [Fact]
    public void UpdateDepartment_ForNonAdvisor_ShouldNotChangeDepartment()
    {
        // Arrange
        var originalDepartment = "TI";
        var user = UserBuilder.Build(type: UserType.Intern, department: originalDepartment);
        var newDepartment = "RH";

        // Act
        user.UpdateDepartment(newDepartment);

        // Assert
        user.Department.Should().Be(originalDepartment);
    }
}