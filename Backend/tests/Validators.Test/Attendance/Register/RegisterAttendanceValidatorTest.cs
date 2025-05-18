using CommonTestUltilities.Request;
using FluentAssertions;
using PontoEstagio.Application.UseCases.Attendance;
using PontoEstagio.Exceptions.ResourcesErrors;

namespace Validators.Test.Attendance.Register;

public class RegisterAttendanceValidatorTest
{

    [Fact]
    public void Success()
    {
        var _validator = new RegisterAttendanceValidator();
        var request = RequestRegisterAttendanceJsonBuilder.Build(); 

        var result = _validator.Validate(request);

        result.IsValid.Should().BeTrue(); 
    }

    [Fact]
    public void Should_Return_Error_When_Date_Is_Empty()
    {
        var _validator = new RegisterAttendanceValidator();
        var request = RequestRegisterAttendanceJsonBuilder.Build();
        request.Date = default;

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .Which.ErrorMessage.Should().Be(ErrorMessages.DateIsRequired);
    }

    [Fact]
    public void Should_Return_Error_When_Date_Is_In_The_Future()
    {
        var _validator = new RegisterAttendanceValidator();

        var request = RequestRegisterAttendanceJsonBuilder.Build();
        request.Date = request.Date.AddDays(1);

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.InvalidAttendanceDate);
    }

    [Fact]
    public void Should_Return_Error_When_CheckIn_Is_Empty()
    {
        var _validator = new RegisterAttendanceValidator();

        var request = RequestRegisterAttendanceJsonBuilder.Build();
        request.CheckIn = default;

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.CheckInTimeIsRequired);
    }

    [Fact]
    public void Should_Return_Error_When_CheckIn_Is_Zero()
    {
        var _validator = new RegisterAttendanceValidator();

        var request = RequestRegisterAttendanceJsonBuilder.Build();
        request.CheckIn = TimeSpan.Zero;

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.CheckInTimeMustBeValid);
    }

    [Fact]
    public void Should_Return_Error_When_CheckOut_Is_Empty()
    {
        var _validator = new RegisterAttendanceValidator();

        var request = RequestRegisterAttendanceJsonBuilder.Build();
        request.CheckOut = default;

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.CheckOutTimeIsRequired);
    }

    [Fact]
    public void Should_Return_Error_When_CheckOut_Is_Zero()
    {
        var _validator = new RegisterAttendanceValidator();

        var request = RequestRegisterAttendanceJsonBuilder.Build();
        request.CheckOut = TimeSpan.Zero;

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.CheckOutTimeMustBeValid);
    }

    [Fact]
    public void Should_Return_Error_When_CheckOut_Is_Before_CheckIn()
    {
        var _validator = new RegisterAttendanceValidator();

        var request = RequestRegisterAttendanceJsonBuilder.Build();
        request.CheckIn = new TimeSpan(10, 0, 0);   // 10:00
        request.CheckOut = new TimeSpan(9, 0, 0);   // 09:00

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.CheckOutMustBeLaterThanCheckIn);
    }

    [Fact]
    public void Validate_ShouldFail_When_ProofImageBase64_IsEmpty()
    {
        var _validator = new RegisterAttendanceValidator();

        var request = RequestRegisterAttendanceJsonBuilder.Build();
        request.ProofImageBase64 = String.Empty;

        var result = _validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == ErrorMessages.InvalidProofImage);
    }
}
