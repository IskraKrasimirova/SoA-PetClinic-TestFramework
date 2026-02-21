Feature: PetRegistrationTests

As a system user, I want to add pets to owners so the system can associate each pet with its respective owner.

Background:
	Given I am on the Home Page
	And I navigate to Find Owners page

@PetRegistration @E2E
Scenario: Verify user is able to add a new pet to an owner successfully
	Given I navigate to Add Owner page
	And I create a new owner with valid details
	And the Owner Details page is displayed for the created owner
	When I navigate to Add New Pet page
	And I create a new pet with valid details
	Then the pet is displayed in the Owner Details page
