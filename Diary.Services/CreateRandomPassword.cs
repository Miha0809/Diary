using System;

namespace Diary.Services
{
    public static class CreateRandom
    {
        public static string Password { get; set; }

        private const ushort _lengthPasword = 16;

        static CreateRandom()
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            for (int i = 0; i < _lengthPasword; i++)
            {
                var random = new Random().Next(0, letters.Length);
                Password += letters[random];
            }
        }
    }
}
