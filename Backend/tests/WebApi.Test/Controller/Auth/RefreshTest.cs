

using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using PontoEstagio.Communication.Request;

namespace WebApi.Test.Controller.Auth;
public class RefreshTest : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "api/auth/refresh";

    private readonly HttpClient _httpClient;

    private readonly string _email;
    private readonly string _name;
    private readonly string _password;

    public RefreshTest(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
        _email = webApplicationFactory.GetEmail();
        _name = webApplicationFactory.GetName();
        _password = webApplicationFactory.GetPassword();
    }

    [Fact]
    public async Task RefreshToken_Success()
    {
        var loginRequest = new RequestLoginUserJson
        {
            Email = _email,
            Password = _password
        };

        var loginResponse = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
        loginResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var loginJson = await loginResponse.Content.ReadFromJsonAsync<JsonElement>();
        var refreshToken = loginJson.GetProperty("refreshToken").GetString();

        refreshToken.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Error_InvalidRefreshToken()
    {
        var request = new RequestRefreshTokenJson
        {
            RefreshToken = "refresh_token_invalido"
        };

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        var error = response.RootElement.GetProperty("errorMessage").GetString();

        error.Should().NotBeNullOrEmpty();
    }


}
