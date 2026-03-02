using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.ApplicationCore.Domain;

public class Passenger
{
    [Key]
    [StringLength(7, MinimumLength = 7, ErrorMessage = "PassportNumber must be exactly 7 characters")]
    public required string PassportNumber { get; set; }

    [Required]
    [StringLength(25, MinimumLength = 3, ErrorMessage = "FirstName must be between 3 and 25 characters")]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string EmailAddress { get; set; }

    [Display(Name = "Date of Birth")]
    public DateTime BirthDate { get; set; }

    [RegularExpression(@"^\d{8}$", ErrorMessage = "TelNumber must be exactly 8 digits")]
    public required string TelNumber { get; set; }

    public virtual ICollection<Flight> Flights { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; }

    public Passenger()
    {
        Flights = new List<Flight>();
        Tickets = new List<Ticket>();
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
