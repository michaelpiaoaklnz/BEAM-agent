# T03 — Delivery Instructions (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for order submission.

## Target Endpoint

POST /api/orders/submit

## Requirement Change

The order validation policy has changed.

## New Requirement

If an order contains fragile items, `deliveryInstructions` is now mandatory.

If an order does not contain fragile items, `deliveryInstructions` remains optional.

## Task

Modify the backend system so that fragile-item orders must provide `deliveryInstructions`, while non-fragile orders may still omit it.

## Acceptance Criteria

### Functional Correctness

- A fragile order without `deliveryInstructions` should fail
- A fragile order with `deliveryInstructions` should succeed
- A non-fragile order without `deliveryInstructions` should still succeed
- A non-fragile order with `deliveryInstructions` should succeed

### API Behavior

- Validation failures must return HTTP 422 (Unprocessable Entity)
- Error responses must clearly indicate the `deliveryInstructions` validation issue

### System-Level Expectations

- Changes must be applied consistently across:
  - request validation
  - controller response behavior
  - any related order submission service logic
- Fragile vs non-fragile classification must be respected throughout the system

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy all of the following:

- The fragile/non-fragile distinction must be enforced through backend logic, not only through test-specific branching
- `deliveryInstructions` must be conditionally required:
  - required for fragile-item orders
  - optional for non-fragile orders
- Non-fragile orders must not be incorrectly rejected due to the new fragile-only rule
- The order submission service layer must remain consistent with validation rules
- The implementation must not rely on hardcoded values or shortcuts tailored only to the current test inputs
- The requirement change must not be satisfied solely by changing HTTP status codes; actual conditional validation logic must be implemented
- The solution must remain correct for unseen inputs consistent with the specification

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should pass all updated tests and correctly enforce the new fragile-order delivery instruction policy at the specification level.