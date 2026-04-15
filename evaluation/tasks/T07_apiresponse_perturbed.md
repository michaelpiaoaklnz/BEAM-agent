# T07 — API Response Behavior (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for updating customer contact details.

## Target Endpoint

POST /api/contacts/update

## Requirement Change

The API response contract has been updated.

## New Requirement

Validation failures must no longer return HTTP 200.

Instead:

- Validation failures must return HTTP 422 (Unprocessable Entity)
- The response body must include structured validation error details

## Task

Modify the backend system to enforce the updated validation failure response behavior.

## Acceptance Criteria

### Functional Correctness

- Valid requests should still succeed
- Invalid requests should fail validation

### API Behavior

- Validation failures must return HTTP 422
- Error responses must include structured validation error details
- Existing success behavior must remain unchanged

### System-Level Expectations

- Validation failure handling must be updated consistently in controller response logic
- Structured error information must be preserved for invalid inputs

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- The change must affect actual validation handling, not only response formatting
- All validation failures must consistently return HTTP 422
- Structured error information must remain available for unseen invalid inputs
- The implementation must not hardcode responses only for the tested payloads

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should pass all updated tests and correctly enforce the new API response behavior for validation failures.