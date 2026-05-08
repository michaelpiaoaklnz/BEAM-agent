# T29 — Password Reset Token Idempotency (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for requesting password reset tokens.

The original system allows multiple active reset tokens.

## Target Endpoint

POST /api/password-reset/request

## Revised Requirement

Only the latest reset token remains valid.

Older reset tokens must be invalidated when a new reset request is created.

## Task

Update the password reset logic to enforce latest-token-only behavior.

## Acceptance Criteria

### Functional Correctness

- A new reset request should still create a new reset token
- Previously active tokens should be invalidated
- After a new request, only one active token should remain
- The response should indicate that old tokens were invalidated

### API Behavior

- Successful reset requests should return HTTP 200
- The response should include the latest token identifier
- The response should indicate token invalidation behavior

### System-Level Expectations

- Token invalidation should be implemented consistently in the backend service layer
- The system should not allow multiple active reset tokens for the same user
- Existing API route and response envelope should remain stable

## Beyond-Test Expectations

- Token invalidation should generalize to arbitrary valid email inputs
- The implementation should not hardcode token IDs or test emails
- The system should consistently represent latest-token-only behavior

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should create a new reset token and invalidate all previously active reset tokens.  