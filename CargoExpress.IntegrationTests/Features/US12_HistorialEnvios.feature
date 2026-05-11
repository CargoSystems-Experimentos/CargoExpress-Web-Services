@wip
Feature: US12 - Historial de envíos realizados
    As a logistics management entrepreneur I want to have a shipment history to keep a detailed record of all the services I have performed and access this information at any time

    @US12 @Historial @HappyPath
    Scenario: View shipment history
        Given the entrepreneur needs to keep a record of all services performed
        And has shipments registered in the system
        When accesses the application to review previously performed services
        And selects the History section to view "Shipment History"
        Then the list of performed shipments will be displayed
        And can view the details of each one

    @US12 @Historial @Vacio
    Scenario: Empty history when there are no shipments
        Given the entrepreneur has no shipments registered
        When accesses the Shipment History section
        Then a message indicating no shipments are registered will be displayed
        And the option to register a new trip will be offered

    @US12 @Historial @Filtro
    Scenario: Filter history by date
        Given the entrepreneur is in the shipment history
        When applies a date filter
        And specifies a date range
        Then the platform will display only shipments within that range