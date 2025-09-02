using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.TypeConverter;

namespace TimeAttendanceManager.Features.Masters.Employee.Classes
{

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
    ///    Title:          PropertyGrid filter class
    ///    Name:           EmployeeMasterFilter.cs
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

    public class EmployeeMasterFilter
    {
        private string _unitCode;
        private DateTime _fromDate = DateTime.Now.AddMonths(-1);
        private DateTime _toDate = DateTime.Now;
        private List<string> _departmentList = new List<string>();
        private List<string> _designationList = new List<string>();
        private List<string> _employeeCodeList = new List<string>();
        private List<string> _employeeNameList = new List<string>();
        private bool _onlyActive = true;

        [Category("01 Unit"),
         DisplayName("Unit Code"),
         Description("Select Unit Code"),
         TypeConverter(typeof(UnitCodeListConverter))]
        public string UnitCode
        {
            get { return _unitCode; }
            set { _unitCode = value; }
        }

        [Category("02 Period"),
         DisplayName("From Date"),
         Description("Filter from date")]
        public DateTime FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }

        [Category("02 Period"),
         DisplayName("To Date"),
         Description("Filter to date")]
        public DateTime ToDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }

        [Category("03 Employee"),
         DisplayName("Employee Code"),
         Description("Filter by employee code"),
         Editor(typeof(StringListEditor), typeof(UITypeEditor))]
        public List<string> EmployeeCodeList
        {
            get { return _employeeCodeList; }
            set { _employeeCodeList = value; }
        }

        [Category("03 Employee"),
         DisplayName("Employee Name"),
         Description("Filter by employee name"),
         Editor(typeof(StringListEditor), typeof(UITypeEditor))]
        public List<string> EmployeeNameList
        {
            get { return _employeeNameList; }
            set { _employeeNameList = value; }
        }

        [Category("04 Organization"),
         DisplayName("Department"),
         Description("Filter by department"),
         Editor(typeof(StringListEditor), typeof(UITypeEditor))]
        public List<string> DepartmentList
        {
            get { return _departmentList; }
            set { _departmentList = value; }
        }

        [Category("04 Organization"),
         DisplayName("Designation"),
         Description("Filter by designation"),
         Editor(typeof(StringListEditor), typeof(UITypeEditor))]
        public List<string> DesignationList
        {
            get { return _designationList; }
            set { _designationList = value; }
        }

        [Category("05 Status"),
         DisplayName("Only Active Employees"),
         Description("Filter only active employees")]
        public bool OnlyActive
        {
            get { return _onlyActive; }
            set { _onlyActive = value; }
        }
    }
}
