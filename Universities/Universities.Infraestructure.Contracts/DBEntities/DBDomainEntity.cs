﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Universities.Infraestructure.Contracts.DBEntities
{
    public partial class DBDomainEntity
    {
        public int Id { get; set; }
        public int? UniversitiProperty { get; set; }
        public string Domain1 { get; set; }

        public virtual DBUniversityEntity UniversitiPropertyNavigation { get; set; }
    }
}