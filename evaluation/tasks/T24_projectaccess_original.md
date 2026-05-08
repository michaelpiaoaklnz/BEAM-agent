# T24 — Project Access Authorization (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for viewing project details.

## Target Endpoint

POST /api/projects/view

## Original Requirement

Any authenticated user can view project details.

The original system does not distinguish confidential projects or project membership.

## Task

Implement the project access authorization logic according to the original policy.

## Acceptance Criteria

### Functional Correctness

- Any authenticated request should be allowed to access project details
- Confidential project status should not affect access
- Project membership should not affect access

### API Behavior

- Successful access should return HTTP 200
- The response should indicate whether access was granted

### System-Level Expectations

- Authorization logic should be implemented in the backend service layer
- The system should consistently allow authenticated project access

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Authorization decisions should not depend on hardcoded project IDs
- The system should consistently apply access rules for arbitrary valid requests
- Existing API response envelope behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

Authenticated users should be able to access project details without membership restrictions.