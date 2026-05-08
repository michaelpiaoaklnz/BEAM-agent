# T21 — Invoice Payment State Transition (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for applying payments to invoices.

## Target Endpoint

POST /api/invoices/payment

## Original Requirement

Invoice status moves from Pending to Paid after payment is applied.

The original system does not distinguish between full and partial payment amounts.

## Task

Implement the invoice payment state transition according to the original invoice payment policy.

## Acceptance Criteria

### Functional Correctness

- Applying a payment should move the invoice status to Paid
- Payment amount should not create intermediate states
- The response should include the final invoice status

### API Behavior

- Successful payment application should return HTTP 200
- The response should indicate that the invoice payment was applied

### System-Level Expectations

- State transition logic should be implemented in the backend service layer
- The system should consistently move invoices to Paid after payment application

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- State transition should not depend on hardcoded invoice IDs
- The system should consistently return Paid for arbitrary valid payment inputs
- Existing API response envelope behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should move invoices to Paid after payment is applied.