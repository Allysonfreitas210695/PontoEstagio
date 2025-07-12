

using CommonTestUltilities.Entities;
using FluentAssertions;

namespace Domain.Test.Entities;

public class EmailTemplates
{
    [Fact]
    public void Success()
    {
        var course = EmailTemplateBuilder.Build();
        course.Should().NotBeNull();
    }
}