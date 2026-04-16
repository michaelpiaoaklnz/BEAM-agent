# T08 — Order Cancellation Consistency (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for cancelling orders.

## Target Endpoint

POST /api/orders/cancel

## Requirement Change

The cancellation behavior has been updated.

## New Requirement

Cancelling an order must no longer update only the order status.

It must also reverse related system effects.

Specifically, when an order is cancelled:

- The order status must be set to `Cancelled`
- Inventory reservation must be released
- Billing hold must be removed

## Task

Modify the backend system so that order cancellation updates the order status and reverses related system effects.

## Acceptance Criteria

### Functional Correctness

- Cancelling an order should still succeed
- The order status should become `Cancelled`
- Inventory reservation should be released
- Billing hold should be removed

### API Behavior

- Successful cancellations should return HTTP 200
- The response should clearly indicate the resulting cancellation state

### System-Level Expectations

- Cancellation must be handled consistently across the order module and related system-effect logic
- Reversal of related effects must be enforced in backend logic
- Partial cancellation behavior must not be treated as correct

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- The related system effects must be reversed through backend logic, not by hardcoded response shaping
- The implementation must coordinate state changes consistently across modules
- The solution must remain correct for unseen inputs consistent with the specification
- The implementation must not update only the visible order status while leaving related system effects unchanged

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should pass all updated tests and correctly enforce coordinated cancellation behavior across affected modules.