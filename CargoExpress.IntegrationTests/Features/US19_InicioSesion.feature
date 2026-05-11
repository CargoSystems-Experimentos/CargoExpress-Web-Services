@wip
Feature: US19 - Inicio de sesión
    As a user I want to access my registered account to use the application features

    @US19 @Login @HappyPath
    Scenario Outline: Successful login with credentials
        Given the user is on the "Sign In" section
        And has a registered account with:
            | Username | Password  |
            | <user>  | <password>|
        When enters his credentials
        And the data is validated
        Then receives a welcome message
        And has access to the user view

        Examples:
            | user          | password |
            | usuarioValido | Pass123! |
            | entrepreneur1 | Secure99 |

    @US19 @Login @Error
    Scenario: Login with incorrect credentials
        Given the user is on the "Sign In" section
        When enters incorrect credentials
            | Username | Password |
            | wrongUser| wrongPass|
        And the data is invalidated
        Then access to the account is denied
        And an error message is displayed indicating the error

    @US19 @Login @Google
    Scenario: Successful login with Google
        Given the user is on the "Sign In" section
        And selects the option to sign in with Google
        When provides a valid Google token
        And the system validates the token with Google
        Then receives a welcome message
        And has access to the user view

    @US19 @Login @GoogleError
    Scenario: Login with Google with invalid token
        Given the user is on the "Sign In" section
        And selects the option to sign in with Google
        When provides an invalid Google token
        And the system cannot validate the token
        Then access to the account is denied
        And an error message about Google authentication is displayed