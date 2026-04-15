# T04 — Expense Claim Approval (Perturbed Specification)

## System Context

You are working on an ASP.NET Core Web API backend.

The system includes an endpoint for submitting employee expense claims.

## Target Endpoint

POST /api/expenses/submit

## Requirement Change

The expense approval policy has been updated.

## New Approval Rules

Auto-approval is no longer based solely on the claim amount.

A claim is auto-approved only if ALL of the following conditions are met:

- Amount is below NZD 500
- The employee has no recent policy violations
- The claim category is not restricted

Restricted categories include:

- Travel
- Entertainment

If any of the above conditions are not satisfied:

- The claim must NOT be auto-approved
- It should be marked for manual review

## Task

Modify the backend system to implement the updated multi-condition approval logic.

## Acceptance Criteria

### Functional Correctness

- Claims below 500 with no violations and allowed category → auto-approved
- Claims below 500 with violations → NOT auto-approved
- Claims below 500 in restricted category → NOT auto-approved
- Claims >= 500 → NOT auto-approved

### API Behavior

- Successful submissions should return HTTP 200
- The response should clearly indicate approval status

### System-Level Expectations

- The approval decision must be based on all conditions together
- Logic must be implemented consistently in the service layer
- The system must correctly combine multiple conditions

## Beyond-Test Expectations

In addition to passing the provided tests, the implementation must also satisfy:

- All approval conditions must be enforced (no partial implementation)
- The system must not rely on hardcoded values tailored to test cases
- Employee violation status and category restrictions must be respected for all inputs
- The decision logic must remain correct for unseen combinations of inputs
- The implementation must not bypass logic by only modifying response outputs

## Constraints

- Do not change the API route
- Minimize unrelated code changes
- Maintain overall system structure

## Expected Outcome

The system should pass all updated tests and correctly enforce the new multi-condition approval logic.