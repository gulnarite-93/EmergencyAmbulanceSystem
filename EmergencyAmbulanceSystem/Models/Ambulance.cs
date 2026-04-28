using System;

namespace EmergencyAmbulanceSystem.Models
{
    public class Ambulance
    {
        private static int _id = 1;

        public int Id { get; private set; }
        public string PlateNumber { get; }
        public string DriverName { get; }
        public bool IsAvailable { get; private set; } = true;

        
        public Ambulance(string plateNumber, string driverName)
        {
            if (string.IsNullOrWhiteSpace(plateNumber) || string.IsNullOrWhiteSpace(driverName))
                throw new Exception("Invalid data");

            Id = _id++;
            PlateNumber = plateNumber;
            DriverName = driverName;
        }

        
        public void Assign()
        {
            IsAvailable = false;
        }

       
        public void Release()
        {
            IsAvailable = true;
        }
    }
}
