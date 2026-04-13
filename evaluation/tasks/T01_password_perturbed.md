# T01 — Password Validation (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for user registration.

## Target Endpoint

POST /api/accounts/register

## Requirement Change

The password policy has been updated with stricter security requirements:

### New Password Rules

* Minimum length: 12 characters
* Must contain:

  * at least one uppercase letter
  * at least one lowercase letter
  * at least one digit
  * at least one special character

### Password Reuse Rule

* The system must prevent reuse of recent passwords
* A mock implementation is acceptable (e.g., in-memory check)

## API Behavior Change

* Validation failures must return HTTP 422 (Unprocessable Entity)
* Response must include structured validation error details

## Task

Modify the backend system to enforce the updated password policy and response behavior.

## Acceptance Criteria

### Functional Correctness

* A valid complex password should succeed
* A password that does not meet complexity requirements should fail
* A password shorter than 12 characters should fail
* Reusing a recent password should fail

### API Behavior

* Validation failures must return HTTP 422
* Error responses must clearly indicate validation issues

### System-Level Expectations

* Changes should be applied consistently across:

  * Controller
  * Validation logic
  * Any related service or policy components

### Constraints

* Do not change API route
* Minimize unrelated code changes
* Maintain overall system structure

## Expected Outcome

The system should pass all updated tests that reflect the new password policy and response behavior.
