using System;

namespace EmergencyAmbulanceSystem.Helpers
{
    internal static class Validator
    {
        private static int _serial;

        public static bool CheckFullName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length >= 2;
        }

        public static bool CheckPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            int digits = 0;
            foreach (var c in phone)
            {
                if (char.IsDigit(c)) digits++;
            }
            return digits >= 10 && digits <= 15;
        }

        public static bool CheckLocation(string location)
        {
            return !string.IsNullOrWhiteSpace(location);
        }

        public static string GenerateCaseNo()
        {
            _serial++;
            return $"EMG{1000 + _serial}";
        }
    }
}
