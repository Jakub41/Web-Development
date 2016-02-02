using System;

namespace ComputerWebShop.Models
{
    public enum GenderType
    {
        Other = 0,
        Male = 1,
        Female = 2
    }
    public class Person
    {
        // Class variables
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }

        /// Default constructor, calls Person(string newFirstName, string newLastName) and set the values to ""
        public Person() : this("", "")
        {
            // does nothing, the this("","") calls the Constructor below
        }
        /// Constructor with first and last name parameters
        public Person(string newFirstName, string newLastName)
        {
            // set id to nothing
            this.Id = "";
            // set this Person object public class variables based on the respective Constructor parameters
            this.FirstName = newFirstName;
            this.LastName = newLastName;
        }
        /// <summary>
        /// Constructor with the same of before + genderType
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="genderType"></param>
        public Person(string firstName, string lastName, GenderType genderType)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = genderType;
        }
    }
}﻿
