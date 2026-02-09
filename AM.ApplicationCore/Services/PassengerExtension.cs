using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Services;

public static class PassengerExtension
{

    public static string UpperFullName(this Passenger passenger)
    {
        string firstName = passenger.FirstName;
        string lastName = passenger.LastName;

        if (!string.IsNullOrEmpty(firstName))
        {
            firstName = char.ToUpper(firstName[0]) + firstName.Substring(1).ToLower();
        }

        if (!string.IsNullOrEmpty(lastName))
        {
            lastName = char.ToUpper(lastName[0]) + lastName.Substring(1).ToLower();
        }

        return $"{firstName} {lastName}";
    }
}
