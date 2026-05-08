# T27 — Product Price Update Consistency (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for updating product prices.

The original system updates the product price record only.

## Target Endpoint

POST /api/product-prices/update

## Revised Requirement

Price update must also create pricing history and invalidate cached price values.

The system must coordinate three effects:

- update the product price record
- create a price history entry
- invalidate cached price values

## Task

Update the product price handling logic to enforce the revised consistency requirement.

## Acceptance Criteria

### Functional Correctness

- Product price update should still succeed
- Product record should be updated
- A pricing history entry should be created
- Cached price values should be invalidated

### API Behavior

- Successful price update should return HTTP 200
- The response should indicate all required consistency effects

### System-Level Expectations

- Coordinated update logic should be implemented in the backend service layer
- Price history and cache invalidation should not be omitted
- Existing API route and response envelope should remain stable

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Consistency effects should occur for arbitrary valid product IDs
- The implementation should not hardcode product IDs or price values
- The system should not report success unless all required effects are represented
- Existing product update behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should update the product price, create price history, and invalidate cached price values.