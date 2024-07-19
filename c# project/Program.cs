using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using CSharpProject.Models;

public class Program
{
    private static List<Department> departments;
    private static string filePath = "data.json";

    public static void Main(string[] args)
    {
        LoadData();
        Reserve();
        SaveData();
    }

    public static void LoadData()
    {
        if (File.Exists(filePath))
        {
            var jsonData = File.ReadAllText(filePath);
            departments = JsonConvert.DeserializeObject<List<Department>>(jsonData);
        }
        else
        {
            departments = new List<Department>
            {
                new Department("Pediatrics", new List<Doctor>
                {
                    new Doctor("John", "Doe", 10, new Dictionary<string, bool>
                    {
                        {"09:00-11:00", false},
                        {"12:00-14:00", false},
                        {"15:00-17:00", false}
                    }),
                    new Doctor("Jane", "Smith", 8, new Dictionary<string, bool>
                    {
                        {"09:00-11:00", false},
                        {"12:00-14:00", false},
                        {"15:00-17:00", false}
                    })
                }),
                new Department("Traumatology", new List<Doctor>
                {
                    new Doctor("Tom", "Brown", 12, new Dictionary<string, bool>
                    {
                        {"09:00-11:00", false},
                        {"12:00-14:00", false},
                        {"15:00-17:00", false}
                    })
                }),
                new Department("Dentistry", new List<Doctor>
                {
                    new Doctor("Alice", "Johnson", 15, new Dictionary<string, bool>
                    {
                        {"09:00-11:00", false},
                        {"12:00-14:00", false},
                        {"15:00-17:00", false}
                    })
                })
            };
        }
    }

    public static void SaveData()
    {
        var jsonData = JsonConvert.SerializeObject(departments, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }

    public static void Reserve()
    {
        Console.WriteLine("Enter user details (First Name, Last Name, Email, Phone):");
        var user = new User(Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), Console.ReadLine());

        Console.WriteLine("Select department:");
        for (int i = 0; i < departments.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {departments[i].Name}");
        }
        int deptIndex = int.Parse(Console.ReadLine()) - 1;
        var chosenDept = departments[deptIndex];

        Console.WriteLine("Select doctor:");
        for (int i = 0; i < chosenDept.Doctors.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {chosenDept.Doctors[i].FirstName} {chosenDept.Doctors[i].LastName}");
        }
        int docIndex = int.Parse(Console.ReadLine()) - 1;
        var chosenDoctor = chosenDept.Doctors[docIndex];

        Console.WriteLine("Select time slot:");
        int timeSlotIndex = 1;
        foreach (var time in chosenDoctor.AvailableTimes)
        {
            Console.WriteLine($"{timeSlotIndex}. {time.Key} {(time.Value ? "(Reserved)" : "(Available)")}");
            timeSlotIndex++;
        }
        int chosenTimeIndex = int.Parse(Console.ReadLine()) - 1;
        var selectedTime = chosenDoctor.AvailableTimes.ElementAt(chosenTimeIndex);

        if (selectedTime.Value)
        {
            Console.WriteLine("This time slot is already reserved.");
            return;
        }

        chosenDoctor.AvailableTimes[selectedTime.Key] = true;
        Console.WriteLine($"Thank you {user.FirstName} {user.LastName}, you have reserved a slot with Dr. {chosenDoctor.FirstName} {chosenDoctor.LastName} at {selectedTime.Key}.");
    }
}
