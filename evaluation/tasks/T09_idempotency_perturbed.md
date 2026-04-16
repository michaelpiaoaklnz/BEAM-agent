# T09 — Duplicate Request Handling (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for creating support cases.

## Target Endpoint

POST /api/cases/create

## Requirement Change

Duplicate requests must now be idempotent.

## New Requirement

If the same request is submitted more than once with the same idempotency key:

- The system must not create multiple records
- The system must return the original case result

If a different idempotency key is used:

- A new case may be created normally

## Task

Modify the backend system to enforce idempotent request handling for duplicate case creation requests.

## Acceptance Criteria

### Functional Correctness

- A valid request with a new idempotency key should create a case successfully
- Repeating the same request with the same idempotency key should not create a second case
- The repeated request should return the original case result
- Using a different idempotency key should allow a new case to be created

### API Behavior

- Successful requests should return HTTP 200
- Duplicate idempotent requests should still return a valid success response
- The response should clearly preserve the original created case result

### System-Level Expectations

- Idempotency handling must be implemented in backend logic
- Duplicate detection must not rely on test-specific hardcoded payloads
- The implementation must consistently honor the idempotency key

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- The same idempotency key must always map to the same previously created result
- The implementation must not accidentally treat all requests as duplicates
- Different idempotency keys must remain independent
- The solution must remain correct for unseen valid duplicate/non-duplicate combinations

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should pass all updated tests and correctly enforce idempotent case creation behavior.