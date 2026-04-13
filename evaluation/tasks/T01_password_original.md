# T01 — Password Validation (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for user registration.

## Target Endpoint

POST /api/accounts/register

## Current Behavior

The system currently enforces the following rules:

* Password must be at least 8 characters long
* ConfirmPassword must match Password
* Validation failures return HTTP 200 with an error response envelope

## Task

Ensure the registration endpoint behaves correctly according to the current specification.

## Acceptance Criteria

### Functional Correctness

* A valid request with password length >= 8 should succeed
* A request with password length < 8 should fail validation
* ConfirmPassword mismatch should fail validation

### API Behavior

* All responses (including validation failures) must return HTTP 200
* Validation errors must be returned in the existing response format

### Constraints

* Do not change API route
* Do not change response schema
* Do not introduce new validation rules
* Do not modify unrelated modules

## Expected Outcome

The system should pass all existing tests for the registration endpoint under the current specification.
