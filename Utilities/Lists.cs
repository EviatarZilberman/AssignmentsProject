using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Lists
    {
        public static List<SelectListItem> AssignmentStatuses()
        {
            return new List<SelectListItem>()
            {
            new SelectListItem { Text = "To Do" },
            new SelectListItem { Text = "In Progress" },
            new SelectListItem { Text = "Done" }
            };
        }

        public static string EnumToString (CoreReturns c)
        {
            string message = c.ToString().ToLower();
            string finalMessage = char.ToUpper(message[0]) + message.Substring(1);
            return finalMessage.Replace('_', ' ');            
        }
    }
}

