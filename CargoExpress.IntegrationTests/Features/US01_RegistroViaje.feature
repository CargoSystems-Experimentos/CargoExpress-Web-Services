@wip
Feature: US01 - Registro de un nuevo viaje
    As a logistics management entrepreneur I want to register a new trip data to have a saved record and show transparency to my clients

    @US01 @Viaje @HappyPath
    Scenario Outline: Register new trip successfully
        Given the entrepreneur wants to register a new trip in the application
        And has the following resources available:
            | Resource  | Availability |
            | Conductor | Available    |
            | Vehicle   | Available    |
        When accesses the platform and selects the Registration section
        And selects the option to register a new trip
        And fills the required trip fields:
            | Field      | Value         |
            | Origin     | <origin>      |
            | Destination| <destination> |
            | StartDate  | <startDate>   |
            | Client     | <client>      |
        And clicks on "Register"
        Then the entered data will be validated
        And information about the new trip will be registered

        Examples:
            | origin | destination | startDate | client    |
            | Lima   | Cusco        | 2024-01-15| ClienteA  |
            | Arequipa| Tacna        | 2024-02-01| EmpresaB  |

    @US01 @Viaje @ErrorValidacion
    Scenario: Register trip with incomplete data
        Given the entrepreneur wants to register a new trip in the application
        When accesses the platform and selects the Registration section
        And selects the option to register a new trip
        And fills the required fields with incomplete data:
            | Field     | Value |
            | Origin    |       |
            | Destination|      |
        And clicks on "Register"
        Then the trip will not be registered
        And a message indicating the required fields will be displayed

    @US01 @Viaje @ErrorRecurso
    Scenario: Register trip with no available driver
        Given the entrepreneur wants to register a new trip in the application
        And all drivers are assigned to other trips
        When tries to register a new trip
        Then the trip will not be registered
        And a message indicating no drivers are available will be displayed