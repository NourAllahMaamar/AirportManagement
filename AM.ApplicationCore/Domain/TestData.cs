namespace AM.ApplicationCore.Domain;

public static class TestData
{
    public static Plane BoingPlane = new Plane(PlaneType.Boing, 150, new DateTime(2015, 2, 3));
    public static Plane AirbusPlane = new Plane(PlaneType.Airbus, 250, new DateTime(2020, 11, 11));

    public static Staff Captain = new Staff
    {
        FirstName = "Captain",
        LastName = "Smith",
        EmailAddress = "captain@airline.com",
        BirthDate = new DateTime(1965, 1, 1),
        EmployementDate = new DateTime(1999, 1, 1),
        Salary = 99999
    };

    public static Staff Hostess1 = new Staff
    {
        FirstName = "Hostess",
        LastName = "One",
        EmailAddress = "hostess1@airline.com",
        BirthDate = new DateTime(1995, 1, 1),
        EmployementDate = new DateTime(2020, 1, 1),
        Salary = 999
    };

    public static Staff Hostess2 = new Staff
    {
        FirstName = "Hostess",
        LastName = "Two",
        EmailAddress = "hostess2@airline.com",
        BirthDate = new DateTime(1996, 1, 1),
        EmployementDate = new DateTime(2020, 1, 1),
        Salary = 999
    };

    public static Traveller Traveller1 = new Traveller
    {
        FirstName = "John",
        LastName = "Doe",
        EmailAddress = "john.doe@email.com",
        BirthDate = new DateTime(1980, 1, 1),
        Nationality = "American",
        HealthInformation = "Good"
    };

    public static Traveller Traveller2 = new Traveller
    {
        FirstName = "Pierre",
        LastName = "Dupont",
        EmailAddress = "pierre.dupont@email.com",
        BirthDate = new DateTime(1981, 1, 1),
        Nationality = "French",
        HealthInformation = "Good"
    };

    public static Traveller Traveller3 = new Traveller
    {
        FirstName = "Ahmed",
        LastName = "Ben Ali",
        EmailAddress = "ahmed.benali@email.com",
        BirthDate = new DateTime(1982, 1, 1),
        Nationality = "Tunisian",
        HealthInformation = "Good"
    };

    public static Traveller Traveller4 = new Traveller
    {
        FirstName = "Mary",
        LastName = "Johnson",
        EmailAddress = "mary.johnson@email.com",
        BirthDate = new DateTime(1983, 1, 1),
        Nationality = "American",
        HealthInformation = "Good"
    };

    public static Traveller Traveller5 = new Traveller
    {
        FirstName = "Carlos",
        LastName = "Garcia",
        EmailAddress = "carlos.garcia@email.com",
        BirthDate = new DateTime(1984, 1, 1),
        Nationality = "Spanish",
        HealthInformation = "Good"
    };

    public static Flight Flight1 = new Flight
    {
        FlightDate = new DateTime(2022, 1, 1, 15, 10, 10),
        Destination = "Paris",
        EffectiveArrival = new DateTime(2022, 1, 1, 17, 0, 0),
        EstimatedDuration = 110,
        Plane = AirbusPlane,
        Passengers = new List<Passenger> { Traveller1, Traveller2, Traveller3, Traveller4, Traveller5, Captain, Hostess1, Hostess2 }
    };

    public static Flight Flight2 = new Flight
    {
        FlightDate = new DateTime(2022, 2, 1, 21, 10, 10),
        Destination = "Paris",
        EffectiveArrival = new DateTime(2022, 2, 1, 22, 55, 0),
        EstimatedDuration = 105,
        Plane = BoingPlane
    };

    public static Flight Flight3 = new Flight
    {
        FlightDate = new DateTime(2022, 3, 1, 5, 10, 10),
        Destination = "Paris",
        EffectiveArrival = new DateTime(2022, 3, 1, 6, 50, 0),
        EstimatedDuration = 100,
        Plane = BoingPlane
    };

    public static Flight Flight4 = new Flight
    {
        FlightDate = new DateTime(2022, 4, 1, 6, 10, 10),
        Destination = "Madrid",
        EffectiveArrival = new DateTime(2022, 4, 1, 8, 20, 0),
        EstimatedDuration = 130,
        Plane = BoingPlane
    };

    public static Flight Flight5 = new Flight
    {
        FlightDate = new DateTime(2022, 5, 1, 17, 10, 10),
        Destination = "Madrid",
        EffectiveArrival = new DateTime(2022, 5, 1, 18, 55, 0),
        EstimatedDuration = 105,
        Plane = BoingPlane
    };

    public static Flight Flight6 = new Flight
    {
        FlightDate = new DateTime(2022, 6, 1, 20, 10, 10),
        Destination = "Lisbonne",
        EffectiveArrival = new DateTime(2022, 6, 1, 23, 30, 0),
        EstimatedDuration = 200,
        Plane = AirbusPlane
    };

    public static List<Flight> listFlights = new List<Flight>
    {
        Flight1, Flight2, Flight3, Flight4, Flight5, Flight6
    };
}
