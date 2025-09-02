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
///    Name:           AttShiftSchedule.cs
///    Created:        25th August 2025
///    Date Completed: 25th August 2025
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

namespace TimeAttendanceManager.Attendance.Models
{
    public class AttShiftSchedule
    {
        public int? Id { get; set; }
        public string UnitCode { get; set; }
        public string ShiftCode { get; set; }
        public string ShiftDescription { get; set; }
        public string ShiftType { get; set; }
        public TimeSpan? ShiftStartTime { get; set; }
        public TimeSpan? ShiftEndTime { get; set; }
        public byte? ShiftDurationHours { get; set; }
        public byte? ShiftGraceEarlyMin { get; set; }
        public byte? ShiftGraceLateMin { get; set; }
        public TimeSpan? ShiftLunchStartTime { get; set; }
        public TimeSpan? ShiftLunchEndTime { get; set; }
        public byte? ShiftLunchHourTime { get; set; }
        public byte? ShiftLunchGraceEarlyMin { get; set; }
        public byte? ShiftLunchGraceLateMin { get; set; }
        public byte? ShiftLunchMinTime { get; set; }
        public byte? ShiftMinimumOtMin { get; set; }
        public byte? MonthlyShiftInLatePermitted { get; set; }
        public byte? MonthlyLunchLatePermitted { get; set; }
        public int? ShiftDurationMin { get; set; }
        public int? PaidDurationMin { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? RowVersion { get; set; }
        public int? UserId { get; set; }
        public string UserRowGuid { get; set; }
        public int? UserUpdatedId { get; set; }
        public string UserUpdatedRowGuid { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string DeletedByUserRowGuid { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string IpAddsCreated { get; set; }
        public string IpAddsUpdated { get; set; }
        public string HostName { get; set; }
        public string UpdatedHostName { get; set; }
    }
}
