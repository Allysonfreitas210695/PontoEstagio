using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PontoEstagio.Communication.Request;
using FluentAssertions;

namespace WebApi.Test.Controller.Auth;
public class LoginTest : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "api/auth/login";

    private readonly HttpClient _httpClient;

    private readonly string _email;
    private readonly string _name;
    private readonly string _password;

    public LoginTest(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
        _email = webApplicationFactory.GetEmail();
        _name = webApplicationFactory.GetName();
        _password = webApplicationFactory.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginUserJson
        {
            Email = _email,
            Password = _password
        };

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("name").GetString().Should().Be(_name);
        response.RootElement.GetProperty("accessToken").GetString().Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_InvalidCredentials()
    {
        var request = new RequestLoginUserJson
        {
            Email = _email,
            Password = "senha_incorreta"
        };

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessage").EnumerateArray();

        errors.Should().HaveCount(1);

    }
}
