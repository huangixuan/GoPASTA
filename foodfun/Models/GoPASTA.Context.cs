﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace foodfun.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GoPASTAEntities : DbContext
    {
        public GoPASTAEntities()
            : base("name=GoPASTAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Carts> Carts { get; set; }
        public virtual DbSet<Categorys> Categorys { get; set; }
        public virtual DbSet<MealService> MealService { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrdersDetails> OrdersDetails { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductsProperty> ProductsProperty { get; set; }
        public virtual DbSet<ProgramList> ProgramList { get; set; }
        public virtual DbSet<Propertys> Propertys { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
