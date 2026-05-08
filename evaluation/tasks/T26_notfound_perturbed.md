# T26 — Structured Not-Found Error Response (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for looking up documents.

The original system returns generic error text for missing documents.

## Target Endpoint

POST /api/documents/lookup

## Revised Requirement

Not-found requests must return HTTP 404 with structured error code and trace ID.

Required response fields:

- errorCode
- traceId

The response should no longer use generic unstructured errors.

## Task

Update the document lookup error handling to support structured not-found responses.

## Acceptance Criteria

### Functional Correctness

- Missing documents should return HTTP 404
- The response should contain:
  - structured errorCode
  - traceId
- The response should preserve the existing API envelope structure where possible

### API Behavior

- Not-found requests should return HTTP 404
- The response should contain structured error metadata
- Generic error-only responses should no longer be used

### System-Level Expectations

- Structured error generation should be implemented consistently
- Trace IDs should be dynamically generated
- Existing endpoint route should remain stable

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Error handling should generalize to arbitrary invalid document IDs
- Trace IDs should not be hardcoded
- Structured error fields should consistently appear for not-found cases
- Existing unrelated endpoint behavior should remain functional

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should return HTTP 404 with structured error metadata for missing documents.