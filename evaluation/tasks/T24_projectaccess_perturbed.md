# T24 — Project Access Authorization (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for viewing project details.

The original system allows any authenticated user to access projects.

## Target Endpoint

POST /api/projects/view

## Revised Requirement

Only project members and administrators may access confidential project details.

Rules:

- Non-confidential projects remain accessible to authenticated users
- Confidential projects may only be accessed by:
  - project members
  - administrators
- Non-members without administrator role must be denied access

## Task

Update the authorization logic to support confidential project access restrictions.

## Acceptance Criteria

### Functional Correctness

- Project members should access confidential projects
- Administrators should access confidential projects
- Non-members should not access confidential projects
- Existing non-confidential project access behavior should remain stable

### API Behavior

- Successful access should return HTTP 200
- Authorization failures should preserve the API response envelope
- The response should indicate whether access was granted

### System-Level Expectations

- Authorization logic should be implemented consistently in the backend service layer
- Confidential project restrictions should override default authenticated access
- Existing API structure should remain stable

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Authorization decisions should be based on actual request values
- The implementation should not rely on hardcoded project IDs or user roles
- Membership and administrator logic should generalize to arbitrary valid requests
- Existing non-confidential access behavior should remain functional

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

Only project members and administrators should access confidential project details.