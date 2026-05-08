# T25 — Resource Creation Response Contract (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for creating resources.

## Target Endpoint

POST /api/resources/create

## Original Requirement

Successful creation returns the full entity object.

## Task

Implement the resource creation response behavior according to the original API contract.

## Acceptance Criteria

### Functional Correctness

- Resource creation should succeed for valid input
- The response should include the created resource ID
- The response should include the submitted resource name and type

### API Behavior

- Successful creation should return HTTP 200
- The response should return the full created entity object

### System-Level Expectations

- Creation logic should be implemented in the backend service layer
- Existing API response envelope behavior should be preserved

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Resource IDs should not be hardcoded
- The response should reflect actual request values
- The implementation should not rely on test-specific inputs

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should return the full created resource entity after successful creation.