# T02 — Supplier Tax Number (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for supplier onboarding.

## Target Endpoint

POST /api/suppliers/onboard

## Requirement Change

The supplier onboarding policy has been updated.

## New Requirement

For domestic suppliers:

* `taxNumber` is now mandatory
* `taxNumber` must satisfy format validation

For non-domestic suppliers:

* `taxNumber` remains optional

## Format Rule

A valid `taxNumber` must:

* Consist of exactly 9 digits
* Contain only numeric characters (0–9)

A mock implementation is acceptable, but validation must be enforced by backend logic.

## Task

Modify the backend system so that domestic suppliers must provide a valid `taxNumber`, while non-domestic suppliers may still omit it.

## Acceptance Criteria

### Functional Correctness

* A domestic supplier without `taxNumber` should fail
* A domestic supplier with an invalid `taxNumber` should fail
* A domestic supplier with a valid 9-digit `taxNumber` should succeed
* A non-domestic supplier without `taxNumber` should still succeed

### API Behavior

* Validation failures must return HTTP 422 (Unprocessable Entity)
* Error responses must clearly indicate the `taxNumber` validation issue

### System-Level Expectations

* Changes must be applied consistently across:

  * request validation
  * controller response behavior
  * onboarding service logic
* Domestic vs non-domestic classification must be respected throughout the system

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy all of the following:

* The domestic/non-domestic distinction must be enforced through backend logic, not only through test-specific conditions
* `taxNumber` validation must be conditionally enforced:

  * Required for domestic suppliers
  * Optional for non-domestic suppliers
* Non-domestic suppliers must not be incorrectly rejected due to the new domestic-only rule
* The onboarding service layer must remain consistent with validation rules (no divergence between controller and service logic)
* The solution must not rely on hardcoded values or inputs tailored only to the tests
* The implementation must remain correct for unseen valid and invalid inputs that follow the specification
* The requirement change must not be satisfied solely by modifying HTTP status codes; actual validation logic must be implemented

## Constraints

* Do not change the API route
* Minimize unrelated code changes
* Maintain overall system structure

## Expected Outcome

The system should pass all updated tests and correctly enforce the new domestic supplier tax number policy at the specification level.
