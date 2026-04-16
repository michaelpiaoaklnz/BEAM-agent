# T09 — Duplicate Request Handling (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for creating support cases.

## Target Endpoint

POST /api/cases/create

## Original Requirement

Duplicate requests may create multiple records.

No idempotency handling is required.

## Task

Implement the case creation endpoint according to the original behavior.

## Acceptance Criteria

### Functional Correctness

- A valid request should create a case successfully
- Repeating the same request may create multiple distinct cases

### API Behavior

- Successful requests should return HTTP 200
- The response should include the created case identifier

### System-Level Expectations

- Case creation should be handled through backend service logic
- Duplicate submissions are allowed to create separate records under the original requirement

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Case creation must not be hardcoded to specific test payloads
- Distinct requests must produce valid case creation results
- The implementation must remain consistent with the original non-idempotent behavior

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should correctly create cases and allow duplicate submissions to create multiple records under the original specification.