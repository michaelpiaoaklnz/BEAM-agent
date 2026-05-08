# T32 — Product Deletion Propagation (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for product deletion handling.

## Target Endpoint

POST /api/product-deletion/delete

## Original Requirement

Product deletion hides the product from the catalogue only.

The original system does not block new orders or preserve historical order relationships.

## Task

Implement the product deletion behavior according to the original policy.

## Acceptance Criteria

### Functional Correctness

- Product deletion should remove the product from the catalogue
- New orders should not be explicitly blocked
- Existing order history preservation is not required

### API Behavior

- Successful deletion should return HTTP 200
- The response should indicate the deletion effects

### System-Level Expectations

- Product deletion logic should be implemented in the backend service layer
- The original implementation should only remove catalogue visibility

## Beyond-Test Expectations

- Deletion behavior should generalize to arbitrary product IDs
- The implementation should not rely on hardcoded product IDs
- Existing API response envelope behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should remove the product from the catalogue only.