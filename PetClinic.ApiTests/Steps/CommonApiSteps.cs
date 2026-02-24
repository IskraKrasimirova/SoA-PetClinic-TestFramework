using FluentAssertions;
using NUnit.Framework;
using PetClinic.ApiTests.Models.Dtos;
using PetClinic.ApiTests.Utils;
using Reqnroll;

namespace SeleniumFramework.ApiTests.Steps;

[Binding]
public class CommonApiSteps
{
    private readonly ScenarioContext _scenarioContext;

    public CommonApiSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Then("the response status code should be {int}"),
     Given("the response status code should be {int}")]
    public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
    {
        var statusCode = _scenarioContext.Get<int>(ContextConstants.StatusCode);

        Assert.That(statusCode, Is.EqualTo(expectedStatusCode));
    }

    [Then("the response should contain the following error message {string}")]
    public void ThenTheResponseShouldContainTheFollowingErrorMessage(string errorMessage)
    {
        var response = _scenarioContext.Get<string>(ContextConstants.RawResponse);
        response.Should().Contain(errorMessage);
    }

    [Then("response should contain error messages")]
    public void ThenResponseShouldContainErrorMessages(Table table)
    {
        var expectedErrorMessages = table.Rows.Select(row => row["ErrorMessage"]).ToList();
        var actualErrorResponse = _scenarioContext.Get<ErrorsDto>("ErrorsResponse");
        
        actualErrorResponse.Message.Should().Contain(expectedErrorMessages);
    }

    [Then("the response content type should be {string}")]
    public void ThenTheResponseContentTypeShouldBe(string expectedContentType)
    {
        var contentType = _scenarioContext.Get<string>(ContextConstants.ContentType);

        Assert.That(contentType, Is.EqualTo(expectedContentType));
    }
}