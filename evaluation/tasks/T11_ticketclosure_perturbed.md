# T11 — Ticket Closure Workflow (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for closing support tickets.

## Target Endpoint

POST /api/tickets/close

## Requirement Change

The closure workflow has been updated.

## New Requirement

Tickets may still auto-close after 7 days, but priority tickets must require manual closure.

Specifically:

- Non-priority tickets older than 7 days may be auto-closed
- Priority tickets must not be auto-closed
- Priority tickets require manual closure

## Task

Modify the backend system to enforce the updated conditional workflow behavior.

## Acceptance Criteria

### Functional Correctness

- Non-priority tickets older than 7 days should still be auto-closed
- Priority tickets older than 7 days should not be auto-closed
- Manual closure of priority tickets should succeed

### API Behavior

- Successful closures should return HTTP 200
- Invalid auto-closure attempts for priority tickets should return HTTP 422
- Responses should clearly indicate the resulting workflow outcome

### System-Level Expectations

- The workflow must apply condition-based closure rules consistently
- Priority handling must be enforced in backend logic
- Existing non-priority closure behavior must remain correct

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- The priority exception must not be implemented through hardcoded test-specific shortcuts
- The implementation must correctly distinguish auto-closure from manual closure
- The workflow must remain correct for unseen valid and invalid combinations of ticket age, priority, and closure mode
- The solution must not preserve the original behavior for priority tickets by mistake

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should pass all updated tests and correctly enforce the revised conditional ticket closure workflow.