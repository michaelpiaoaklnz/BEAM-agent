# T16 — Emergency Contact Handling (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for user account registration.

The original system treated emergency contact information as optional for all employees.

## Target Endpoint

POST /api/accounts/register

## Revised Requirement

emergencyContact becomes mandatory for field staff.

For field staff, emergency contact information must include:

- phone number
- relationship

Non-field staff may still register without emergency contact details.

## Task

Update the registration validation logic to enforce the revised emergency contact requirement.

## Acceptance Criteria

### Functional Correctness

- Non-field staff should be allowed to register without emergency contact details
- Field staff should be rejected if emergency contact phone is missing
- Field staff should be rejected if emergency contact relationship is missing
- Field staff should be accepted when both emergency contact phone and relationship are provided

### API Behavior

- Successful registration should return HTTP 200
- Invalid field staff registration should return the existing validation failure response envelope
- Failure responses should indicate missing emergency contact information

### System-Level Expectations

- Validation logic should apply conditionally based on field staff status
- Existing registration functionality should remain stable
- Emergency contact validation should not apply to non-field staff

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Validation should consistently apply whenever isFieldStaff is true
- The implementation should not rely on hardcoded test users
- Both phone and relationship should be required for field staff
- Existing unrelated registration rules should remain unaffected

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should require complete emergency contact details for field staff while preserving optional behavior for non-field staff.