﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DruckLib
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PrintingContainer : DbContext
    {
        public PrintingContainer()
            : base("name=PrintingContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Person> PersonSet { get; set; }
        public virtual DbSet<Drucker> DruckerSet { get; set; }
        public virtual DbSet<Druckauftrag> DruckauftragSet { get; set; }
    }
}