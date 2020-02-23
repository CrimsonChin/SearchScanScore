using System;
using System.Linq;

namespace Domain
{
    public static class IdGenerator
    {
        private static readonly Random random = new Random();

        public static string Create(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string[] CreateBatch(int batchSize, int length)
        {
            var result = new string[batchSize];
            var codesGenerated = 0;

            while (codesGenerated < batchSize)
            {
                var code = Create(length);
                if (result.Contains(code))
                {
                    continue;
                }

                result[codesGenerated] = code;
                codesGenerated++;
            }

            return result;
        }
    }
}
