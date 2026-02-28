# 🐾 Pet Clinic — UI & API Automated Tests
Automated UI and API tests for Spring Pet Clinic, developed as part of the School of Automation Testing Endava – Final Exam.
The project validates core business functionalities through Selenium UI tests and REST API tests.


## 🚀 Environment Setup
### Start Web UI Application
```bash
docker run -p 8080:8080 springcommunity/spring-framework-petclinic
```
#### Start API Application
```bash
docker run -p 9966:9966 springcommunity/spring-petclinic-rest
```
Application URL: http://localhost:8080

Swagger UI: http://localhost:9966/petclinic/swagger-ui.html


## 📘 User Stories & Acceptance Criteria


### 1. Owner Registration

**User Story:**  
As a system user, I want to register an owner so that the system can maintain a record of clinic clients.

**Acceptance Criteria:**  
- All fields are mandatory.  
- Validation rules:  
  - First Name, Last Name, City: English alphabet only, minimum 2 characters.  
  - Address → letters, digits, special characters allowed  
  - Telephone → digits only, maximum 10 digits  
- On successful registration: The owner is created and displayed correctly in a table.  
- On failure: The system provides clear warning and error details.


### 2. Add Pet to Owner

**User Story:**  
As a system user, I want to add pets to owners so the system can associate each pet with its respective owner.

**Acceptance Criteria:**  
- The Add New Pet button in an owner’s information page opens the pet registration form.  
- The form displays the owner’s name.
- All fields are required.
- Only one pet type can be selected from the available list.
- Name may contain English letters and numbers.
- Birthdate cannot be in the future. 
- On success: 
	- The owner’s information page is updated with pet details.
- On failure:
	- The system displays a warning describing the issue.
- Multiple pets can be added for a single owner.


### 3. Book a Visit for a Pet

**User Story:**  
As a system user, I want to add visits for pets so that the system can log visits and assign veterinarians.

**Acceptance Criteria:**  
- Each pet on an owner’s information page has an Add Visit link.
- The link opens the visit booking form.
- The visit page displays pet information: name, birthdate, type, and owner name.
- All fields are mandatory.
- Date: Cannot be earlier than the current date.
- Description: Allows letters, numbers, and special characters.
- On success: 
	- The owner information page is updated with all visit details.
- On failure:
	- The system displays a warning describing the problem.
- A pet may have multiple visits recorded.


## 🔗 REST API Automation
### Task A — GET /api/owners

**Goal:** Validate the list of owners is returned correctly.

**Requirements (assert at minimum):**
- Status 200 and Content-Type: application/json.
- For at least one element, validate core fields are present and non-empty: 
	- id, 
	- firstName, 
	- lastName, 
	- address, 
	- city, 
	- telephone.


### Task B — POST /api/owners/{ownerId}/pets

**Goal:** Create a pet for an existing owner and verify persistence.

**Pre-steps:**
- From Task A’s response, pick a valid ownerId.
- Fetch a valid pet type via GET /api/pettypes to obtain type.id.
- Request Content-Type: application/json

**Requirements (assert at minimum):**
- Status 201 Created.
- Response JSON contains:
	- non-null id 
	- echoes name 
	- references a valid type.id. 


## 🛠️ Technologies & Frameworks
### UI Automation
- C# / .NET 8
- Selenium WebDriver
- NUnit
- ChromeDriver
- Page Object Model (POM)


### API Automation
- C# / .NET 8
- RestSharp
- NUnit
- Newtonsoft.Json


### Infrastructure
- Docker
- Swagger UI
- Spring Pet Clinic (UI & REST)
