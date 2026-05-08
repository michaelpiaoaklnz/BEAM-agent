# T36 — Failed Login Audit Logging (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for recording failed login attempts.

## Target Endpoint

POST /api/login-audit/record-failure

## Original Requirement

Failed login attempts are logged.

The original system does not require IP address logging, device fingerprint logging, or lockout decision tracking.

## Task

Implement failed login audit logging according to the original policy.

## Acceptance Criteria

### Functional Correctness

- Failed login attempts should be recorded
- IP address logging is not required
- Device fingerprint logging is not required
- Lockout decision logging is not required

### API Behavior

- Successful audit recording should return HTTP 200
- The response should indicate which audit fields were logged

### System-Level Expectations

- Audit logging logic should be implemented in the backend service layer
- The original implementation should log only basic failure events

## Beyond-Test Expectations

- Audit behavior should generalize to arbitrary usernames
- The implementation should not rely on hardcoded usernames
- Existing response envelope behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should record failed login attempts without detailed security metadata.