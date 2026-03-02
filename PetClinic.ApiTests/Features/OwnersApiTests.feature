Feature: OwnersApiTests

Validate Owners API endpoints

@OwnersApi
Scenario: Get all owners returns a list of valid owners
	Given I make a get request to owners endpoint
	Then the response status code should be 200
	And the response content type should be "application/json"
	And owners response should contain non-empty list of owners
	And each owner in the list should have valid data


@OwnersApi @CreatePet @E2E @CleanupPet
Scenario: Create a pet for an existing owner and verify persistence
	Given I make a get request to owners endpoint
	And I select an existing owner from the response
	And I make a get request to pet types endpoint
	And I select an existing pet type from the response
	When I make a post request to create a pet for the selected owner with valid data
	Then the response status code should be 201
	And the response content type should be "application/json"
	And the created pet response should contain valid pet data
	And I make a get request to retrieve the created pet by its ID and owner ID
	And the response status code should be 200
	And the retrieved pet matches the created pet data


@OwnersApi @CreateOwner @CreatePet @E2E @CleanupPet @CleanupOwner
Scenario: Add a pet to a newly created owner and verify persistence
	Given I create a new owner successfully
	And I make a get request to pet types endpoint
	And I select an existing pet type from the response
	When I make a post request to create a pet for the newly created owner with valid data
	Then the response status code should be 201
	And the response content type should be "application/json"
	And the created pet response should contain valid pet data
	And I make a get request to retrieve the created pet by its ID and created owner ID
	And the response status code should be 200
	And the retrieved pet matches the created pet data


@OwnersApi @Validation
Scenario: Verify a user is not able to register an owner with missing mandatory fields
	Given I make a post request to owners endpoint with empty mandatory fields
	Then the response status code should be 400
	And response should contain error messages:
		| ErrorMessage                   | # Field                |
		| size must be between 1 and 30  | firstName and lastName |
		| must match                     | lastName               |
		| size must be between 1 and 255 | address                |
		| size must be between 1 and 80  | city                   |
		| size must be between 1 and 20  | telephone              |


@OwnersApi @Validation
Scenario Outline: Verify a user is not able to register an owner with invalid details
	Given I make a post request to owners endpoint with not valid details for "<field>" with "<value>"
	Then the response status code should be <statusCode>
	And the response should contain the following error message "<message>"
Examples:
	| field     | value                           | message                                | statusCode | #Description       |
	| Telephone |                                 | size must be between 1 and 20          |        400 | Empty              |
	| Telephone |                               1 | Phone number must be exactly 10 digits |        500 | 1 digit            |
	| Telephone |                          123456 | Phone number must be exactly 10 digits |        500 | 6 digits           |
	| Telephone |            12345678901234567890 | Phone number must be exactly 10 digits |        500 | 20 digits          |
	| Telephone |           123456789012345678901 | size must be between 1 and 20          |        400 | 21 digits          |
	| Telephone | AbcdePhone                      | must match                             |        400 | 10 letters         |
	| Telephone | !@#$%^-&*?                      | must match                             |        400 | special characters |
	| FirstName |                                 | size must be between 1 and 30          |        400 | Empty              |
	| FirstName | AAAAAAAAAAbbbbbbbbbbccccccccccd | size must be between 1 and 30          |        400 | 31 letter          |
	| FirstName | Mary‑Jane                       | must match                             |        400 | Hyphenated name    |
	| FirstName | Maria!$%                        | must match                             |        400 | special characters |
	| FirstName | Iskra123                        | must match                             |        400 | letters and digits |
	| LastName  |                                 | size must be between 1 and 30          |        400 | Empty              |
	| LastName  | AAAAAAAAAAbbbbbbbbbbccccccccccD | size must be between 1 and 30          |        400 | 31 letter          |
	| LastName  | Gray‑Stone                      | must match                             |        400 | Hyphenated name    |
	| LastName  | Smith#@!                        | must match                             |        400 | special characters |
	| LastName  | Davis123                        | must match                             |        400 | letters and digits |
	| City      |                                 | size must be between 1 and 80          |        400 | Empty              |
	| City      | LONG_81                         | size must be between 1 and 80          |        400 | 81 letter          |
	| Address   |                                 | size must be between 1 and 255         |        400 | Empty              |
	| Address   | LONG_256                        | size must be between 1 and 255         |        400 | 256 letter         |


# API allows Unicode letters due to \p{L} regex. UI restricts to English letters only.

@OwnersApi @Validation @CreateOwner @CleanupOwner
Scenario Outline: Verify a user is able to register an owner with specific details
	Given I make a post request to owners endpoint with valid specific details for "<field>" with "<value>"
	Then the response status code should be 201
	And the created owner persists with the provided details
Examples:
	| field     | value                          | #Description       |
	| FirstName | Искра                          | Cyrillic letters   |
	| FirstName | A                              | 1 letter           |
	| FirstName | AAAAAAAAAAbbbbbbbbbbcccccccccc | 30 letters         |
	| LastName  | Кръстева                       | Cyrillic letters   |
	| LastName  | a                              | 1 letter           |
	| LastName  | aaaaaaaaaabbbbbbbbbbcccccCCCCC | 30 letters         |
	| City      | Frankfurt‑Oder                 | Hyphenated city    |
	| City      | O'ConnorTown                   | Apostrophe         |
	| City      | София                          | Cyrillic letters   |
	| City      | Tokyo123                       | Letters and digits |
	| City      | Madrid!@#                      | Special characters |
	| City      | a                              | 1 letter           |
	| City      | LONG_80                        | 80 letters         |
	| Address   | b                              | 1 letter           |
	| Address   | LONG_255                       | 255 letters        |