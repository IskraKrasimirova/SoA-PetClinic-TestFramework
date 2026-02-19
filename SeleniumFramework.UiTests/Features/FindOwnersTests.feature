Feature: FindOwnersTests

A short summary of the feature

@smoke
Scenario: I can open the Find Owners page
	Given I am on the Home Page
	When I navigate to the "Find Owners" page
	Then the "Find Owners" title is displayed
