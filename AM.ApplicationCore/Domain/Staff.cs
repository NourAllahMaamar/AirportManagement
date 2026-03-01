namespace AM.ApplicationCore.Domain;

public class Staff : Passenger
{
    public DateTime EmployementDate { get; set; }
    
    // Salary as decimal (SQLite compatible)
    public decimal Salary { get; set; }

    public override void PassengerType()
    {
        base.PassengerType();
        Console.WriteLine("I am a Staff Member");
    }

    public override string ToString()
    {
        return base.ToString() + $", Employment Date: {EmployementDate.ToShortDateString()}, Salary: {Salary:C}";
    }
}
