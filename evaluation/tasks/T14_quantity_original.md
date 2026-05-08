# T14 — Product Quantity Validation (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for creating product orders.

Each order includes a product category and requested quantity.

## Target Endpoint

POST /api/orders/create

## Original Requirement

Product quantity accepts any positive integer.

The system only validates that the quantity is greater than zero.

## Task

Implement the order quantity validation logic based on the original validation policy.

## Acceptance Criteria

### Functional Correctness

- Quantities greater than zero should be accepted
- Zero quantity should be rejected
- Negative quantities should be rejected

### API Behavior

- Successful order submissions should return HTTP 200
- Invalid quantity submissions should return the existing validation failure response envelope

### System-Level Expectations

- Quantity validation should be implemented consistently in the backend validation logic
- Validation should not depend on product category

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Validation should work for arbitrary positive integer inputs
- The implementation should not hardcode accepted quantity values
- Product category should not affect validation behavior

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should accept any positive integer quantity and reject zero or negative values.