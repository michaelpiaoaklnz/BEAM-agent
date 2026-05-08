# T33 — Ticket Auto-close Workflow (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for evaluating automatic support ticket closure.

## Target Endpoint

POST /api/tickets/autoclose/evaluate

## Original Requirement

All low-priority tickets auto-close after inactivity.

A low-priority ticket should automatically close after 7 or more inactive days.

## Task

Implement the ticket auto-close workflow according to the original rule.

## Acceptance Criteria

### Functional Correctness

- Low-priority tickets inactive for 7 or more days should auto-close
- Low-priority tickets inactive for fewer than 7 days should remain open
- Non-low-priority tickets should not auto-close under this rule

### API Behavior

- Successful evaluations should return HTTP 200
- The response should indicate whether the ticket was auto-closed

### System-Level Expectations

- Auto-close logic should be implemented in the backend service layer
- The inactivity threshold should be consistently enforced

## Beyond-Test Expectations

- Auto-close decisions should depend on actual priority and inactivity values
- The implementation should not rely on hardcoded ticket IDs
- Existing response envelope behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should auto-close low-priority inactive tickets after 7 or more inactive days.