# T26 — Not-Found Error Response (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for looking up documents.

## Target Endpoint

POST /api/documents/lookup

## Original Requirement

Not-found requests return generic error text.

The original system does not use structured error codes or trace identifiers.

## Task

Implement the document lookup error behavior according to the original API contract.

## Acceptance Criteria

### Functional Correctness

- Lookup requests should return a failure response for missing documents
- The response should contain generic error text
- The system does not need structured error metadata

### API Behavior

- The endpoint should return HTTP 200
- The response should contain a generic failure message

### System-Level Expectations

- Lookup logic should be implemented in the backend service layer
- Existing response envelope structure should remain stable

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Failure responses should not depend on hardcoded document IDs
- Generic error behavior should remain consistent for arbitrary invalid requests
- Existing API route behavior should remain unchanged

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should return generic error text for missing documents.