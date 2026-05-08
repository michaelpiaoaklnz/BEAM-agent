# T18 — Search Pagination Defaults (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for searching records.

The original system uses a fixed default page size when pagination parameters are missing.

## Target Endpoint

POST /api/search

## Revised Requirement

Missing pagination parameters must use role-specific default limits.

If pageSize is omitted, the default page size depends on userRole:

- admin: 100
- manager: 50
- user: 20

If userRole is missing or unknown, the system should use the user default of 20.

## Task

Update the search pagination behavior to apply role-specific default page sizes.

## Acceptance Criteria

### Functional Correctness

- Missing page should still default to page 1
- Missing pageSize for admin should default to 100
- Missing pageSize for manager should default to 50
- Missing pageSize for user should default to 20
- Explicit pageSize should override role-specific defaults
- Unknown or missing userRole should default to 20

### API Behavior

- Successful searches should return HTTP 200
- The response should reflect the applied pagination values

### System-Level Expectations

- Role-specific pagination defaults should be handled consistently
- Existing search behavior outside pagination should remain stable
- The implementation should not break empty-search behavior from T17 original

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Role-specific defaults should work for arbitrary valid requests
- Explicit pageSize values should not be overwritten
- The implementation should not rely on hardcoded test-only payloads
- Unknown roles should safely fall back to the normal user default

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should apply role-specific default pagination limits when pageSize is missing.