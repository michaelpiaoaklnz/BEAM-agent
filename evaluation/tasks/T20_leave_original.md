# T20 — Leave Request Auto-approval (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for submitting employee leave requests.

## Target Endpoint

POST /api/leave/submit

## Original Requirement

Leave requests below 2 days are auto-approved.

Leave requests equal to or above 2 days require manual review.

## Task

Implement the leave request approval logic based on the original approval policy.

## Acceptance Criteria

### Functional Correctness

- Leave requests with daysRequested < 2 should be auto-approved
- Leave requests with daysRequested >= 2 should not be auto-approved

### API Behavior

- Successful submissions should return HTTP 200
- The response should indicate whether the leave request was auto-approved

### System-Level Expectations

- Approval logic should be implemented in the backend service layer
- The system should consistently apply the daysRequested threshold rule

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Approval decisions must be based on the actual daysRequested value
- The threshold logic must be consistently applied for any valid input
- The system should not rely on test-specific values or shortcuts

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should correctly auto-approve leave requests below 2 days.