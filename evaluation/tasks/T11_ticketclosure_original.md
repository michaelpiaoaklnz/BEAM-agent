# T11 — Ticket Closure Workflow (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for closing support tickets.

## Target Endpoint

POST /api/tickets/close

## Original Requirement

Tickets auto-close after 7 days.

No special handling is required for priority tickets.

## Task

Implement the original ticket closure workflow so that tickets can be auto-closed after 7 days.

## Acceptance Criteria

### Functional Correctness

- Tickets older than 7 days should be eligible for auto-closure
- Valid closure requests should succeed

### API Behavior

- Successful closures should return HTTP 200
- The response should indicate that the ticket was closed

### System-Level Expectations

- Closure logic should be implemented in backend workflow logic
- No special conditional workflow is required for priority tickets under the original specification

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Closure decisions must be driven by backend logic, not test-specific shortcuts
- The response must reflect the actual closure result
- The implementation must remain consistent with the original workflow behavior

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should correctly auto-close eligible tickets and pass all original tests.