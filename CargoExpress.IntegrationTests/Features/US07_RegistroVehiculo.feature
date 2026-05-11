@wip
Feature: US07 - Registro de datos de vehículo
    As a logistics management entrepreneur I want to register my vehicles' data to keep track of the shipments they make

    @US07 @Vehiculo @HappyPath
    Scenario Outline: Register vehicle data successfully
        Given the entrepreneur wants to register vehicle data in the application
        When accesses the platform and selects the Registration section
        And selects the option to register a new vehicle
        And fills the required fields:
            | Field        | Value        |
            | Model        | <model>      |
            | Plate        | <plate>      |
            | TractorPlate | <tractorPlate>|
            | MaxLoad      | <maxLoad>    |
            | Volume       | <volume>     |
        And clicks on "Register"
        Then the entered data will be validated
        And the vehicle data will be registered

        Examples:
            | model   | plate   | tractorPlate | maxLoad | volume |
            | Volvo FH| ABC-123| DEF-456      | 20000   | 80     |
            | Scania R| XYZ-789| QRS-012      | 18000   | 75     |

    @US07 @Vehiculo @ErrorValidacion
    Scenario: Register vehicle with duplicate plate
        Given the entrepreneur wants to register vehicle data in the application
        And there is already a registered vehicle with plate "ABC-123"
        When tries to register a new vehicle with the same plate
        Then the vehicle will not be registered
        And a message indicating the vehicle already exists will be displayed

    @US07 @Vehiculo @ErrorCapacidad
    Scenario: Register vehicle with invalid max load
        Given the entrepreneur wants to register vehicle data in the application
        When enters a negative max load
            | Field   | Value |
            | MaxLoad | -1000 |
        Then the vehicle will not be registered
        And a message indicating max load must be positive will be displayed