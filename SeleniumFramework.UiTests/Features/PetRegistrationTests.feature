Feature: PetRegistrationTests

As a system user, I want to add pets to owners so the system can associate each pet with its respective owner.

Background:
	Given I am on the Home Page
	And I navigate to Find Owners page

@PetRegistration @E2E
Scenario: Verify a user is able to add a new pet to an owner successfully
	Given I navigate to Add Owner page
	And I create a new owner with valid details
	And the Owner Details page is displayed for the created owner
	When I navigate to Add New Pet page
	And I create a new pet with valid details
	Then the pet is displayed in the Owner Details page


@PetRegistration @Validation
Scenario Outline: Verify a user is not able to add a pet with invalid details
	Given I am on the Add New Pet page for an existing owner
	When I try to create a new pet with invalid details for "<field>" with "<value>"
	Then an appropriate error message "<message>" is shown for field "<field>"
Examples:
	| field     | value      | message                                     | #Description       | #Actual |
	| Name      |            | is required                                 | Empty              | Pass    |
	| BirthDate |            | is required                                 | Empty              | Pass    |
	| Type      |            | is required                                 | Empty              | Pass    |
	| Name      | Tom2-#&!   | only English letters and digits are allowed | special characters | Bug 9   |
	| Name      | Том        | only English letters and digits are allowed | cyrilic letters    | Bug 9   |
	| BirthDate | 2050-12-12 | select valid date                           | Future date        | Bug 10  |


@PetRegistration @Validation
Scenario: User cannot register a pet with all mandatory fields empty
	Given I am on the Add New Pet page for an existing owner
	When I try to add a pet without filling all mandatory fields
	Then validation messages are displayed for all mandatory fields
