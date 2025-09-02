using System;

namespace TimeAttendanceManager.Auth.Models
{
    /// <summary>
    /// 14.08.2025
    /// </summary>
    public class UserLogin
    {
        public int? Id { get; set; }
        public string UnitCode { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }  // For varbinary(max)
        public byte[] MasterPasswordHash { get; set; }
        public byte[] Password { get; set; }
        public byte[] MasterPassword { get; set; }
        public byte[] Salt { get; set; }
        public byte[] MasterPasswordSalt { get; set; }
        public int? DeptId { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public int? RoleId { get; set; }
        public int? CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? RowVersion { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLockedOut { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? UserLocked { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string IpAddsCreated { get; set; }
        public string IpAddsUpdated { get; set; }
        public Guid? UserRowGuid { get; set; }
    }

    /// <summary>
    /// 14.08.2025
    /// </summary>
    public class UserLoginView
    {
        public int Id { get; set; }
        public string UnitCode { get; set; }
        public string UserName { get; set; }
        public int? DeptId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyUnitCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAliasName { get; set; }
        public string CompanyAliasName01 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserFirstLastName { get; set; }
        public int? RowVersion { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLockedOut { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? UserLocked { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string IpAddsCreated { get; set; }
        public string IpAddsUpdated { get; set; }
        public Guid? UserRowGuid { get; set; }
    }
}
