using System;
using EmergencyAmbulanceSystem;
using EmergencyAmbulanceSystem.Models;

class Program
{
    static void Main(string[] args)
    {
        EmergencyService service = new EmergencyService();

        while (true)
        {
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1. Create Emergency Case");
            Console.WriteLine("2. Assign Ambulance");
            Console.WriteLine("3. Start Dispatch");
            Console.WriteLine("4. Complete Case");
            Console.WriteLine("5. Get Case by CaseNo");
            Console.WriteLine("6. Get All Cases");
            Console.WriteLine("7. Filter by Status");
            Console.WriteLine("8. High Priority Cases");
            Console.WriteLine("9. Available Ambulances");
            Console.WriteLine("10. System Info");
            Console.WriteLine("0. Exit");

            Console.Write("Seçim et: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Ad Soyad: ");
                    string name = Console.ReadLine();

                    Console.Write("Telefon: ");
                    string phone = Console.ReadLine();

                    Console.Write("Ünvan: ");
                    string location = Console.ReadLine();

                    Console.Write("Priority (Low, Medium, High): ");
                    Priority priority = Enum.Parse<Priority>(Console.ReadLine(), true);

                    Patient patient = new Patient
                    {
                        FullName = name,
                        PhoneNumber = phone,
                        Location = location
                    };

                    service.CreateEmergencyCase(patient, priority);
                    break;

                case "2":
                    Console.Write("CaseNo daxil et: ");
                    service.AssignAmbulance(Console.ReadLine());
                    break;

                case "3":
                    Console.Write("CaseNo daxil et: ");
                    service.StartDispatch(Console.ReadLine());
                    break;

                case "4":
                    Console.Write("CaseNo daxil et: ");
                    service.CompleteCase(Console.ReadLine());
                    break;

                case "5":
                    Console.Write("CaseNo daxil et ");
                    service.GetCase(Console.ReadLine());
                    break;

                case "6":
                    service.GetAllCases();
                    break;

                case "7":
                    Console.Write("Status daxil et ");
                    service.GetCasesByStatus(Console.ReadLine());
                    break;

                case "8":
                    service.GetHighPriorityCases();
                    break;

                case "9":
                    service.GetAvailableAmbulances();
                    break;

                case "10":
                    service.GetSystemInfo();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Yanlış seçim");
                    break;
            }
        }
    }
}