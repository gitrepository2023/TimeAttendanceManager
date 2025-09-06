using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <Copyright>
///     Copyright © 2025-2026 GTN Industries Ltd - Nagpur Unit 
///     All rights reserved.
///     IT Department (Anil K. Waghmare)
/// </Copyright>
/// 
/// <summary>
///    Redistribution and use in source and binary forms, with or without modification, 
///    are permitted provided that the following conditions are met:
///
///   - Redistributions of source code must retain the above copyright notice, 
///     this list of conditions and the following disclaimer.
///
///   - Redistributions in binary form must reproduce the above copyright notice, 
///     this list of conditions and the following disclaimer in the documentation 
///     and/or other materials provided with the distribution.
/// </summary>
/// 
/// <Email>
///     (anil@gtnindustries.com)
/// </Email>
/// 
/// <Module>
///    Title:          Table class
///    Name:           MasterEmployee.cs
///    Created:        01st September 2025
///    Date Completed: 01st September 2025
/// </Module >
/// 
/// <ChangeLog>
///   Date Modified:   
/// 
/// </ChangeLog>
/// 
/// <databaseDetails>
///   Database Name:     
/// </databaseDetails>
/// 
/// <tablesUsed>
///   
/// </tablesUsed>
/// 
/// <commonControlsUsed>
///   
/// </commonControlsUsed>
/// 
/// <containersUsed>
///   
/// </containersUsed>
/// 
/// <menusToolBarsUsed>
///  
/// </menusToolBarsUsed>
/// 
/// <dataComponentsUsed>
///   
/// </dataComponentsUsed>
/// 
/// <componentsUsed>
///   
/// </componentsUsed>
/// 
/// <remarks>
///    
/// </remarks>
/// 

namespace TimeAttendanceManager.Features.Masters.Employee.Models
{
    public class MasterEmployee
    {
        public int? Id { get; set; }
        public string UnitCode { get; set; }
        public string EmployeeCode { get; set; }
        public string PunchCardNumber { get; set; }
        public string EmployeeName { get; set; }
        public string FathersName { get; set; }
        public string LastName { get; set; }
        public string EmployeeDisplayName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfRetirement { get; set; }
        public int? EmpGenderId { get; set; }
        public int? EmployeeTypeId { get; set; }
        public int? DutyLocationId { get; set; }
        public int? DesignationId { get; set; }
        public int? DepartmentId { get; set; }
        public int? JobCategoryId { get; set; }
        public int? GradeCodeId { get; set; }
        public int? BatchCodeId { get; set; }
        public int? ContractorId { get; set; }
        public int? CardColorId { get; set; }
        public int? MaritalStatusId { get; set; }
        public bool? IsWeeklyOffApplicable { get; set; }
        public int? WeeklyOffDayId { get; set; }
        public int? BloodGroupId { get; set; }
        public string ReportingManagerCode { get; set; }
        public DateTime? ResignationDate { get; set; }
        public string ReasonForLeaving { get; set; }
        public bool? IsServiceExtended { get; set; }
        public DateTime? ServiceExtendedUpto { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? RowVersion { get; set; }
        public int? UserId { get; set; }
        public int? UserUpdatedId { get; set; }
        public string UserRowGuid { get; set; }
        public string UserUpdatedRowGuid { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string IpAddsCreated { get; set; }
        public string IpAddsUpdated { get; set; }
        public string HostName { get; set; }
        public string HostNameUpdated { get; set; }
    }
}
