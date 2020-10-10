﻿using System;

namespace AuditExample.Models
{
    public enum EnumState
    {
        Update = 1,
        Delete = 2,
        Added = 3
    }
    
    public class ChangeLog
    {
        public string EntityName { get; set; }
        public string PropertyName { get; set; }
        public string PrimaryKeyValue { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime DateChanged { get; set; }
        public EnumState State { get; set; }
    }
}