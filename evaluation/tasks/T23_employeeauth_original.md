# T23 — Employee Profile Authorization (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for updating employee profiles.

## Target Endpoint

POST /api/employeeprofiles/update

## Original Requirement

Managers can edit employee profiles.

The original system does not restrict updates by department or field type.

## Task

Implement the employee profile update authorization logic according to the original policy.

## Acceptance Criteria

### Functional Correctness

- Managers should be allowed to update employee profiles
- The update operation should succeed regardless of department
- Salary-related updates should also succeed

### API Behavior

- Successful updates should return HTTP 200
- The response should indicate whether the profile was updated

### System-Level Expectations

- Authorization logic should be implemented in the backend service layer
- The system should consistently allow manager profile updates

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Authorization decisions should not depend on hardcoded employee IDs
- The system should consistently apply update permissions for arbitrary valid requests
- Existing response envelope behavior should remain stable

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

Managers should be able to update employee profiles without department restrictions.