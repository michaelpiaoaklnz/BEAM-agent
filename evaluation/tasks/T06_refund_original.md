# T06 — Refund Authorization (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for approving refund requests.

## Target Endpoint

POST /api/refunds/approve

## Original Requirement

Only users with the role `Admin` are allowed to approve refunds.

## Task

Implement the refund approval logic with role-based authorization.

## Acceptance Criteria

### Functional Correctness

- Requests from Admin should succeed
- Requests from non-Admin users should fail

### API Behavior

- Successful approvals should return HTTP 200
- Unauthorized approvals should return HTTP 403

### System-Level Expectations

- Authorization must be enforced in backend logic
- Only the Admin role may approve refunds

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Authorization decisions must not rely on hardcoded test-specific inputs
- The Admin-only rule must be applied consistently for any valid request
- Other roles must not be accidentally granted refund approval permission

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should correctly enforce Admin-only refund approval and pass all original tests.