using CommonTestUltilities.Request;
using FluentAssertions;
using PontoEstagio.Application.UseCases.Login.DoLogin;

namespace Validators.Test.Auth.Login;
    
public class LoginValidatorTest
{
    [Fact]
    public void Success()
    {
        //ARRANGE
        var validator = new LoginValidator();

        //ACT
        var request = RequestLoginUserJsonBuilder.Build();
        var result = validator.Validate(request);

        //ASSERT
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_DoLogin_EmailIsEmpty()
    {
        //ARRANGE
        var validator = new LoginValidator();

        //ACT
        var request = RequestLoginUserJsonBuilder.Build();
        request.Email = string.Empty;
        var result = validator.Validate(request);

        //ASSERT
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Error_DoLogin_Email_Invalid()
    {
        //ARRANGE
        var validator = new LoginValidator();

        //ACT
        var request = RequestLoginUserJsonBuilder.Build();
        request.Email = "invalid-Email";
        var result = validator.Validate(request);

        //ASSERT
        result.IsValid.Should().BeFalse();
    }
}
 