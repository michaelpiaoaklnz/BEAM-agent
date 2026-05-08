# T13 — Email Domain Registration Policy (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for user account registration.

## Target Endpoint

POST /api/accounts/register

## Original Requirement

User registration accepts any valid email format.

The system only validates whether the email format is syntactically valid.

## Task

Implement the registration logic based on the original email validation policy.

## Acceptance Criteria

### Functional Correctness

- Any syntactically valid email address should be accepted
- Invalid email formats should be rejected

### API Behavior

- Successful registrations should return HTTP 200
- Failed validation should return the existing validation failure response envelope

### System-Level Expectations

- Email validation should rely on standard email format validation
- Registration logic should not restrict domains

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Validation must work consistently for arbitrary valid email inputs
- The system should not hardcode accepted email addresses
- Domain names should not affect registration success

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should accept any valid email format and reject malformed email inputs.