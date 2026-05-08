# T25 — Resource Creation Response Contract (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for creating resources.

The original system returns the full created entity object.

## Target Endpoint

POST /api/resources/create

## Revised Requirement

Successful creation must return HTTP 201 with resource ID and location only.

The response should not return the full entity object.

## Task

Update the resource creation response behavior to satisfy the revised API contract.

## Acceptance Criteria

### Functional Correctness

- Resource creation should still succeed for valid input
- The response should include the created resource ID
- The response should include a location path for the created resource
- The response should not include the full resource entity

### API Behavior

- Successful creation should return HTTP 201
- The response should include resource ID and location only

### System-Level Expectations

- API contract changes should be implemented consistently
- Existing creation behavior should remain stable
- The endpoint route should remain unchanged

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Resource IDs should not be hardcoded
- Location should correspond to the generated resource ID
- The implementation should not leak unnecessary entity fields
- The response contract should generalize to arbitrary valid resource inputs

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should return HTTP 201 with only resource ID and location after successful creation.