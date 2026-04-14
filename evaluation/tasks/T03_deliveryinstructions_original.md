# T03 — Delivery Instructions (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for order submission.

## Target Endpoint

POST /api/orders/submit

## Original Requirement

If `deliveryInstructions` is missing, it should default to `null`.

This applies regardless of whether the order contains fragile items.

## Expected Behavior

- An order without `deliveryInstructions` should still be accepted when the request is otherwise valid
- An order with `deliveryInstructions` should also be accepted
- Fragile and non-fragile orders should both succeed when `deliveryInstructions` is omitted

## Task

Implement the order submission behavior so that `deliveryInstructions` remains optional and defaults to `null` when omitted.

## Acceptance Criteria

### Functional Correctness

- A non-fragile order without `deliveryInstructions` should succeed
- A fragile order without `deliveryInstructions` should succeed
- An order with `deliveryInstructions` should succeed

### API Behavior

- Successful submission should return HTTP 200
- Response body should follow the existing API response structure

### System-Level Expectations

- The optional nature of `deliveryInstructions` should be reflected consistently in request handling and order submission logic
- No unnecessary validation rule should block valid orders

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy the following:

- `deliveryInstructions` must remain optional in backend logic, not only in test-specific handling
- Missing `deliveryInstructions` must not trigger hidden validation failures
- Fragile and non-fragile orders must be treated consistently under the original requirement
- The implementation must not include hardcoded logic tailored only to the provided tests

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should correctly accept orders with missing `deliveryInstructions` and pass all original tests.