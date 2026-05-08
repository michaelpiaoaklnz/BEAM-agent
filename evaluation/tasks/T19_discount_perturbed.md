# T19 — Discount Approval Logic (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for evaluating customer discounts.

The original system applies discounts solely based on order total.

## Target Endpoint

POST /api/discounts/evaluate

## Revised Requirement

Discount eligibility depends on:

- customer tier
- order history
- restricted product categories

Rules:

- platinum customers always qualify if order total exceeds NZD 500
- gold customers qualify if order total exceeds NZD 800 and previousOrders >= 5
- standard customers qualify only if order total exceeds NZD 1200
- restricted product categories never qualify for discounts

Restricted categories:

- alcohol
- tobacco

## Task

Update the discount evaluation logic to support multi-condition discount rules.

## Acceptance Criteria

### Functional Correctness

- Platinum customers should qualify above NZD 500
- Gold customers should require both threshold and order history
- Standard customers should require NZD 1200 threshold
- Restricted categories should never receive discounts

### API Behavior

- Successful evaluations should return HTTP 200
- The response should indicate whether the discount was applied

### System-Level Expectations

- Multi-condition logic should be consistently enforced
- Restricted categories should override customer tier rules
- Existing API response structure should remain stable

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Logic should generalize to arbitrary valid inputs
- The implementation should avoid hardcoded test-only cases
- Thresholds and category restrictions should be consistently enforced

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should correctly apply role- and category-dependent discount rules.