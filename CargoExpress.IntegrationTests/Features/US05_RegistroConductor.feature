@wip
Feature: US05 - Registro de datos de conductor
    As a logistics management entrepreneur I want to register my drivers' data to assign them properly to each shipment

    @US05 @Conductor @HappyPath
    Scenario Outline: Register driver data successfully
        Given the entrepreneur wants to register driver data in the application
        When accesses the platform and selects the Registration section
        And selects the option to register a new driver
        And fills the required fields:
            | Field    | Value     |
            | Name     | <name>    |
            | DNI      | <dni>     |
            | License  | <license> |
            | Phone    | <phone>   |
        And clicks on "Register"
        Then the entered data will be validated
        And the driver data will be registered

        Examples:
            | name        | dni      | license    | phone     |
            | Juan Perez  | 12345678 | CD-123456  | 987654321 |
            | Maria Garcia| 87654321 | CD-654321  | 912345678 |

    @US05 @Conductor @ErrorValidacion
    Scenario: Register driver with duplicate DNI
        Given the entrepreneur wants to register driver data in the application
        And there is already a registered driver with DNI "12345678"
        When tries to register a new driver with the same DNI
        Then the driver will not be registered
        And a message indicating the driver already exists will be displayed

    @US05 @Conductor @ErrorFormato
    Scenario: Register driver with invalid phone format
        Given the entrepreneur wants to register driver data in the application
        When fills the fields with invalid phone format
            | Field  | Value      |
            | Phone | abcdefghij |
        Then the driver will not be registered
        And a message indicating the phone must have 9 digits will be displayed