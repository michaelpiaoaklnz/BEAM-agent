# T28 — Customer Deletion Consistency (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for deleting customer profiles.

The original system deletes the customer profile only.

## Target Endpoint

POST /api/customers/delete

## Revised Requirement

Customer deletion must archive related orders and block future invoice creation.

Customer deletion must also be prevented when active disputes exist.

## Task

Update the customer deletion logic to enforce coordinated consistency behavior.

## Acceptance Criteria

### Functional Correctness

- Customer deletion should archive related orders when related orders exist
- Customer deletion should block future invoice creation
- Customer deletion should be blocked when active disputes exist
- Customer profile should only be deleted when deletion is not blocked

### API Behavior

- Successful deletion should return HTTP 200
- Blocked deletion should preserve the response envelope and indicate failure
- The response should indicate all required consistency effects

### System-Level Expectations

- Coordinated deletion logic should be implemented in the backend service layer
- Active disputes should override normal deletion behavior
- Related-order archiving and invoice blocking should not be omitted

## Beyond-Test Expectations

- Consistency behavior should generalize to arbitrary customer IDs
- The implementation should not hardcode customer IDs or specific test cases
- The system should not report full success unless required side effects are represented

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should archive related orders, block future invoices, and prevent deletion when active disputes exist.