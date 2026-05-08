# T34 — Purchase Approval Workflow (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for evaluating purchase order approvals.

## Target Endpoint

POST /api/purchase-approval/evaluate

## Original Requirement

Purchase orders are approved after manager review.

The original system does not require finance approval for high-value purchase orders.

## Task

Implement the purchase approval workflow according to the original approval policy.

## Acceptance Criteria

### Functional Correctness

- Purchase orders should be approved when manager approval is present
- Finance approval should not be required
- Approval decisions should depend on manager approval only

### API Behavior

- Successful evaluations should return HTTP 200
- The response should indicate whether the purchase order was approved

### System-Level Expectations

- Approval workflow logic should be implemented in the backend service layer
- The original implementation should only require manager approval

## Beyond-Test Expectations

- Approval behavior should generalize to arbitrary purchase order IDs
- The implementation should not rely on hardcoded purchase amounts
- Existing API response envelope behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should approve purchase orders based on manager approval only.