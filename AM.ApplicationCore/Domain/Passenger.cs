using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.ApplicationCore.Domain;

public class Passenger
{
    // PassportNumber as Primary Key with 7 characters
    [Key]
    [StringLength(7, MinimumLength = 7, ErrorMessage = "PassportNumber must be exactly 7 characters")]
    public string PassportNumber { get; set; }

    // FirstName with Min 3, Max 25 characters and custom error message
    [Required]
    [StringLength(25, MinimumLength = 3, ErrorMessage = "FirstName must be between 3 and 25 characters")]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    // EmailAddress with Email validation
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string EmailAddress { get; set; }

    // BirthDate with Display name "Date of Birth"
    [Display(Name = "Date of Birth")]
    public DateTime BirthDate { get; set; }

    // TelNumber with 8 digits regex validation
    [RegularExpression(@"^\d{8}$", ErrorMessage = "TelNumber must be exactly 8 digits")]
    public string TelNumber { get; set; }

    // Navigation properties - virtual for lazy loading
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
