using System.Collections.Generic;

namespace CSharpProject.Models
{
    public class Department
    {
        public string Name { get; set; }
        public List<Doctor> Doctors { get; set; }

        public Department(string name, List<Doctor> doctors)
        {
            Name = name;
            Doctors = doctors;
        }
    }
}
