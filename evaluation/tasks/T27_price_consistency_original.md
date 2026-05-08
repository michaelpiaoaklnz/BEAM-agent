# T27 — Product Price Update Consistency (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for updating product prices.

## Target Endpoint

POST /api/product-prices/update

## Original Requirement

Updating product price changes the product record only.

The original system does not create price history or invalidate cached price values.

## Task

Implement the product price update behavior according to the original consistency policy.

## Acceptance Criteria

### Functional Correctness

- Product price update should succeed for valid input
- The product record should be marked as updated
- Price history should not be created
- Cached price values should not be invalidated

### API Behavior

- Successful price update should return HTTP 200
- The response should indicate that the product price was updated

### System-Level Expectations

- Price update logic should be implemented in the backend service layer
- The system should only update the product record in the original behavior

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Price update behavior should generalize to arbitrary product IDs
- The system should not rely on hardcoded test values
- Existing API response envelope behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should update the product price record without creating price history or invalidating cache entries.