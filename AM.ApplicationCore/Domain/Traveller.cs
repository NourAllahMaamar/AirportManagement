namespace AM.ApplicationCore.Domain;

public class Traveller : Passenger
{
    public required string HealthInformation { get; set; }
    public required string Nationality { get; set; }

    public override void PassengerType()
    {
        base.PassengerType();
        Console.WriteLine("I am a traveller");
    }

    public override string ToString()
    {
        return base.ToString() + $", Nationality: {Nationality}, Health Info: {HealthInformation}";
    }
}
