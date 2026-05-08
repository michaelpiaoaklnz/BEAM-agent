# T32 — Product Deletion Propagation (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for product deletion handling.

The original system only hides deleted products from the catalogue.

## Target Endpoint

POST /api/product-deletion/delete

## Revised Requirement

Product deletion must:

- prevent new orders
- preserve existing order history

Deleted products should no longer be purchasable, while historical order relationships remain intact.

## Task

Update product deletion handling to enforce cross-module propagation behavior.

## Acceptance Criteria

### Functional Correctness

- Product should still be removed from the catalogue
- New orders for deleted products should be blocked
- Existing order history should be preserved

### API Behavior

- Successful deletion should return HTTP 200
- The response should indicate all required propagation effects

### System-Level Expectations

- Cross-module propagation logic should be implemented consistently
- Existing order history preservation should not be omitted
- Existing API route and response envelope should remain stable

## Beyond-Test Expectations

- Propagation behavior should generalize to arbitrary product IDs
- The implementation should not hardcode product IDs or test cases
- The system should consistently represent all required deletion effects
- Existing catalogue removal behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should remove products from the catalogue, block new orders, and preserve historical order relationships.