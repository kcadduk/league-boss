namespace LeagueBoss.Application.Tests.Results;

using System;
using System.Linq;
using LeagueBoss.Application.Results;

public class ResultTests
{
    [Fact]
    public void OkShould_SetIsSuccessToTrue_WhenCalled()
    {
        var result = Result.Ok();
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public void FailShould_SetIsFailureToTrue_WhenCalled()
    {
        var result = Result.Fail();
        
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public void ImplicitConversionShould_SetIsFailureAndException_WhenExceptionGiven()
    {
        var exception = new Exception();
        Result result = exception;
        
        result.IsFailure.Should().BeTrue();
        result.Errors.First().Should().BeSameAs(exception);
    }
}

public class ResultOfTUnitTests
{
    [Fact]
    public void OkShould_SetIsSuccessAndValue_WhenValueGiven()
    {
        var testValue = "test";
        var result = Result<string>.Ok(testValue);
        
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Value.Should().Be(testValue);
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public void FailShould_SetIsFailureToTrue_WhenCalled()
    {
        var result = Result<string>.Fail();
        
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public void ImplicitConversionShould_SetIsSuccessAndValue_WhenValueGiven()
    {
        var testValue = "test";
        Result<string> result = testValue;
        
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(testValue);
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public void ImplicitConversionShould_SetIsFailureAndException_WhenExceptionGiven()
    {
        var exception = new Exception();
        Result<string> result = exception;
        
        result.IsFailure.Should().BeTrue();
        result.Errors.First().Should().BeSameAs(exception);
    }    
}