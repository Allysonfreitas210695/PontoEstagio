using FluentAssertions;
using PontoEstagio.Application.UseCases.Company;
using PontoEstagio.Communication.Request;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace Validators.Test.Company.Register.RegisterCompanyValidatorTest;

public class RegisterCompanyValidatorTest
{
  
    [Fact]
    public void Success()
    {
        var _validator = new RegisterCompanyValidator();
        var request = RequestRegisterCompanyJsonBuilder.Build();
        var result = _validator.Validate(request);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var _validator = new RegisterCompanyValidator();

        var request = RequestRegisterCompanyJsonBuilder.Build();
        request.Name = string.Empty;

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.Company_Name_Required);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Short()
    {
        var _validator = new RegisterCompanyValidator();

        var request = RequestRegisterCompanyJsonBuilder.Build();
        request.Name = "AB";

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.Company_Name_MinLength);
    }

    [Fact]
    public void Should_Have_Error_When_CNPJ_Is_Empty()
    {
        var _validator = new RegisterCompanyValidator();

        var request = RequestRegisterCompanyJsonBuilder.Build();
        request.CNPJ = string.Empty;

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.Company_CNPJ_Required);
    }

    [Fact]
    public void Should_Have_Error_When_CNPJ_Is_Invalid_Format()
    {
        var _validator = new RegisterCompanyValidator();

        var request = RequestRegisterCompanyJsonBuilder.Build();
        request.CNPJ = "123456789";

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.Company_CNPJ_InvalidFormat);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var _validator = new RegisterCompanyValidator(); 

        var request = RequestRegisterCompanyJsonBuilder.Build();
        request.Email = string.Empty;

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.Company_Email_Required);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var _validator = new RegisterCompanyValidator();

        var request = RequestRegisterCompanyJsonBuilder.Build();
        request.Email = "invalid-email";

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.Company_Email_Invalid);
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Empty()
    {
        var _validator = new RegisterCompanyValidator();

        var request = RequestRegisterCompanyJsonBuilder.Build();
        request.Phone = string.Empty;

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.Company_Phone_Required);
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Has_Invalid_Format()
    {
        var _validator = new RegisterCompanyValidator();

        var request = RequestRegisterCompanyJsonBuilder.Build();
        request.Phone = "12345678";

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.Company_Phone_InvalidFormat);
    }
}
