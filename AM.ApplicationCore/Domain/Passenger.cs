namespace AM.ApplicationCore.Domain;

public class Passenger
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<Flight> Flights { get; set; }

    public Passenger()
    {
        Flights = new List<Flight>();
    }

    
    public bool CheckProfile(string firstName, string lastName, string? email = null)
    {
        if (email == null)
            return FirstName == firstName && LastName == lastName;
        else
            return FirstName == firstName && LastName == lastName && EmailAddress == email;
    }

    public virtual void PassengerType()
    {
        Console.WriteLine("I am a passenger");
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}, Email: {EmailAddress}, Birth Date: {BirthDate.ToShortDateString()}";
    }
}
