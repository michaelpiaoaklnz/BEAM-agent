# T22 — Support Ticket State Transition (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for applying support ticket state transitions.

## Target Endpoint

POST /api/tickets/state/transition

## Original Requirement

Support ticket moves from Open to Resolved to Closed.

The original system does not support reopening closed tickets.

## Task

Implement the support ticket state transition logic based on the original workflow.

## Acceptance Criteria

### Functional Correctness

- Open tickets should move to Resolved when the resolve action is applied
- Resolved tickets should move to Closed when the close action is applied
- Unsupported transitions should leave the ticket status unchanged

### API Behavior

- Successful transition evaluation should return HTTP 200
- The response should include the resulting ticket status

### System-Level Expectations

- State transition logic should be implemented in the backend service layer
- The Open to Resolved to Closed workflow should be consistently applied
- Reopening behavior should not be introduced in the original implementation

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- State transitions should not depend on hardcoded ticket IDs
- The system should consistently evaluate transitions for arbitrary valid inputs
- Existing response envelope behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should correctly support the Open to Resolved to Closed ticket workflow.