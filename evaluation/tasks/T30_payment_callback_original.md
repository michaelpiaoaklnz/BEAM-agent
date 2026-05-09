# T30 — Payment Callback Handling (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for processing payment provider callbacks.

## Target Endpoint

POST /api/payment-callbacks/process

## Original Requirement

Repeated payment callbacks may update payment status multiple times.

The original system does not enforce idempotency for repeated transaction references.

## Task

Implement the payment callback behavior according to the original callback handling policy.

## Acceptance Criteria

### Functional Correctness

- Payment callback processing should mark the payment as Paid
- Each callback should create a new ledger entry
- Each callback should create a new customer notification
- Duplicate callback detection is not required

### API Behavior

- Successful callback processing should return HTTP 200
- The response should indicate that the payment callback was processed

### System-Level Expectations

- Callback processing logic should be implemented in the backend service layer
- The original behavior should allow repeated processing side effects

## Beyond-Test Expectations

- Processing should generalize to arbitrary transaction references
- The implementation should not hardcode transaction IDs
- Existing response envelope behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should process each payment callback and create repeated side effects when callbacks are repeated.
