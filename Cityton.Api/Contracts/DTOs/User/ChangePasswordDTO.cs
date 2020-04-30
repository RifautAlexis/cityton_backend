using System;
namespace Cityton.Api.Contracts.DTOs
{
    public class ChangePasswordDTO
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        internal void Deconstruct(out string oldPassword, out string newPassword)
        {
            oldPassword = OldPassword;
            newPassword = NewPassword;
        }
    }
}
