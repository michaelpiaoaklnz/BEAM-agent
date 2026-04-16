# T12 — Audit Logging (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for processing account updates.

## Target Endpoint

POST /api/audit/process

## Requirement Change

Audit logging requirements have been strengthened.

## New Requirement

Audit logs must now include full audit details without changing the existing schema.

In addition to user ID and timestamp, the audit information must include:

- action name
- entity type
- entity ID
- outcome

The implementation must preserve the existing response structure while enriching the audit details.

## Task

Modify the backend system so that audit processing returns full audit details.

## Acceptance Criteria

### Functional Correctness

- Valid requests should still succeed
- The audit information should include all required fields:
  - user ID
  - timestamp
  - action name
  - entity type
  - entity ID
  - outcome

### API Behavior

- Successful requests should return HTTP 200
- The response should preserve the existing contract while including enriched audit information

### System-Level Expectations

- Audit enrichment must be implemented in backend logic
- The implementation must not require schema changes
- The solution must remain correct for unseen valid requests

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Audit details must not be hardcoded to specific test payloads
- The enriched audit information must remain consistent with request content
- The implementation must preserve the original response structure while enhancing detail
- The solution must not silently omit any required audit field

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should pass all updated tests and correctly return full audit details without requiring schema changes.