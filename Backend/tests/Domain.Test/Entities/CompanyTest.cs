using CommonTestUltilities.Entities; 
using FluentAssertions;
using PontoEstagio.Exceptions.Exceptions;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace Domain.Test.Entities;

public class CompanyTest
{
    [Fact]
    public void Success()
    {
        // Arrange & Act
        var company = CompanyBuilder.Build();

        // Assert
        company.Should().NotBeNull();
        company.Name.Should().NotBeNullOrEmpty();
        company.CNPJ.Should().NotBeNullOrEmpty();
        company.Email.Should().NotBeNullOrEmpty();
        company.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Should_Throw_When_Name_Is_Null_Or_Empty()
    {
        // Act
        Action act = () => CompanyBuilder.Build(name: "");

        // Assert
        act.Should()
           .Throw<ErrorOnValidationException>()
           .Where(e => e.Errors.Contains(ErrorMessages.NameCannotBeEmpty));
    }

    [Fact]
    public void Should_Throw_When_CNPJ_Is_Null_Or_Empty()
    {
        // Act
        Action act = () => CompanyBuilder.Build(cnpj: "");

        // Assert
        act.Should()
           .Throw<ErrorOnValidationException>()
           .Where(e => e.Errors.Contains(ErrorMessages.CnpjCannotBeEmpty));
    }

    [Fact]
    public void Should_Throw_When_CNPJ_Is_Invalid()
    {
        // Act
        Action act = () => CompanyBuilder.Build(cnpj: "123456");

        // Assert
        act.Should()
           .Throw<ErrorOnValidationException>()
           .Where(e => e.Errors.Contains(ErrorMessages.InvalidCnpjFormat));
    }

    [Fact]
    public void Should_Throw_When_Email_Is_Null_Or_Empty()
    {
        // Act
        Action act = () => CompanyBuilder.Build(email: "");

        // Assert
        act.Should()
           .Throw<ErrorOnValidationException>()
           .Where(e => e.Errors.Contains(ErrorMessages.EmailCannotBeEmpty));
    }

    [Fact]
    public void Should_Throw_When_Email_Format_Is_Invalid()
    {
        // Act
        Action act = () => CompanyBuilder.Build(email: "invalid-email");

        // Assert
        act.Should()
           .Throw<ErrorOnValidationException>()
           .Where(e => e.Errors.Contains(ErrorMessages.InvalidEmailFormat));
    }
}
