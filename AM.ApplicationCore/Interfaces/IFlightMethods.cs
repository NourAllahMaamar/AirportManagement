using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Interfaces;

public interface IFlightMethods
{
    List<DateTime> GetFlightDates(string destination);
    List<DateTime> GetFlightDatesLinq(string destination);
    void ShowFlightDetails(Plane plane);
    int ProgrammedFlightNumber(DateTime startDate);
    double DurationAverage(string destination);
    List<Flight> OrderedDurationFlights();
    List<Traveller> SeniorTravellers(Flight flight);
    void DestinationGroupedFlights();
    List<Flight> GetFlights(string filterType, string filterValue);
}
