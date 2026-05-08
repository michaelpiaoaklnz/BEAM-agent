# T20 — Leave Request Auto-approval (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for submitting employee leave requests.

The original system auto-approves leave requests solely based on duration.

## Target Endpoint

POST /api/leave/submit

## Revised Requirement

Auto-approval depends on remaining leave balance and team staffing level.

Rules:

- Leave request must be below 2 days
- Remaining leave balance must be greater than or equal to daysRequested
- At least 2 team members must be available
- If any condition fails, the request must require manual review

## Task

Update the leave request approval logic to support multi-condition auto-approval.

## Acceptance Criteria

### Functional Correctness

- Short leave requests should auto-approve only when leave balance is sufficient
- Short leave requests should not auto-approve when balance is insufficient
- Short leave requests should not auto-approve when fewer than 2 team members are available
- Requests equal to or above 2 days should still require manual review

### API Behavior

- Successful submissions should return HTTP 200
- The response should indicate whether the leave request was auto-approved

### System-Level Expectations

- Multi-condition decision logic should be implemented in the backend service layer
- Existing API response structure should remain stable
- The system should not auto-approve requests when staffing or balance constraints fail

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Approval decisions should generalize to arbitrary valid input values
- All required conditions should be evaluated together
- The implementation should not rely on hardcoded test cases
- Existing unrelated leave request behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should auto-approve leave requests only when duration, balance, and staffing conditions are all satisfied.