# T30 — Payment Callback Idempotency (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for processing payment provider callbacks.

The original system may process repeated callbacks multiple times.

## Target Endpoint

POST /api/payment-callbacks/process

## Revised Requirement

Payment callback processing must be idempotent using transaction reference.

Repeated callbacks for the same transaction reference must not duplicate ledger entries or customer notifications.

## Task

Update payment callback handling to enforce idempotent processing.

## Acceptance Criteria

### Functional Correctness

- First callback processing should mark the payment as Paid
- Duplicate callbacks should not create additional ledger entries
- Duplicate callbacks should not create additional customer notifications
- The response should indicate when a duplicate callback was ignored

### API Behavior

- Successful callback processing should return HTTP 200
- Duplicate callbacks should also return HTTP 200 but avoid duplicate side effects

### System-Level Expectations

- Idempotency should be based on transactionReference
- Ledger entry creation must not be duplicated
- Customer notification creation must not be duplicated

## Beyond-Test Expectations

- Idempotency logic should generalize to arbitrary transaction references
- The implementation should not hardcode transaction IDs
- Repeated callbacks should be safely handled without duplicate side effects
- Existing payment status behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should process payment callbacks idempotently using transaction reference.