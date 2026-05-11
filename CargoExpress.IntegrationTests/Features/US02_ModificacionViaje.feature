@wip
Feature: US02 - Modificación de datos de un viaje
    As a logistics management entrepreneur I want to modify the data of a trip to correct erroneous data that was recorded

    @US02 @Viaje @Modificacion @HappyPath
    Scenario Outline: Modify trip data successfully
        Given the entrepreneur wants to modify the data of a trip in the application
        And there is a registered trip with ID <tripId>
        When accesses the platform and selects the Registration section
        And selects the option to modify a trip
        And enters the trip ID to modify
        And modifies the required fields:
            | Field      | New Value    |
            | Destination| <newDestination> |
        And clicks on "Save"
        Then the entered data will be validated
        And the modifications on the trip will be registered

        Examples:
            | tripId | newDestination |
            | 1      | Ica             |
            | 2      | Huancayo        |

    @US02 @Viaje @Modificacion @Error
    Scenario: Modify non-existent trip
        Given the entrepreneur wants to modify the data of a trip in the application
        When enters a non-existent trip ID
        And tries to modify the data
        Then the modification will not be made
        And a message indicating the trip does not exist will be displayed