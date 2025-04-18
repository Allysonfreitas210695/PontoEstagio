using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using PontoEstagio.Communication.Request;

namespace Validators.Test.Users;

public class PasswordValidatorTest
{

    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("Aaaaaaaa")]
    [InlineData("Aaaaaaaa1")]
    public void Error_Password_Invalid(string password)
    {
        var validator = new PasswordValidator<RequestRegisterUserJson>();

        var result = validator.IsValid(
            new FluentValidation.ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), 
            password
        );

        result.Should().BeFalse();
    }
}