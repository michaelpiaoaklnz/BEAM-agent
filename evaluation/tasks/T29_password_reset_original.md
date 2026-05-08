# T29 — Password Reset Token Handling (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for requesting password reset tokens.

## Target Endpoint

POST /api/password-reset/request

## Original Requirement

Multiple password reset requests create multiple reset tokens.

The original system does not invalidate previously issued reset tokens.

## Task

Implement the password reset request behavior according to the original token handling policy.

## Acceptance Criteria

### Functional Correctness

- A password reset request should create a new reset token
- Existing active tokens should remain valid
- The active token count should increase after a new reset request

### API Behavior

- Successful reset requests should return HTTP 200
- The response should include the newly created token identifier
- The response should indicate that old tokens were not invalidated

### System-Level Expectations

- Token creation logic should be implemented in the backend service layer
- The system should preserve the original behavior of allowing multiple active tokens

## Beyond-Test Expectations

- Token generation should not rely on hardcoded token IDs
- Behavior should generalize to arbitrary valid email inputs
- Existing API response envelope behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should create a new reset token while leaving existing reset tokens active.