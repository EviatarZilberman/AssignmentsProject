using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Colboinik
    {
        public static string EnumToString(CoreReturns c)
        {
            string message = c.ToString().ToLower();
            string finalMessage = char.ToUpper(message[0]) + message.Substring(1);
            return finalMessage.Replace('_', ' ');
        }
    }
}
