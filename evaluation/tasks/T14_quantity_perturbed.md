# T14 — Product Quantity Validation (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for creating product orders.

The existing system currently accepts any positive integer quantity.

## Target Endpoint

POST /api/orders/create

## Revised Requirement

Product quantity must respect category-specific minimum and maximum limits.

The allowed quantity range depends on the product category.

Example constraints:

- electronics: minimum 1, maximum 5
- office: minimum 1, maximum 100
- bulk: minimum 10, maximum 1000

## Task

Update the quantity validation logic to enforce category-specific quantity limits.

## Acceptance Criteria

### Functional Correctness

- Valid quantities within category limits should be accepted
- Quantities below category minimum should be rejected
- Quantities above category maximum should be rejected
- Unknown categories should return validation failure

### API Behavior

- Successful order submissions should return HTTP 200
- Invalid quantity submissions should return the existing validation failure response envelope
- Failure responses should indicate invalid quantity constraints

### System-Level Expectations

- Quantity validation rules should be implemented in backend validation or service logic
- Validation should consistently apply category-specific constraints
- Existing order submission behavior outside quantity validation should remain stable

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Validation must work for arbitrary valid and invalid quantity values
- The implementation should not rely on hardcoded test inputs only
- Category lookup and quantity validation should remain maintainable and extensible
- Validation behavior should remain consistent across all supported categories

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should correctly enforce category-specific quantity limits while preserving existing order submission behavior.