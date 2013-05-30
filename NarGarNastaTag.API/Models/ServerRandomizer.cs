using System;

namespace NarGarNastaTag.API.Models
{
    class ServerRandomizer : IServerRandomizer
    {
        public int GetServerNumber()
        {
            var random = new Random().NextDouble() + 5;
            return Convert.ToInt16(Math.Round(random));
        }
    }
}