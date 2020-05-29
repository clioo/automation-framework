using DatabaseUtility.Models;
using System;

namespace DatabaseUtility.Extensions
{
    public static class ContentExtensions
    {
        public static void SetUpdatedFields(this ContentBase content)
        {
            content.UpdatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            content.UpdatedUtc = DateTime.UtcNow;
            content.Version++;
            content.VersionStatus = (int)VersionStatusEnum.Current;
        }

        public static void SetCreatedFields(this ContentBase content)
        {
            content.CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            content.CreatedUtc = DateTime.UtcNow;
            content.Version = 1;
            content.VersionStatus = (int)VersionStatusEnum.Current;
        }
    }
}