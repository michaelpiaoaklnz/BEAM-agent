# T36 — Failed Login Security Audit Logging (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for recording failed login attempts.

The original system logs failed login attempts only.

## Target Endpoint

POST /api/login-audit/record-failure

## Revised Requirement

Failed login logs must include:

- IP address
- device fingerprint
- lockout decision

Lockout decisions should depend on failed attempt thresholds.

## Task

Update failed login audit handling to support detailed security logging.

## Acceptance Criteria

### Functional Correctness

- Failed login attempts should still be recorded
- IP address should be logged
- Device fingerprint should be logged
- Lockout decision should be logged
- Lockout decisions should depend on failed attempt count

### API Behavior

- Successful audit recording should return HTTP 200
- The response should indicate all logged security metadata

### System-Level Expectations

- Security audit logic should be implemented consistently
- Lockout decisions should be based on actual failed attempt values
- Existing API route and response envelope should remain stable

## Beyond-Test Expectations

- Audit behavior should generalize to arbitrary usernames and IP addresses
- The implementation should not hardcode usernames or thresholds
- Security metadata logging should consistently apply
- Lockout decisions should not be random or static

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should record detailed failed login security metadata including IP address, device fingerprint, and lockout decisions.