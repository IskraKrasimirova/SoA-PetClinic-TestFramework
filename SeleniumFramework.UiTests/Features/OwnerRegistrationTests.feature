Feature: OwnerRegistrationTests

As a system user, I want to register an owner so that the system can maintain a record of clinic clients

Background:
	Given I am on the Home Page
	And I navigate to Find Owners page
	And I navigate to Add Owner page

@OwnerRegistration @E2E
Scenario: Verify user is able to register an owner successfully
	When I create a new owner with valid details
	Then the Owner Details page is displayed for the created owner
	And I search for the created owner by Last Name
	And the owner appears in the search results with correct details
