@wip
Feature: US18 - Registro de usuario
    As a user I want to register in the application to have authorized and personalized access

    @US18 @Registro @HappyPath
    Scenario Outline: Successful user registration
        Given the user is on the "Register" section
        When fills the registration form with:
            | Field   | Value       |
            | Username| <username>  |
            | Password| <password>  |
        And the data is validated
        Then the account will be created successfully
        And will receive a confirmation email with a link to verify the account

        Examples:
            | username       | password   |
            | nuevoUsuario1  | Pass123!   |
            | usuarioEmpresario | SecurePass99 |

    @US18 @Registro @ErrorValidacion
    Scenario: Registration with invalid data
        Given the user is on the "Register" section
        When fills the registration form with incorrect information
            | Field   | Value |
            | Username| ab    |
            | Password| 123   |
        Then the account will not be created
        And an error message will be displayed indicating the error

    @US18 @Registro @ErrorDuplicado
    Scenario: Registration with duplicate email
        Given the user is on the "Register" section
        And there is already a registered user with email "test@example.com"
        When tries to register a new user with the same email
        Then the account will not be created
        And an error message will be displayed indicating the user already exists