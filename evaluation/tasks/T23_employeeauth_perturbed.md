# T23 — Employee Profile Authorization (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for updating employee profiles.

The original system allows managers to edit employee profiles without restrictions.

## Target Endpoint

POST /api/employeeprofiles/update

## Revised Requirement

Managers may edit employee profiles only within their own department.

Salary fields must not be editable by managers.

Rules:

- Managers may update employees in the same department
- Managers may not update employees in other departments
- Salary-related updates must always be rejected

## Task

Update the authorization logic to support department-scoped profile editing and salary restrictions.

## Acceptance Criteria

### Functional Correctness

- Managers should be allowed to update employees within the same department
- Managers should not be allowed to update employees outside their department
- Salary-related updates should be rejected
- Existing successful update behavior should remain stable for valid cases

### API Behavior

- Successful updates should return HTTP 200
- Authorization failures should still preserve the API response envelope
- The response should indicate whether the update succeeded

### System-Level Expectations

- Authorization rules should be implemented in the backend service layer
- Department comparison should be consistently enforced
- Salary field restrictions should override department permissions

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Authorization decisions should be based on actual request values
- The implementation should not rely on hardcoded department names
- Field-level restrictions should generalize to arbitrary valid requests
- Existing API structure should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

Managers should only edit employees within their department and should never edit salary fields.