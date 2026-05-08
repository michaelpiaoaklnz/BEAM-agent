# T13 — Email Domain Registration Policy (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for user account registration.

The existing system currently accepts any valid email format.

## Target Endpoint

POST /api/accounts/register

## Revised Requirement

Registration must reject disposable email domains.

Registration must also require verified company domains.

Only approved company domains should be accepted.

## Task

Update the registration logic to enforce the revised email domain policy.

## Acceptance Criteria

### Functional Correctness

- Disposable email domains must be rejected
- Unverified public domains must be rejected
- Verified company domains should be accepted
- Invalid email formats should still fail validation

### API Behavior

- Successful registrations should return HTTP 200
- Failed registrations should return the existing failure response envelope
- Failure responses should clearly indicate the reason for rejection

### System-Level Expectations

- Domain validation logic should be implemented in the backend service layer
- Disposable and verified domain checks should be consistently enforced
- Existing registration behavior outside email-domain validation should remain stable

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- Domain checks must work for arbitrary email inputs
- The system should not rely on hardcoded test emails
- Validation should correctly extract and evaluate email domains
- The implementation should remain maintainable and extensible

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should reject disposable and unverified email domains while allowing verified company domains.