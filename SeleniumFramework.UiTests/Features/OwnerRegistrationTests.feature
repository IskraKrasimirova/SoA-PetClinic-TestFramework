Feature: OwnerRegistrationTests

As a system user, I want to register an owner so that the system can maintain a record of clinic clients

Background:
	Given I am on the Home Page
	And I navigate to Find Owners page
	And I navigate to Add Owner page

@OwnerRegistration @E2E
Scenario: Verify a user is able to register an owner successfully
	When I create a new owner with valid details
	Then the Owner Details page is displayed for the created owner
	And I search for the created owner by Last Name
	And the owner appears in the search results with correct details

@OwnerRegistration @Validation
Scenario Outline: Verify a user is not able to register an owner with invalid details
	When I try to create a new owner with invalid details for "<field>" with "<value>"
	Then an appropriate error message "<message>" is displayed for field "<field>"
Examples:
	| field     | value       | message                                                       | #Description       | #Actual      |
	| FirstName |             | must not be empty                                             | Empty              | Pass         |
	| LastName  |             | must not be empty                                             | Empty              | Pass         |
	| Address   |             | must not be empty                                             | Empty              | Pass         |
	| City      |             | must not be empty                                             | Empty              | Pass         |
	| Telephone |             | numeric value out of bounds (<10 digits>.<0 digits> expected) | Empty              | Pass (Bug)   |
	| FirstName | a           | min length 2 characters                                       | 1 letter           | Bug 4        |
	| LastName  | B           | min length 2 characters                                       | 1 letter           | Bug 4        |
	| City      | c           | min length 2 characters                                       | 1 letter           | Bug 4        |
	| FirstName | Ana-Maria#  | only English letters are allowed                              | special characters | Bug 1        |
	| LastName  | O'Conner    | only English letters are allowed                              | special characters | Bug 2        |
	| City      | @@!         | only English letters are allowed                              | special characters | Bug 3        |
	| FirstName | Искра       | only English letters are allowed                              | cyrilic letters    | Bug 1        |
	| LastName  |         123 | only English letters are allowed                              | digits             | Bug 2        |
	| City      | София       | only English letters are allowed                              | cyrilic letters    | Bug 3        |
	| City      | 123Sofia    | only English letters are allowed                              | digits             | Bug 3        |
	| Telephone | Abc         | numeric value out of bounds (<10 digits>.<0 digits> expected) | letters            | Pass (Bug 5) |
	| Telephone | !@#$%&*     | numeric value out of bounds (<10 digits>.<0 digits> expected) | special characters | Pass (Bug 5) |
	| Telephone | 11234567890 | numeric value out of bounds (<10 digits>.<0 digits> expected) | exceeds 10 digits  | Pass (Bug 6) |


#	Telephone mandatory field validation is inconsistent in the application.
#Sometimes shows "must not be empty", sometimes "numeric value out of bounds".
#Test accepts both as valid outcomes.
@OwnerRegistration @Validation
Scenario: Verify a user is not able to register an owner with missing mandatory fields
	When I try to create a new owner without filling all mandatory fields
	Then appropriate error messages are displayed for all mandatory fields