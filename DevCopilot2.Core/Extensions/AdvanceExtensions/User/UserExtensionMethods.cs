using DevCopilot2.Core.Mappers.Users;
using DevCopilot2.Domain.DTOs.Users;
using DevCopilot2.Domain.Entities.Users;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCopilot2.Core.Extensions.AdvanceExtensions.Users
{
    public static class UserExtensionMethods
    {
        public static string GetName(this string phoneNumber, string? firstName, string? lastName
            , string defaultName = "کاربر سایت")
        {
            if (!string.IsNullOrEmpty(firstName) &&
               !string.IsNullOrEmpty(lastName)) return
                    $"{firstName} {lastName}";
            if (!string.IsNullOrEmpty(firstName)) return firstName;
            if (!string.IsNullOrEmpty(lastName)) return lastName;
            return defaultName;
        }

        public static string GetName(this string phoneNumber, string? firstName, string? lastName)
            => phoneNumber.GetName(firstName, lastName, "کاربر سایت");
        public static string GetName(this UserListDto user, string defaultName = "کاربر سایت")
        => user.PhoneNumber.GetName(user.FirstName, user.LastName, defaultName);

        public static string GetName(this User? user, string defaultName = "کاربر سایت")
        => user is not null ? user.ToDto().GetName() : "-";
    }
}
