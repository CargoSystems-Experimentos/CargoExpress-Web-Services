@wip
Feature: TS03 - Seguridad y autenticación en nuestra API
    As a user I want the application to comply with security standards to protect my registered information

    @TS03 @Seguridad @SignUp
    Scenario: Register new user with security verification (sign-up)
        Given the user wants their data to be protected
        When the system receives a POST request with the new user's data to the API
            | Endpoint           | Method | Body                                              |
            | /api/v1/auth/sign-up| POST  | {"username": "newUser", "password": "SecurePass123"} |
        Then the authenticity Token will be verified
        And once verified, will provide a response to the request made

    @TS03 @Seguridad @SignIn
    Scenario: User login with security verification (sign-in)
        Given the user wants their data to be protected
        And has a registered account
        When the system receives a POST request with the user's login credentials to the API
            | Endpoint           | Method | Body                                    |
            | /api/v1/auth/sign-in| POST  | {"username": "user", "password": "pass"}|
        Then the authenticity token will be verified
        And once verified, the API responds with status code 200 and a valid authentication token if the credentials are correct

    @TS03 @Seguridad @Token
    Scenario: Access protected resource with valid token
        Given the user has logged in successfully
        And has a valid authentication token
        When accesses a protected resource of the API
        Then the API allows access to the resource

    @TS03 @Seguridad @TokenInvalido
    Scenario: Access protected resource with invalid token
        Given the user has an invalid authentication token
        When tries to access a protected resource of the API
        Then the API responds with status code 401
        And returns an authentication error message