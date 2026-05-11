@wip
Feature: US20 - Cierre de sesión
    As a user I want to sign out securely at any time to guarantee the security and privacy of my data

    @US20 @Logout @HappyPath
    Scenario: Successful sign out
        Given the user has signed in to the application
        And is on any application screen
        When goes to the configuration section
        And selects the "Sign Out" option
        Then the account access will be closed
        And the "Sign In" section will be displayed again

    @US20 @Logout @Token
    Scenario: Sign out invalidates the token
        Given the user has signed in to the application
        And has a valid authentication token
        When signs out
        Then the authentication token will be invalidated
        And cannot make authenticated requests with the previous token