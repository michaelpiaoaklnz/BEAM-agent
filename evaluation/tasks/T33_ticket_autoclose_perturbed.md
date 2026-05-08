# T33 — Ticket Auto-close Workflow Exception (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for evaluating automatic support ticket closure.

The original system auto-closes low-priority inactive tickets.

## Target Endpoint

POST /api/tickets/autoclose/evaluate

## Revised Requirement

Tickets with unresolved customer replies must not auto-close.

Tickets with pending escalation must also not auto-close.

The original inactivity rule should still apply only when no exception exists.

## Task

Update the ticket auto-close workflow to support exception-aware closure logic.

## Acceptance Criteria

### Functional Correctness

- Low-priority inactive tickets should auto-close when no exception exists
- Tickets with unresolved customer replies should not auto-close
- Tickets with pending escalation should not auto-close
- Tickets inactive for fewer than 7 days should not auto-close

### API Behavior

- Successful evaluations should return HTTP 200
- The response should indicate whether the ticket was auto-closed

### System-Level Expectations

- Exception-aware workflow logic should be implemented in the backend service layer
- Customer reply and escalation exceptions should override the inactivity rule
- Existing API route and response envelope should remain stable

## Beyond-Test Expectations

- Exception handling should generalize to arbitrary ticket IDs
- The implementation should not hardcode specific test cases
- The system should consistently prioritize exceptions over auto-close rules

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should not auto-close tickets with unresolved customer replies or pending escalations.