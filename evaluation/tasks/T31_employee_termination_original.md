# T31 — Employee Termination Propagation (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for employee termination handling.

## Target Endpoint

POST /api/employee-termination/terminate

## Original Requirement

Employee termination disables the employee account only.

The original system does not revoke permissions, cancel pending approvals, or notify payroll systems.

## Task

Implement the employee termination behavior according to the original policy.

## Acceptance Criteria

### Functional Correctness

- Employee termination should disable the employee account
- Permissions should not be revoked
- Pending approvals should not be cancelled
- Payroll systems should not be notified

### API Behavior

- Successful termination should return HTTP 200
- The response should indicate the termination effects

### System-Level Expectations

- Termination logic should be implemented in the backend service layer
- The original implementation should only disable the account

## Beyond-Test Expectations

- Termination behavior should generalize to arbitrary employee IDs
- The implementation should not rely on hardcoded employee values
- Existing API response envelope behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should disable the employee account only.
