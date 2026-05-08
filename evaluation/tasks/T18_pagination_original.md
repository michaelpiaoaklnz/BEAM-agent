# T18 — Search Pagination Defaults (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for searching records.

## Target Endpoint

POST /api/search

## Original Requirement

Missing pagination parameters use a default page size.

If page or pageSize is omitted, the system should apply default pagination behavior.

## Task

Implement the search pagination behavior according to the original pagination policy.

## Acceptance Criteria

### Functional Correctness

- Missing page should default to page 1
- Missing pageSize should default to 20
- Explicit pageSize should be respected
- Search results should be paginated consistently

### API Behavior

- Successful searches should return HTTP 200
- The response should indicate successful search completion
- The response should reflect the applied pagination values

### System-Level Expectations

- Pagination defaults should be handled in backend service logic
- Pagination behavior should not depend on user role
- Existing search behavior should remain stable

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Pagination should work for arbitrary valid page and pageSize values
- The implementation should not rely on hardcoded test-only inputs
- Missing pagination values should be handled consistently

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should apply a fixed default page size when pagination parameters are missing.