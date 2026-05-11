@wip
Feature: US03 - Registro de gastos de viaje
    As a logistics management entrepreneur I want to record the expenses incurred during trips to maintain accurate records and keep my clients informed about the costs associated with their services

    @US03 @Gastos @HappyPath
    Scenario Outline: Register trip expenses successfully
        Given the entrepreneur wants to register trip expenses in the application
        And there is a registered trip with ID <tripId>
        When accesses the platform and selects the Registration section
        And selects the option to register a new expense
        And enters the trip ID
        And fills the required expense fields:
            | Field    | Value     |
            | Gasoline | <gasoline>|
            | Tolls    | <tolls>   |
            | PerDiem  | <perDiem> |
        And clicks on "Register"
        Then the entered data will be validated
        And the trip expenses will be registered

        Examples:
            | tripId | gasoline | tolls | perDiem |
            | 1      | 150.00   | 50.00 | 80.00   |
            | 2      | 200.50   | 75.00 | 120.00  |

    @US03 @Gastos @ErrorValidacion
    Scenario: Register expenses with negative values
        Given the entrepreneur wants to register trip expenses in the application
        When tries to register an expense with negative values
            | Field   | Value  |
            | Gasoline| -50.00 |
            | Tolls   | -10.00 |
        Then the expense will not be registered
        And a message indicating values must be positive will be displayed

    @US03 @Gastos @ErrorViaje
    Scenario: Register expenses for non-existent trip
        Given the entrepreneur wants to register trip expenses in the application
        When enters a non-existent trip ID
        Then the expense will not be registered
        And a message indicating the trip does not exist will be displayed