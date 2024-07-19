using System.Collections.Generic;

namespace CSharpProject.Models
{
    public class Doctor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Experience { get; set; }
        public Dictionary<string, bool> AvailableTimes { get; set; }

        public Doctor(string firstName, string lastName, int experience, Dictionary<string, bool> availableTimes)
        {
            FirstName = firstName;
            LastName = lastName;
            Experience = experience;
            AvailableTimes = availableTimes;
        }
    }
}
