# T17 — Empty Search Keyword Handling (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for searching records.

The original system returns all records when the keyword is empty.

## Target Endpoint

POST /api/search

## Revised Requirement

Empty search keyword must return HTTP 400 with a validation message.

The system should no longer treat missing, null, or empty keyword values as unfiltered searches.

## Task

Update the search input-handling behavior to reject empty search keywords.

## Acceptance Criteria

### Functional Correctness

- Missing keyword should be rejected
- Null keyword should be rejected
- Empty keyword should be rejected
- Whitespace-only keyword should be rejected
- Non-empty keyword should still return successful search results

### API Behavior

- Empty keyword validation failures should return HTTP 400
- The response should include a validation message explaining that keyword is required
- Successful non-empty searches should return HTTP 200

### System-Level Expectations

- Validation should be enforced before search results are returned
- The search service should not return all records for invalid empty input
- Existing successful search behavior for non-empty keywords should remain stable

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Validation should consistently reject null, empty, missing, and whitespace-only keywords
- The implementation should not rely on hardcoded test-specific values
- The API route and response envelope should remain consistent where applicable
- The system should avoid silently treating invalid input as a broad unfiltered query

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should reject empty search keywords with HTTP 400 while preserving successful search behavior for non-empty keywords.