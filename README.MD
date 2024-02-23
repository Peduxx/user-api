# User API documentation

## Description

This API will be responsible for managing the creation of a new user in the system.

## Illustrations

![User API basic flowchart.](https://i.imgur.com/ejGTRVn.png)

## Use cases

### Sign Up

[x] Check if user already exists </br>
[x] Save user in the database </br>
[ ] Send email to create password and confirm account </br>

### Sign In

[x] Check if user exists </br>
[x] Check password </br>
[x] Generate token </br>

### Password

[x] Generate salt </br>
[x] Encrypt password </br>
[x] Save password and salt in the database </br>
[x] Turn user active </br>
## Unit Tests

### Sign Up
 - SignUp_ReturnsSuccessResponse </br>
 - SignUp_Empty_Email_ReturnsFailureResponse </br>
 - SignUp_InvalidEmail_ReturnsFailureResponse </br>
 - SignUp_UserAlreadyExists_ReturnsFailureResponse </br>
 - SignUp_InvalidBirthDateBeforeThePossible_ReturnsFailureResponse </br>
 - SignUp_InvalidBirthDateAfterThePossible_ReturnsFailureResponse </br>

### Sign In
 - SignIn_ReturnsSuccessResponse </br>
 - SignUp_IncorrectEmailOrPassword_ReturnsFailureResponse </br>
 - SignUp_InactiveUser_ReturnsFailureResponse </br>

### Password
 - Password_Create_ReturnsSuccessResponse </br>
 - Password_TryEmptyValue_ReturnsFailureResponse


