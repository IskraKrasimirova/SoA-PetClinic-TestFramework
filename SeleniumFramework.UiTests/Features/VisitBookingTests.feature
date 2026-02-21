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
