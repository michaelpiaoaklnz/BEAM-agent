# T08 — Order Cancellation Consistency (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for cancelling orders.

## Target Endpoint

POST /api/orders/cancel

## Original Requirement

Cancelling an order updates the order status only.

No additional system-side reversal behavior is required.

## Task

Implement the original order cancellation behavior so that cancellation updates the order status.

## Acceptance Criteria

### Functional Correctness

- Cancelling an order should succeed
- The order status should be updated to `Cancelled`

### API Behavior

- Successful cancellations should return HTTP 200
- The response should indicate the updated order status

### System-Level Expectations

- Cancellation logic should update only the order status
- No additional module coordination is required under the original specification

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- The cancellation logic must update status through backend logic, not hardcoded responses
- The updated status must be reflected consistently in the response
- The implementation must not rely on test-specific shortcuts

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should correctly update the order status to `Cancelled` and pass all original tests.