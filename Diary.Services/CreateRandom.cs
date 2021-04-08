using System;

namespace Diary.Services
{
    public static class CreateRandom
    {
        private const ushort _lengthPasword = 16;

        public static string Password()
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ_1234567890_abcdefghijklmnopqrstuvwxyz";
            var password = "";

            for (int i = 0; i < _lengthPasword; i++)
            {
                var random = new Random().Next(0, letters.Length);
                password += letters[random];
            }

            return password;
        }
    }
}
