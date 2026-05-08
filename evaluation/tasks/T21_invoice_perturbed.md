# T21 — Invoice Payment State Transition (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for applying payments to invoices.

The original system moves invoices directly from Pending to Paid.

## Target Endpoint

POST /api/invoices/payment

## Revised Requirement

Add a Partially Paid state when payment amount is below invoice total.

State transition rules:

- If paymentAmount is less than invoiceTotal, status should become Partially Paid
- If paymentAmount is equal to or greater than invoiceTotal, status should become Paid

## Task

Update the invoice payment state transition logic to distinguish full and partial payments.

## Acceptance Criteria

### Functional Correctness

- Payment below invoice total should produce Partially Paid status
- Payment equal to invoice total should produce Paid status
- Payment greater than invoice total should produce Paid status
- The response should include the correct final status

### API Behavior

- Successful payment application should return HTTP 200
- The response should preserve the existing response envelope

### System-Level Expectations

- State transition logic should be implemented consistently in the backend service layer
- The new Partially Paid state should not break existing full payment behavior
- Existing API route and response structure should remain stable

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Status should be calculated from actual invoiceTotal and paymentAmount values
- The implementation should not rely on hardcoded test values
- Partial payment handling should generalize to arbitrary valid invoice totals and payment amounts
- Full and overpayments should continue to produce Paid status

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should return Partially Paid for partial payments and Paid for full or overpayments.