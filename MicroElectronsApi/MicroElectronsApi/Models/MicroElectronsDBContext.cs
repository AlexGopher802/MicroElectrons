using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MicroElectronsApi.Models
{
    public partial class MicroElectronsDBContext : DbContext
    {
        public static string connectionString; 

        public MicroElectronsDBContext()
        {
        }

        public MicroElectronsDBContext(DbContextOptions<MicroElectronsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ComeJournal> ComeJournals { get; set; }
        public virtual DbSet<Counterparty> Counterparties { get; set; }
        public virtual DbSet<Dismissal> Dismissals { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeStatus> EmployeeStatuses { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<Labor> Labors { get; set; }
        public virtual DbSet<OperationType> OperationTypes { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<StorageMethod> StorageMethods { get; set; }
        public virtual DbSet<Supply> Supplies { get; set; }
        public virtual DbSet<SupplyCompo> SupplyCompos { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskStatus> TaskStatuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }
        public virtual DbSet<VisitorJournal> VisitorJournals { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.36-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            modelBuilder.Entity<ComeJournal>(entity =>
            {
                entity.ToTable("ComeJournal");

                entity.HasIndex(e => e.OperationId, "operationId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.DateTimeConfirm)
                    .HasColumnType("datetime")
                    .HasColumnName("dateTimeConfirm");

                entity.Property(e => e.IsCome)
                    .HasColumnName("isCome")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.OperationId)
                    .HasColumnType("int(11)")
                    .HasColumnName("operationId");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(11)")
                    .HasColumnName("quantity");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("subjectName")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.ComeJournals)
                    .HasForeignKey(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ComeJournal_ibfk_1");
            });

            modelBuilder.Entity<Counterparty>(entity =>
            {
                entity.ToTable("Counterparty");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("address")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Bic)
                    .HasMaxLength(9)
                    .HasColumnName("bic");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Tin)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("tin");
            });

            modelBuilder.Entity<Dismissal>(entity =>
            {
                entity.ToTable("Dismissal");

                entity.HasIndex(e => e.EmployeeId, "employeeId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.DateConfirm)
                    .HasColumnType("date")
                    .HasColumnName("dateConfirm");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employeeId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Dismissals)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Dismissal_ibfk_1");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasIndex(e => e.PostId, "postId");

                entity.HasIndex(e => e.StatusId, "statusId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstName")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastName")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(50)
                    .HasColumnName("patronymic")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.PostId)
                    .HasColumnType("int(11)")
                    .HasColumnName("postId");

                entity.Property(e => e.StatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("statusId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_ibfk_1");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_ibfk_2");
            });

            modelBuilder.Entity<EmployeeStatus>(entity =>
            {
                entity.ToTable("EmployeeStatus");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.ToTable("Holiday");

                entity.HasIndex(e => e.EmployeeId, "employeeId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("date")
                    .HasColumnName("dateEnd");

                entity.Property(e => e.DateStart)
                    .HasColumnType("date")
                    .HasColumnName("dateStart");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employeeId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Holidays)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Holiday_ibfk_1");
            });

            modelBuilder.Entity<Labor>(entity =>
            {
                entity.ToTable("Labor");

                entity.HasIndex(e => e.EmployeeId, "employeeId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.DateConfirm)
                    .HasColumnType("date")
                    .HasColumnName("dateConfirm");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employeeId");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Labors)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Labor_ibfk_1");
            });

            modelBuilder.Entity<OperationType>(entity =>
            {
                entity.ToTable("OperationType");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.CategoryId, "categoryId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("categoryId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_ibfk_1");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<StorageMethod>(entity =>
            {
                entity.ToTable("StorageMethod");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Supply>(entity =>
            {
                entity.ToTable("Supply");

                entity.HasIndex(e => e.CounterpartyId, "counterpartyId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CounterpartyId)
                    .HasColumnType("int(11)")
                    .HasColumnName("counterpartyId");

                entity.Property(e => e.DateSupply)
                    .HasColumnType("date")
                    .HasColumnName("dateSupply");

                entity.Property(e => e.IsSell)
                    .HasColumnName("isSell")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Counterparty)
                    .WithMany(p => p.Supplies)
                    .HasForeignKey(d => d.CounterpartyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Supply_ibfk_1");
            });

            modelBuilder.Entity<SupplyCompo>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "productId");

                entity.HasIndex(e => e.SupplyId, "supplyId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("productId");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(11)")
                    .HasColumnName("quantity");

                entity.Property(e => e.Summa).HasColumnName("summa");

                entity.Property(e => e.SupplyId)
                    .HasColumnType("int(11)")
                    .HasColumnName("supplyId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SupplyCompos)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SupplyCompos_ibfk_1");

                entity.HasOne(d => d.Supply)
                    .WithMany(p => p.SupplyCompos)
                    .HasForeignKey(d => d.SupplyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SupplyCompos_ibfk_2");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.HasIndex(e => e.EmployeeId, "employeeId");

                entity.HasIndex(e => e.StatusId, "statusId");

                entity.HasIndex(e => e.WarehouseId, "warehouseId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.DateDeadline)
                    .HasColumnType("date")
                    .HasColumnName("dateDeadline");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("date")
                    .HasColumnName("dateEnd");

                entity.Property(e => e.DateStart)
                    .HasColumnType("date")
                    .HasColumnName("dateStart");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employeeId");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(11)")
                    .HasColumnName("quantity");

                entity.Property(e => e.StatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("statusId");

                entity.Property(e => e.WarehouseId)
                    .HasColumnType("int(11)")
                    .HasColumnName("warehouseId");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_ibfk_2");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_ibfk_3");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_ibfk_1");
            });

            modelBuilder.Entity<TaskStatus>(entity =>
            {
                entity.ToTable("TaskStatus");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.EmployeeId, "employeeId");

                entity.HasIndex(e => e.Login, "login")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.EmployeeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employeeId");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("password");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_ibfk_1");
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.ToTable("Visitor");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstName")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastName")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Passport)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("passport");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(50)
                    .HasColumnName("patronymic")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<VisitorJournal>(entity =>
            {
                entity.ToTable("VisitorJournal");

                entity.HasIndex(e => e.EmployeeEntryId, "employeeEntryId");

                entity.HasIndex(e => e.EmployeeExitId, "employeeExitId");

                entity.HasIndex(e => e.VisitorId, "visitorId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.DateTimeEntry)
                    .HasColumnType("datetime")
                    .HasColumnName("dateTimeEntry");

                entity.Property(e => e.DateTimeExit)
                    .HasColumnType("datetime")
                    .HasColumnName("dateTimeExit");

                entity.Property(e => e.EmployeeEntryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employeeEntryId");

                entity.Property(e => e.EmployeeExitId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employeeExitId");

                entity.Property(e => e.VisitorId)
                    .HasColumnType("int(11)")
                    .HasColumnName("visitorId");

                entity.HasOne(d => d.EmployeeEntry)
                    .WithMany(p => p.VisitorJournalEmployeeEntries)
                    .HasForeignKey(d => d.EmployeeEntryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VisitorJournal_ibfk_1");

                entity.HasOne(d => d.EmployeeExit)
                    .WithMany(p => p.VisitorJournalEmployeeExits)
                    .HasForeignKey(d => d.EmployeeExitId)
                    .HasConstraintName("VisitorJournal_ibfk_2");

                entity.HasOne(d => d.Visitor)
                    .WithMany(p => p.VisitorJournals)
                    .HasForeignKey(d => d.VisitorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VisitorJournal_ibfk_3");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("Warehouse");

                entity.HasIndex(e => e.ProductId, "productId");

                entity.HasIndex(e => e.StorageMethodId, "storageMethodId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("productId");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(11)")
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.StorageMethodId)
                    .HasColumnType("int(11)")
                    .HasColumnName("storageMethodId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Warehouse_ibfk_1");

                entity.HasOne(d => d.StorageMethod)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.StorageMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Warehouse_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
