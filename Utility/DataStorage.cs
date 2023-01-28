﻿using apicea.Models;

namespace apicea.Utility
{
    public static class DataStorage
    {
        public static List<Employee> GetAllEmployees() =>
           new List<Employee>
           {
                new Employee { Name="Mike", LastName="Turner", Age=35, Gender="Male"},
                new Employee { Name="Sonja", LastName="Markus", Age=22, Gender="Female"},
                new Employee { Name="Luck", LastName="Martins", Age=40, Gender="Male"},
                new Employee { Name="Sofia", LastName="Packner", Age=30, Gender="Female"},
                new Employee { Name="John", LastName="Doe", Age=45, Gender="Male"}
           };
    }
}
