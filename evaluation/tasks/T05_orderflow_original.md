# T05 — Order State Transition (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for transitioning order workflow states.

## Target Endpoint

POST /api/orders/transition

## Original Workflow

The order workflow is:

- Draft
- Submitted
- Approved
- Issued

The allowed transitions are:

- Draft → Submitted
- Submitted → Approved
- Approved → Issued

No other state transitions are allowed.

## Task

Implement the original order workflow transition logic.

## Acceptance Criteria

### Functional Correctness

- Draft → Submitted should succeed
- Submitted → Approved should succeed
- Approved → Issued should succeed
- Invalid transitions should fail

### API Behavior

- Successful transitions should return HTTP 200
- Invalid transitions should return HTTP 422
- The response should clearly indicate the resulting order status or transition error

### System-Level Expectations

- Transition rules should be enforced consistently in backend workflow logic
- The system should reject transitions that are not explicitly allowed

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- State transitions must be enforced by backend logic, not only by test-specific checks
- The workflow rules must remain consistent for all valid and invalid states
- The implementation must not rely on hardcoded inputs tailored to the provided tests

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should correctly enforce the original order workflow and pass all original tests.