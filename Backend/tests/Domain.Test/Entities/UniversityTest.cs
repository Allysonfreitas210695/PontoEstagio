using CommonTestUltilities.Entities;
using FluentAssertions;

namespace Domain.Test.Entities;
public class UniversityTest
{
    [Fact]
    public void Success()
    {
        var course = UniversityBuilder.Build();
        course.Should().NotBeNull();
    }
}
