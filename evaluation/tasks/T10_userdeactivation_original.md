# T10 — User Deactivation (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for deactivating users.

## Target Endpoint

POST /api/users/deactivate

## Original Requirement

Deactivating a user blocks login only.

No additional cross-module actions are required.

## Task

Implement the original user deactivation behavior so that deactivated users can no longer log in.

## Acceptance Criteria

### Functional Correctness

- Deactivating a user should succeed
- The response should indicate that login is blocked

### API Behavior

- Successful deactivation should return HTTP 200
- The response should indicate the updated user state

### System-Level Expectations

- Deactivation logic should only block login under the original specification
- No role revocation or task reassignment is required

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- The deactivation logic must be enforced through backend logic
- The response must reflect the actual updated state
- The implementation must not rely on hardcoded test-specific values

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should correctly block login on user deactivation and pass all original tests.