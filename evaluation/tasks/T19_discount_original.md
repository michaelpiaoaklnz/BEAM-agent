# T19 — Discount Approval Logic (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for evaluating customer discounts.

## Target Endpoint

POST /api/discounts/evaluate

## Original Requirement

Discount is applied when order total exceeds NZD 1000.

Orders with total greater than NZD 1000 should receive a discount.

## Task

Implement the discount evaluation logic according to the original approval policy.

## Acceptance Criteria

### Functional Correctness

- Orders above NZD 1000 should receive a discount
- Orders equal to or below NZD 1000 should not receive a discount

### API Behavior

- Successful evaluations should return HTTP 200
- The response should indicate whether the discount was applied

### System-Level Expectations

- Discount logic should be implemented in the backend service layer
- The threshold rule should be consistently enforced

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Discount decisions should depend on actual order values
- The system should not hardcode specific test inputs
- Threshold evaluation should work consistently for arbitrary valid requests

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should apply discounts only for orders above NZD 1000.