# T02 — Supplier Tax Number (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for supplier onboarding.

## Target Endpoint

POST /api/suppliers/onboard

## Original Requirement

For domestic suppliers, `taxNumber` is optional.

## Expected Behavior

* A domestic supplier may be onboarded without a `taxNumber`
* A domestic supplier with a provided `taxNumber` should be accepted
* A non-domestic supplier may also omit `taxNumber`
* The onboarding flow should succeed when the request is otherwise valid

## Task

Implement the supplier onboarding behavior so that domestic suppliers are allowed to omit `taxNumber`.

## Acceptance Criteria

### Functional Correctness

* A domestic supplier without `taxNumber` should succeed
* A domestic supplier with `taxNumber` should succeed
* A non-domestic supplier without `taxNumber` should succeed

### API Behavior

* Successful onboarding should return HTTP 200
* Response body should follow the existing API response structure

### System-Level Expectations

* The implementation should be consistent across request validation and onboarding logic
* No unnecessary restrictions should be introduced on `taxNumber`
* The logic should not incorrectly differentiate between domestic and non-domestic suppliers

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy the following:

* The optional nature of `taxNumber` must be preserved in backend logic, not only in test-specific handling
* The onboarding service logic must not implicitly require `taxNumber`
* No hidden validation rules should reject valid requests without `taxNumber`
* The implementation must not include hardcoded conditions tailored only to test inputs

## Constraints

* Do not change the API route
* Minimize unrelated code changes
* Maintain overall system structure

## Expected Outcome

The system should correctly allow supplier onboarding without requiring `taxNumber`, and pass all original tests.
