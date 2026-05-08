# T34 — Multi-Stage Purchase Approval Workflow (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for evaluating purchase order approvals.

The original system only requires manager approval.

## Target Endpoint

POST /api/purchase-approval/evaluate

## Revised Requirement

Purchase orders above NZD 5000 require both manager approval and finance approval.

Purchase orders at or below NZD 5000 continue to require manager approval only.

## Task

Update the purchase approval workflow to support multi-stage approval rules.

## Acceptance Criteria

### Functional Correctness

- Purchase orders at or below NZD 5000 should require manager approval only
- Purchase orders above NZD 5000 should require both manager approval and finance approval
- High-value orders missing finance approval should not be approved

### API Behavior

- Successful evaluations should return HTTP 200
- The response should indicate whether the purchase order was approved
- The response should indicate when finance approval is required

### System-Level Expectations

- Multi-stage approval logic should be implemented consistently
- Finance approval requirements should be based on actual purchase amount values
- Existing API route and response envelope should remain stable

## Beyond-Test Expectations

- Approval logic should generalize to arbitrary purchase amounts
- The implementation should not hardcode purchase IDs or test values
- The system should consistently enforce the high-value approval threshold

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should require finance approval for purchase orders above NZD 5000. 