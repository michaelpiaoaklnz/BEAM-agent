# T05 — Order State Transition (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for transitioning order workflow states.

## Target Endpoint

POST /api/orders/transition

## Requirement Change

The order workflow has been updated to introduce a new intermediate state: `On Hold`.

## Updated Workflow

The workflow states are now:

- Draft
- Submitted
- On Hold
- Approved
- Issued

The system must support the following transitions:

- Draft → Submitted
- Submitted → Approved
- Submitted → On Hold
- On Hold → Approved
- Approved → Issued

## Conditional Rule

An order may transition from `Submitted` to `On Hold` only when `requiresReview` is true.

If `requiresReview` is false:

- Submitted → On Hold must be rejected

## Task

Modify the backend system to support the new `On Hold` state and conditional transition rules.

## Acceptance Criteria

### Functional Correctness

- Draft → Submitted should succeed
- Submitted → Approved should succeed
- Submitted → On Hold should succeed only when `requiresReview` is true
- Submitted → On Hold should fail when `requiresReview` is false
- On Hold → Approved should succeed
- Approved → Issued should succeed
- Invalid transitions should fail

### API Behavior

- Successful transitions should return HTTP 200
- Invalid transitions should return HTTP 422
- Error responses should clearly indicate why the transition is invalid

### System-Level Expectations

- The new `On Hold` state must be integrated consistently into workflow logic
- Conditional transition rules must be enforced in backend logic
- Existing valid transitions must continue to work correctly

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy all of the following:

- The `On Hold` transition must not be implemented through hardcoded test-specific shortcuts
- The `requiresReview` condition must be evaluated as part of the actual transition logic
- Existing transitions must not be broken when adding the new state
- Invalid transitions involving `On Hold` must still be rejected
- The implementation must remain correct for unseen valid and invalid state combinations consistent with the specification

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should pass all updated tests and correctly enforce the revised workflow with the new `On Hold` state.