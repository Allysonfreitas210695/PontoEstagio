

using CommonTestUltilities.Entities;
using FluentAssertions;

namespace Domain.Test.Entities;

public class PasswordRecovery
{
    [Fact]
    public void Success()
    {
        var course = PasswordRecoveryBuilder.Build();
        course.Should().NotBeNull();
    }
}