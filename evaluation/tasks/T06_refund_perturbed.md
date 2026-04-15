# T06 — Refund Authorization (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for approving refund requests.

## Target Endpoint

POST /api/refunds/approve

## Requirement Change

The refund authorization policy has been updated.

## New Authorization Rules

- Users with the role `Admin` can approve all refunds
- Users with the role `TeamLead` can approve refunds only when:
  - the refund amount is less than or equal to 1000
- All other roles must not be allowed to approve refunds

## Task

Modify the backend system to implement the updated authorization logic.

## Acceptance Criteria

### Functional Correctness

- Admin should be allowed to approve any refund
- TeamLead should be allowed to approve refunds with amount <= 1000
- TeamLead should not be allowed to approve refunds with amount > 1000
- Other roles should not be allowed to approve refunds

### API Behavior

- Successful approvals should return HTTP 200
- Unauthorized approvals should return HTTP 403

### System-Level Expectations

- Authorization decisions must be enforced consistently in backend logic
- Role and amount conditions must both be respected
- Existing Admin permissions must remain correct

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- The TeamLead rule must not be implemented through hardcoded test-specific shortcuts
- Both role and amount conditions must be enforced together
- Admin access must not be broken while adding TeamLead support
- The implementation must remain correct for unseen valid and invalid role/amount combinations

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should pass all updated tests and correctly enforce the revised refund authorization policy.