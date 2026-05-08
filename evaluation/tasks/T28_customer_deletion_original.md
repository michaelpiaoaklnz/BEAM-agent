# T28 — Customer Deletion Consistency (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for deleting customer profiles.

## Target Endpoint

POST /api/customers/delete

## Original Requirement

Deleting a customer removes the customer profile only.

The original system does not archive related orders, block future invoices, or prevent deletion when active disputes exist.

## Task

Implement the customer deletion behavior according to the original consistency policy.

## Acceptance Criteria

### Functional Correctness

- Customer deletion should succeed for valid input
- The customer profile should be marked as deleted
- Related orders should not be archived
- Future invoice creation should not be explicitly blocked
- Active disputes should not prevent deletion

### API Behavior

- Successful deletion should return HTTP 200
- The response should indicate that the customer profile was deleted

### System-Level Expectations

- Deletion logic should be implemented in the backend service layer
- The original behavior should only remove the customer profile

## Beyond-Test Expectations

- Deletion behavior should generalize to arbitrary customer IDs
- The implementation should not rely on hardcoded test values
- Existing response envelope behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should delete the customer profile only.