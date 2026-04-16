# T10 — User Deactivation (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for deactivating users.

## Target Endpoint

POST /api/users/deactivate

## Requirement Change

User deactivation must no longer block login only.

## New Requirement

When a user is deactivated:

- Login must be blocked
- Assigned roles must be revoked
- Open tasks assigned to that user must be reassigned

## Task

Modify the backend system so that user deactivation updates all required dependent modules.

## Acceptance Criteria

### Functional Correctness

- Deactivation should still block login
- Roles should be revoked
- Tasks should be reassigned

### API Behavior

- Successful deactivation should return HTTP 200
- The response should indicate all resulting effects of deactivation

### System-Level Expectations

- The deactivation logic must coordinate changes across related modules
- Partial deactivation behavior must not be treated as correct
- The implementation must remain correct for unseen valid requests

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Cross-module effects must be implemented in backend logic, not through hardcoded response shaping
- Role revocation and task reassignment must remain consistent with the deactivated user state
- Existing login blocking behavior must remain correct
- The solution must not update only one dependent effect while leaving others unchanged

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should pass all updated tests and correctly enforce cross-module deactivation behavior.