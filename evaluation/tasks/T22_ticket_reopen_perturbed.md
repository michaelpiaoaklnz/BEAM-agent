# T22 — Support Ticket Reopening State Transition (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for applying support ticket state transitions.

The original system supports Open to Resolved to Closed transitions.

## Target Endpoint

POST /api/tickets/state/transition

## Revised Requirement

Closed tickets may be reopened only within 48 hours.

When a closed ticket is reopened within the allowed window, its status must return to Open.

Reopening after 48 hours must be rejected or leave the ticket closed.

## Task

Update the ticket state transition logic to support conditional reopening.

## Acceptance Criteria

### Functional Correctness

- Closed tickets should reopen to Open when action is reopen and hoursSinceClosed <= 48
- Closed tickets should remain Closed when action is reopen and hoursSinceClosed > 48
- Existing Open to Resolved transition should still work
- Existing Resolved to Closed transition should still work

### API Behavior

- Successful transition evaluation should return HTTP 200
- The response should include the resulting ticket status
- Rejected or expired reopen attempts should not return Open status

### System-Level Expectations

- Conditional reopen logic should be implemented in the backend service layer
- The new reopen behavior should not break existing workflow transitions
- The implementation should consistently apply the 48-hour reopening window

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Reopen behavior should be based on actual hoursSinceClosed values
- The implementation should not rely on hardcoded test cases
- Existing state transitions should remain stable after introducing reopen logic
- Boundary value hoursSinceClosed = 48 should still allow reopening

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should support reopening closed tickets only within the 48-hour window.