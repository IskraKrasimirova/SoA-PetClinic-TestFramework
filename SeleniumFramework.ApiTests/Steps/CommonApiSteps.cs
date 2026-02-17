using FluentAssertions;
using NUnit.Framework;
using Reqnroll;
using SeleniumFramework.ApiTests.Models.Dtos;

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
        var statusCode = _scenarioContext.Get<int>("StatusCode");

        Assert.That(statusCode, Is.EqualTo(expectedStatusCode));
    }

    [Then("the response should contain the following error message {string}")]
    public void ThenTheResponseShouldContainTheFollowingErrorMessage(string errorMessage)
    {
        var response = _scenarioContext.Get<string>("RawResponse");
        response.Should().Contain(errorMessage);
    }

    [Then("response should contain error messages")]
    public void ThenResponseShouldContainErrorMessages(Table table)
    {
        var expectedErrorMessages = table.Rows.Select(row => row["ErrorMessage"]).ToList();
        var actualErrorResponse = _scenarioContext.Get<ErrorsDto>("ErrorsResponse");
        
        actualErrorResponse.Message.Should().Contain(expectedErrorMessages);
    }
}