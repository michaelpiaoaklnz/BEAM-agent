# T15 — Middle Name Handling (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for user account registration.

The original system allowed any optional middleName value.

## Target Endpoint

POST /api/accounts/register

## Revised Requirement

middleName remains optional.

However:

- middleName must be stored when provided
- middleName must be rejected if it contains numeric characters

## Task

Update the registration validation logic to enforce the revised middleName requirement.

## Acceptance Criteria

### Functional Correctness

- Requests without middleName should still succeed
- Requests with alphabetic middleName values should succeed
- Requests with numeric characters in middleName should fail
- Validation should reject mixed alphanumeric middleName values

### API Behavior

- Invalid middleName values should return validation failure responses
- Successful registration should still return HTTP 200

### System-Level Expectations

- Validation logic should be implemented consistently
- Existing registration functionality should remain stable
- The validation should apply to all registration requests

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Validation should detect numeric characters anywhere in middleName
- The implementation should not rely on hardcoded invalid examples
- The system should consistently enforce the revised requirement

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should reject registrations containing numeric middleName values while continuing to support valid optional middleName inputs.  