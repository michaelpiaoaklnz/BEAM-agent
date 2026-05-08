# T35 — Detailed Profile Update Audit Logging (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for recording profile update audit events.

The original system logs only user ID and timestamp.

## Target Endpoint

POST /api/profile-audit/record

## Revised Requirement

Profile updates must log:

- before/after values
- acting user role
- request source
- correlation ID

Sensitive fields must not expose raw values.

## Task

Update profile update audit logging to record detailed audit information while avoiding exposure of sensitive fields.

## Acceptance Criteria

### Functional Correctness

- Before/after values should be logged for non-sensitive fields
- Acting user role should be logged
- Request source should be logged
- Correlation ID should be included
- Sensitive fields should not expose raw before/after values

### API Behavior

- Successful audit recording should return HTTP 200
- The response should indicate which audit details were logged

### System-Level Expectations

- Detailed audit behavior should be implemented consistently
- Sensitive field masking should override normal before/after logging
- Existing API route and response envelope should remain stable

## Beyond-Test Expectations

- Audit behavior should generalize to arbitrary field names and users
- The implementation should not hardcode specific user IDs
- Sensitive-field masking should consistently apply to salary and password fields
- Correlation IDs should not be hardcoded

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should record detailed profile audit information while masking sensitive field values.