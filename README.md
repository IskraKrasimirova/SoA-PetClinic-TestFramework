# 🐾 Pet Clinic — UI & API Automated Tests
Automated UI and API tests for Spring Pet Clinic, developed as part of the School of Automation Testing – Final Exam.
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
