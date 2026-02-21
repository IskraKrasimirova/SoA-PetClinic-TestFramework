Feature: OwnersApiTests

Validate Owners API endpoints

@OwnersApi
Scenario: Get all owners returns a list of valid owners
	Given I make a get request to owners endpoint
	Then the response status code should be 200
	And the response content type should be "application/json"
	And owners response should contain non-empty list of owners
	And each owner in the list should have valid data


@OwnersApi @CreatePet @E2E
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