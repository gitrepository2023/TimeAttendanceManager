using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

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
/// <Class>
///    Title:          Create the Helper Class
///                    
///    Name:           ClassTableCollections.cs
///    Created:        22nd August 2025
///    Date Completed: 22nd August 2025
/// </Class >
/// 
/// <classChangeLog>
///   Date Modified:   NA
/// 
/// </classChangeLog>
/// 
/// <databaseDetails>
///   Database Name:     
/// </databaseDetails>
/// 
/// <tablesUsed>
///  
/// 
/// </tablesUsed>
/// 
/// <remarks>
///    
/// </remarks>
/// 

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
/// <Class>
///    Title:          Create the Helper Class
///                    
///    Name:           ClassTableCollections.cs
///    Created:        22nd August 2025
///    Date Completed: 22nd August 2025
/// </Class >
/// 
/// <classChangeLog>
///   Date Modified:   NA
/// 
/// </classChangeLog>
/// 
/// <databaseDetails>
///   Database Name:     
/// </databaseDetails>
/// 
/// <tablesUsed>
///  
/// 
/// </tablesUsed>
/// 
/// <remarks>
///    
/// </remarks>
/// 

namespace TimeAttendanceManager.Auth.Classes
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))] // nice in PropertyGrid
    public class ClassTableCollections : Collection<TableNames>
    {
        public ClassTableCollections()
        {
        }
        /// <summary>
        /// Initializes from a semicolon-separated string:
        /// "TableA;TableB;TableC"
        /// Each token is passed to tableNames(string) ctor.
        /// </summary>
        public ClassTableCollections(string serialized)
        {
            if (string.IsNullOrWhiteSpace(serialized)) return;

            var tokens = serialized.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var raw in tokens)
            {
                var token = raw == null ? null : raw.Trim();
                if (string.IsNullOrEmpty(token)) continue;

                try
                {
                    Add(new TableNames(token));
                }
                catch (Exception ex)
                {
                    throw new InvalidCastException(
                        $"Invalid table name serialization '{token}'.", ex);
                }
            }
        }

        /// <summary>
        /// Adds a range of items.
        /// </summary>
        public void AddRange(IEnumerable<TableNames> items)
        {
            if (items == null) return;
            foreach (var item in items)
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(items), "tableNames item cannot be null.");
                Add(item);
            }
        }

        /// <summary>
        /// Indexer is already provided by Collection<T> as this[int index],
        /// but you can expose a VB-like alias if you want.
        /// </summary>
        public TableNames this[int index]
        {
            get { return base.Items[index]; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                base.SetItem(index, value);
            }
        }

        /// <summary>
        /// Finds the index of a specific item.
        /// (Alias for base.IndexOf)
        /// </summary>
        public int IndexOf(TableNames value)
        {
            return base.IndexOf(value);
        }

        /// <summary>
        /// Inserts an item at the specified index.
        /// </summary>
        public void Insert(int index, TableNames value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            base.Insert(index, value);
        }

        /// <summary>
        /// Removes the first occurrence of a specific item.
        /// </summary>
        public void Remove(TableNames value)
        {
            if (value == null) return;
            base.Remove(value);
        }

        /// <summary>
        /// Determines whether the collection contains a specific value.
        /// </summary>
        public bool Contains(TableNames value)
        {
            if (value == null) return false;
            return base.Contains(value);
        }

        /// <summary>
        /// Validates items (optional hook).
        /// </summary>
        protected override void InsertItem(int index, TableNames item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item), "Value must be of type tableNames.");
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, TableNames item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item), "New value must be of type tableNames.");
            base.SetItem(index, item);
        }

        /// <summary>
        /// Serializes back to "A;B;C" form using item.ToString().
        /// </summary>
        public override string ToString()
        {
            if (this.Count == 0) return string.Empty;

            // Avoid trailing separators and ignore null/empty string outputs
            var parts = new List<string>(this.Count);
            for (int i = 0; i < this.Count; i++)
            {
                var s = this[i]?.ToString();
                if (!string.IsNullOrWhiteSpace(s))
                    parts.Add(s);
            }

            return string.Join(";", parts);
        }

        /// <summary>
        /// TryParse helper. Returns false if any token fails to construct.
        /// </summary>
        public static bool TryParse(string serialized, out ClassTableCollections result, out string error)
        {
            result = new ClassTableCollections();
            error = null;

            if (string.IsNullOrWhiteSpace(serialized)) return true;

            var tokens = serialized.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var raw in tokens)
            {
                var token = raw == null ? null : raw.Trim();
                if (string.IsNullOrEmpty(token)) continue;

                try
                {
                    result.Add(new TableNames(token));
                }
                catch (Exception ex)
                {
                    error = $"Invalid table name '{token}': {ex.Message}";
                    result = null;
                    return false;
                }
            }
            return true;
        }
    }
}

