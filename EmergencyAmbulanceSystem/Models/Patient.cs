using System;

namespace EmergencyAmbulanceSystem.Models
{
    public class Patient
    {
        private static int _lastId;

        public int Id { get; }
        public string FullName { get; }
        public string PhoneNumber { get; }
        public string Location { get; }

        public Patient(string fullName, string phoneNumber, string location)
        {
            if (!Helpers.Validator.CheckFullName(fullName))
                throw new Exception("Invalid data");
            if (!Helpers.Validator.CheckPhoneNumber(phoneNumber))
                throw new Exception("Invalid data");
            if (!Helpers.Validator.CheckLocation(location))
                throw new Exception("Invalid data");

            _lastId++;
            Id = _lastId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Location = location;
        }
    }
}
