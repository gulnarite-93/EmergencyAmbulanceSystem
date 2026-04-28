using System;
using EmergencyAmbulanceSystem.Helpers;

namespace EmergencyAmbulanceSystem.Models
{
    public enum PriorityLevel { Low, Medium, High }

    public class EmergencyCase
    {
        public string CaseNo { get; }
        public Patient Patient { get; }
        public PriorityLevel Priority { get; }
        public EmergencyStatus Status { get; private set; }
        public Ambulance? AssignedAmbulance { get; private set; }

        public EmergencyCase(Patient patient, PriorityLevel priority)
        {
            Patient = patient ?? throw new ArgumentNullException(nameof(patient));
            Priority = priority;
            CaseNo = Helpers.Validator.GenerateCaseNo();
            Status = EmergencyStatus.Created;
        }

        public void AssignAmbulance(Ambulance ambulance)
        {
            if (ambulance == null) throw new Exception("Invalid data");
            if (!ambulance.IsAvailable) throw new Exception("Invalid data");
            if (AssignedAmbulance != null) throw new Exception("Invalid data");

            ambulance.Assign();
            AssignedAmbulance = ambulance;
            Status = EmergencyStatus.Assigned;
        }

        public void MarkOnRoute()
        {
            if (Status != EmergencyStatus.Assigned) throw new Exception("Invalid data");
            Status = EmergencyStatus.OnRoute;
        }

        public void Complete()
        {
            if (Status == EmergencyStatus.Completed) throw new Exception("Invalid data");
            Status = EmergencyStatus.Completed;
            AssignedAmbulance?.Release();
        }
    }
}
