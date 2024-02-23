# User API documentation

## Description

This API will be responsible for managing the creation of a new user in the system.

## Illustrations

![User API basic flowchart.](https://i.imgur.com/ejGTRVn.png)

## Use cases

### Sign Up

[x] Check if user already exists
[x] Save user in the database
[ ] Send email to create password and confirm account

### Sign In

[x] Check if user exists
[x] Check password
[x] Generate token

### Password

[x] Generate salt
[x] Encrypt password
[x] Save password and salt in the database
[x] Turn user active
## Unit Tests

### Sign Up
 - SignUp_ReturnsSuccessResponse
 - SignUp_Empty_Email_ReturnsFailureResponse
 - SignUp_InvalidEmail_ReturnsFailureResponse
 - SignUp_UserAlreadyExists_ReturnsFailureResponse
 - SignUp_InvalidBirthDateBeforeThePossible_ReturnsFailureResponse
 - SignUp_InvalidBirthDateAfterThePossible_ReturnsFailureResponse

### Sign In
 - SignIn_ReturnsSuccessResponse
 - SignUp_IncorrectEmailOrPassword_ReturnsFailureResponse
 - SignUp_InactiveUser_ReturnsFailureResponse

### Password
 - Password_Create_ReturnsSuccessResponse
 - Password_TryEmptyValue_ReturnsFailureResponse


