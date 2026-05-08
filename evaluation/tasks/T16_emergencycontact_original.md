# T16 — Emergency Contact Handling (Original Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for user account registration.

## Target Endpoint

POST /api/accounts/register

## Original Requirement

emergencyContact is optional for all employees.

The system does not require emergency contact details during registration.

## Task

Implement the registration request model and processing behavior according to the original emergency contact requirement.

## Acceptance Criteria

### Functional Correctness

- Registration should succeed without emergency contact details
- Registration should succeed when emergency contact details are provided
- Field staff and non-field staff should both be allowed to register without emergency contact details

### API Behavior

- Successful registration should return HTTP 200
- The response should indicate successful registration

### System-Level Expectations

- Emergency contact information should remain optional
- Existing registration behavior must remain unchanged
- The system should not enforce staff-type-specific emergency contact validation

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Emergency contact handling should not break existing registration validation
- The system should consistently accept valid registration payloads with or without emergency contact data
- The implementation should not hardcode specific accepted users or staff types

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should successfully process registrations regardless of whether emergency contact details are provided.