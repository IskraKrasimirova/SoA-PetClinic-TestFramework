Feature: VisitBookingTests

As a system user, I want to add visits for pets so that the system can log visits and assign veterinarians.

Background:
	Given I am on the Home Page
	And I navigate to Find Owners page

@VisitBooking
Scenario: Verify a user is able to book a visit for a pet successfully
	Given I search for an owner with empty criteria
	And I select the first owner from the results
	And the Owner Details page is displayed for the selected owner
	When I navigate to Add Visit page for the first pet
	And I create a new visit with valid details
	Then the visit is diplayed in the Owner Details page


@VisitBooking @Validation
Scenario Outline: Verify a user cannot book a visit with invalid details
	Given I search for an owner with empty criteria
	And I select the first owner from the results
	And the Owner Details page is displayed for the selected owner
	When I navigate to Add Visit page for the first pet
	And I try to create a new visit with invalid details for "<field>" with "<value>"
	Then a proper error message "<message>" is shown for field "<field>"
Examples:
	| field       | value      | message           | #Description          | #Actual |
	| Description |            | must not be empty | Empty                 | Pass    |
	| Date        | 2020-12-12 | invalid date      | Past date             | Bug 11  |
	| Date        | 01-10-2020 | invalid date      | Not valid date format | Pass    |