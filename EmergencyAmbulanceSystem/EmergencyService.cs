using System;
using System.Collections.Generic;
using System.Linq;
using EmergencyAmbulanceSystem.Models;

namespace EmergencyAmbulanceSystem
{
    public class EmergencyService
    {
        private List<EmergencyCase> _cases = new List<EmergencyCase>();
        private List<Ambulance> _ambulances = new List<Ambulance>();

        public EmergencyService()
        {
            _ambulances.Add(new Ambulance { Id = 1, PlateNumber = "10-AM-001", DriverName = "Eli Memmedov", IsAvailable = true });
            _ambulances.Add(new Ambulance { Id = 2, PlateNumber = "10-AM-002", DriverName = "Veli Eliyev", IsAvailable = true });
            _ambulances.Add(new Ambulance { Id = 3, PlateNumber = "10-AM-003", DriverName = "Hesen Rehimov", IsAvailable = true });
        }

        public void CreateEmergencyCase(Patient patient, Priority priority)
        {
            if (patient == null)
            {
                Console.WriteLine(" Patient boş ola bilməz");
                return;
            }

            var newCase = new EmergencyCase(patient, priority);
            _cases.Add(newCase);

            Console.WriteLine($" Yeni çağırış yaradıldı: {newCase.CaseNo}");
        }

        public void AssignAmbulance(string caseNo)
        {
            var @case = _cases.FirstOrDefault(c => c.CaseNo == caseNo);

            if (@case == null)
            {
                Console.WriteLine("Çağırış tapılmadı");
                return;
            }

            if (@case.Status != EmergencyStatus.Created)
            {
                Console.WriteLine("Bu çağırışa artıq ambulans təyin edilib və ya status uyğun deyil");
                return;
            }

            var ambulance = _ambulances.FirstOrDefault(a => a.IsAvailable);

            if (ambulance == null)
            {
                Console.WriteLine("Boş ambulans yoxdur");
                return;
            }

            // Use domain methods instead of setting internals directly
            @case.AssignAmbulance(ambulance);
            ambulance.Assign();

            Console.WriteLine($"{ambulance.PlateNumber} çağırışa təyin edildi");
        }

        public void StartDispatch(string caseNo)
        {
            var @case = _cases.FirstOrDefault(c => c.CaseNo == caseNo);

            if (@case == null)
            {
                Console.WriteLine("Çağırış tapılmadı");
                return;
            }

            if (@case.Status != EmergencyStatus.Assigned)
            {
                Console.WriteLine("Yalnız Assigned olan çağırış yola çıxa bilər");
                return;
            }

            // Use domain method to change status
            @case.MarkOnRoute();

            Console.WriteLine(" Ambulans yola çıxdı");
        }

        public void CompleteCase(string caseNo)
        {
            var @case = _cases.FirstOrDefault(c => c.CaseNo == caseNo);

            if (@case == null)
            {
                Console.WriteLine("Çağırış tapılmadı");
                return;
            }

            if (@case.Status != EmergencyStatus.OnRoute)
            {
                Console.WriteLine("Yalnız yolda olan çağırış tamamlanır");
                return;
            }

            // Use domain method to complete the case
            @case.Complete();

            if (@case.AssignedAmbulance != null)
                @case.AssignedAmbulance.Release();

            Console.WriteLine(" Çağırış tamamlandı");
        }

        public void GetCase(string caseNo)
        {
            var @case = _cases.FirstOrDefault(c => c.CaseNo == caseNo);

            if (@case == null)
            {
                Console.WriteLine("Tapılmadı");
                return;
            }

            Console.WriteLine($"No: {@case.CaseNo} | Xəstə: {@case.Patient.FullName} | Status: {@case.Status} | Priority: {@case.Priority}");
        }

        public void GetAllCases()
        {
            Console.WriteLine("\n--- Bütün çağırışlar ---");

            foreach (var c in _cases)
            {
                Console.WriteLine($"No: {c.CaseNo} | Xəstə: {c.Patient.FullName} | Status: {c.Status} | Priority: {c.Priority}");
            }
        }

        public void GetCasesByStatus(string status)
        {
            Console.WriteLine($"\n--- {status} statuslu çağırışlar ---");

            foreach (var c in _cases.Where(c => c.Status.ToString().Equals(status, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"No: {c.CaseNo} | Xəstə: {c.Patient.FullName}");
            }
        }

        public void GetHighPriorityCases()
        {
            Console.WriteLine("\n--- High priority ---");

            foreach (var c in _cases.Where(c => c.Priority == Priority.High))
            {
                Console.WriteLine($"No: {c.CaseNo} | Xəstə: {c.Patient.FullName}");
            }
        }

        public void GetAvailableAmbulances()
        {
            Console.WriteLine("\n--- Boş ambulanslar ---");

            foreach (var a in _ambulances.Where(a => a.IsAvailable))
            {
                Console.WriteLine($"ID: {a.Id} | Plate: {a.PlateNumber} | Sürücü: {a.DriverName}");
            }
        }

        public void GetSystemInfo()
        {
            Console.WriteLine("\n Sistem məlumatı:");

            Console.WriteLine($"butun çağırış: {_cases.Count}");
            Console.WriteLine($"aktiv çağırışlar: {_cases.Count(c => c.Status != EmergencyStatus.Completed)}");
            Console.WriteLine($"Tamamlanmış: {_cases.Count(c => c.Status == EmergencyStatus.Completed)}");
            Console.WriteLine($"Boş ambulanslar: {_ambulances.Count(a => a.IsAvailable)}");
        }
    }
}