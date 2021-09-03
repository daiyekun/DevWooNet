using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Dev.WooNet.Model.Models
{
    public partial class DevDbContext : DbContext
    {
        public DevDbContext()
        {
        }

        public DevDbContext(DbContextOptions<DevDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DevAppGroupUser> DevAppGroupUsers { get; set; }
        public virtual DbSet<DevAppInst> DevAppInsts { get; set; }
        public virtual DbSet<DevAppInstHist> DevAppInstHists { get; set; }
        public virtual DbSet<DevAppInstNode> DevAppInstNodes { get; set; }
        public virtual DbSet<DevAppInstNodeArea> DevAppInstNodeAreas { get; set; }
        public virtual DbSet<DevAppInstNodeAreaHist> DevAppInstNodeAreaHists { get; set; }
        public virtual DbSet<DevAppInstNodeHist> DevAppInstNodeHists { get; set; }
        public virtual DbSet<DevAppInstNodeInfo> DevAppInstNodeInfos { get; set; }
        public virtual DbSet<DevAppInstNodeInfoHist> DevAppInstNodeInfoHists { get; set; }
        public virtual DbSet<DevAppInstNodeLine> DevAppInstNodeLines { get; set; }
        public virtual DbSet<DevAppInstNodeLineHist> DevAppInstNodeLineHists { get; set; }
        public virtual DbSet<DevAppInstOpin> DevAppInstOpins { get; set; }
        public virtual DbSet<DevAppInstOpinHist> DevAppInstOpinHists { get; set; }
        public virtual DbSet<DevCity> DevCities { get; set; }
        public virtual DbSet<DevCompany> DevCompanies { get; set; }
        public virtual DbSet<DevCompcontact> DevCompcontacts { get; set; }
        public virtual DbSet<DevCompdesc> DevCompdescs { get; set; }
        public virtual DbSet<DevCompfile> DevCompfiles { get; set; }
        public virtual DbSet<DevCountry> DevCountries { get; set; }
        public virtual DbSet<DevCurrencyManager> DevCurrencyManagers { get; set; }
        public virtual DbSet<DevDatadic> DevDatadics { get; set; }
        public virtual DbSet<DevDepartment> DevDepartments { get; set; }
        public virtual DbSet<DevDeptmain> DevDeptmains { get; set; }
        public virtual DbSet<DevFlowGroup> DevFlowGroups { get; set; }
        public virtual DbSet<DevFlowGroupuser> DevFlowGroupusers { get; set; }
        public virtual DbSet<DevFlowTemp> DevFlowTemps { get; set; }
        public virtual DbSet<DevFlowTempHist> DevFlowTempHists { get; set; }
        public virtual DbSet<DevFlowTempNode> DevFlowTempNodes { get; set; }
        public virtual DbSet<DevFlowTempNodeHist> DevFlowTempNodeHists { get; set; }
        public virtual DbSet<DevFlowTempNodeInfo> DevFlowTempNodeInfos { get; set; }
        public virtual DbSet<DevFlowTempNodeInfoHist> DevFlowTempNodeInfoHists { get; set; }
        public virtual DbSet<DevLoginLog> DevLoginLogs { get; set; }
        public virtual DbSet<DevOptionLog> DevOptionLogs { get; set; }
        public virtual DbSet<DevProvince> DevProvinces { get; set; }
        public virtual DbSet<DevRemind> DevReminds { get; set; }
        public virtual DbSet<DevRole> DevRoles { get; set; }
        public virtual DbSet<DevRoleModule> DevRoleModules { get; set; }
        public virtual DbSet<DevRolePession> DevRolePessions { get; set; }
        public virtual DbSet<DevSealmanager> DevSealmanagers { get; set; }
        public virtual DbSet<DevSysemail> DevSysemails { get; set; }
        public virtual DbSet<DevSysmodel> DevSysmodels { get; set; }
        public virtual DbSet<DevTempNodeArea> DevTempNodeAreas { get; set; }
        public virtual DbSet<DevTempNodeAreaHist> DevTempNodeAreaHists { get; set; }
        public virtual DbSet<DevTempNodeLine> DevTempNodeLines { get; set; }
        public virtual DbSet<DevTempNodeLineHist> DevTempNodeLineHists { get; set; }
        public virtual DbSet<DevUserModule> DevUserModules { get; set; }
        public virtual DbSet<DevUserPession> DevUserPessions { get; set; }
        public virtual DbSet<DevUserRole> DevUserRoles { get; set; }
        public virtual DbSet<DevUserinfo> DevUserinfos { get; set; }
        public virtual DbSet<DevUserminor> DevUserminors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=devdb;persist security info=True;user id=sa;password=Sasa123", Microsoft.EntityFrameworkCore.ServerVersion.FromString("8.0.20-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DevAppGroupUser>(entity =>
            {
                entity.ToTable("dev_app_group_user");

                entity.Property(e => e.NodeStrId)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevAppInst>(entity =>
            {
                entity.ToTable("dev_app_inst");

                entity.Property(e => e.AddDateTime).HasColumnType("datetime");

                entity.Property(e => e.AppObjAmount).HasPrecision(28, 6);

                entity.Property(e => e.AppObjName)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AppObjNo)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.CurrentNodeName)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CurrentNodeStrId)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FinceType)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<DevAppInstHist>(entity =>
            {
                entity.ToTable("dev_app_inst_hist");

                entity.Property(e => e.AddDatetime).HasColumnType("datetime");

                entity.Property(e => e.AppObjAmount).HasPrecision(28, 6);

                entity.Property(e => e.AppObjName)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AppObjNo)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CompleteDateTime).HasColumnType("datetime");

                entity.Property(e => e.CurrentNodeName)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CurrentNodeStrId)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<DevAppInstNode>(entity =>
            {
                entity.ToTable("dev_app_inst_node");

                entity.Property(e => e.CompDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NodeStrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ReceDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<DevAppInstNodeArea>(entity =>
            {
                entity.ToTable("dev_app_inst_node_area");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevAppInstNodeAreaHist>(entity =>
            {
                entity.ToTable("dev_app_inst_node_area_hist");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevAppInstNodeHist>(entity =>
            {
                entity.ToTable("dev_app_inst_node_hist");

                entity.Property(e => e.AddDateTime).HasColumnType("datetime");

                entity.Property(e => e.CompDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NodeStrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ReceDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<DevAppInstNodeInfo>(entity =>
            {
                entity.ToTable("dev_app_inst_node_info");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Max).HasPrecision(28, 6);

                entity.Property(e => e.Min).HasPrecision(28, 6);

                entity.Property(e => e.NodeStrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevAppInstNodeInfoHist>(entity =>
            {
                entity.ToTable("dev_app_inst_node_info_hist");

                entity.Property(e => e.GroupName)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Max).HasPrecision(28, 6);

                entity.Property(e => e.Min).HasPrecision(28, 6);

                entity.Property(e => e.NodeStrId)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevAppInstNodeLine>(entity =>
            {
                entity.ToTable("dev_app_inst_node_line");

                entity.Property(e => e.From)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.To)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevAppInstNodeLineHist>(entity =>
            {
                entity.ToTable("dev_app_inst_node_line_hist");

                entity.Property(e => e.From)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StrId)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.To)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevAppInstOpin>(entity =>
            {
                entity.ToTable("dev_app_inst_opin");

                entity.HasIndex(e => e.NodeId, "instoption_node");

                entity.Property(e => e.AddDateTime).HasColumnType("datetime");

                entity.Property(e => e.NodeStrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Opinion)
                    .IsRequired()
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Node)
                    .WithMany(p => p.DevAppInstOpins)
                    .HasForeignKey(d => d.NodeId)
                    .HasConstraintName("instoption_node");
            });

            modelBuilder.Entity<DevAppInstOpinHist>(entity =>
            {
                entity.ToTable("dev_app_inst_opin_hist");

                entity.Property(e => e.AddDatetTme).HasColumnType("datetime");

                entity.Property(e => e.NodeStrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Opinion)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevCity>(entity =>
            {
                entity.ToTable("dev_city");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ShowName)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevCompany>(entity =>
            {
                entity.ToTable("dev_company");

                entity.Property(e => e.Account)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AddDateTime).HasColumnType("datetime");

                entity.Property(e => e.BankName)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BusScope)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BusTerm)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Caddress)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Cfax)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Code)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DutyNo)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.EsDateTime).HasColumnType("datetime");

                entity.Property(e => e.ExpDateTime).HasColumnType("datetime");

                entity.Property(e => e.InvAddress)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InvTel)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InvTitle)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LegalPerson)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PaidCapital)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PostCode)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RegAddress)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RegCapital)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(4000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Reserve1)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Reserve2)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Telephone)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Trade)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.WebUrl)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.WnodeName)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevCompcontact>(entity =>
            {
                entity.ToTable("dev_compcontact");

                entity.Property(e => e.AddDateTime).HasColumnType("datetime");

                entity.Property(e => e.Dname)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Fax)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PhoneTel)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Qq)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RoleName)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<DevCompdesc>(entity =>
            {
                entity.ToTable("dev_compdesc");

                entity.Property(e => e.AddDateTime).HasColumnType("datetime");

                entity.Property(e => e.Item)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<DevCompfile>(entity =>
            {
                entity.ToTable("dev_compfile");

                entity.Property(e => e.AddDateTime).HasColumnType("datetime");

                entity.Property(e => e.Extension)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FileName)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FilePath)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FolderName)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GuidFileName)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<DevCountry>(entity =>
            {
                entity.ToTable("dev_country");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ShowName)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevCurrencyManager>(entity =>
            {
                entity.ToTable("dev_currency_manager");

                entity.Property(e => e.Abbreviation)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Code)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDatetime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Rate).HasPrecision(19, 8);

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Sname)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevDatadic>(entity =>
            {
                entity.ToTable("dev_datadic");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.ModifyDatetime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevDepartment>(entity =>
            {
                entity.ToTable("dev_department");

                entity.Property(e => e.Code)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Dpath)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Dsort)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Dstatus).HasColumnName("DStatus");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Sname)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevDeptmain>(entity =>
            {
                entity.ToTable("dev_deptmain");

                entity.Property(e => e.Account)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Address)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BankName)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Fax)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InvoiceName)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LawPerson)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TaxId)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TelePhone)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ZipCode)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevFlowGroup>(entity =>
            {
                entity.ToTable("dev_flow_group");

                entity.Property(e => e.AddDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevFlowGroupuser>(entity =>
            {
                entity.ToTable("dev_flow_groupuser");
            });

            modelBuilder.Entity<DevFlowTemp>(entity =>
            {
                entity.ToTable("dev_flow_temp");

                entity.Property(e => e.AddDateTime).HasColumnType("datetime");

                entity.Property(e => e.CategoryIds)
                    .IsRequired()
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DeptIds)
                    .IsRequired()
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FlowItems)
                    .IsRequired()
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevFlowTempHist>(entity =>
            {
                entity.ToTable("dev_flow_temp_hist");

                entity.Property(e => e.AddDateTime).HasColumnType("datetime");

                entity.Property(e => e.CategoryIds)
                    .IsRequired()
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DeptIds)
                    .IsRequired()
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FlowItems)
                    .IsRequired()
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevFlowTempNode>(entity =>
            {
                entity.ToTable("dev_flow_temp_node");

                entity.Property(e => e.AddDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevFlowTempNodeHist>(entity =>
            {
                entity.ToTable("dev_flow_temp_node_hist");

                entity.Property(e => e.AddDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevFlowTempNodeInfo>(entity =>
            {
                entity.ToTable("dev_flow_temp_node_info");

                entity.Property(e => e.Max).HasPrecision(28, 6);

                entity.Property(e => e.Min).HasPrecision(28, 6);

                entity.Property(e => e.NodeStrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevFlowTempNodeInfoHist>(entity =>
            {
                entity.ToTable("dev_flow_temp_node_info_hist");

                entity.Property(e => e.Max).HasPrecision(28, 6);

                entity.Property(e => e.Min).HasPrecision(28, 6);

                entity.Property(e => e.NodeStrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevLoginLog>(entity =>
            {
                entity.ToTable("dev_login_log");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.LoginIp)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RequestNetIp)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevOptionLog>(entity =>
            {
                entity.ToTable("dev_option_log");

                entity.Property(e => e.ActionName)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ActionTitle)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ControllerName)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.ExecResult)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RequestData)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RequestIp)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RequestMethod)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RequestNetIp)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RequestUrl)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevProvince>(entity =>
            {
                entity.ToTable("dev_province");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Cid).HasColumnName("CId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ShowName)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevRemind>(entity =>
            {
                entity.ToTable("dev_remind");

                entity.Property(e => e.CustomName)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Item)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevRole>(entity =>
            {
                entity.ToTable("dev_role");

                entity.Property(e => e.Code)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevRoleModule>(entity =>
            {
                entity.ToTable("dev_role_module");
            });

            modelBuilder.Entity<DevRolePession>(entity =>
            {
                entity.ToTable("dev_role_pession");

                entity.Property(e => e.DeptIds)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FuncCode)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevSealmanager>(entity =>
            {
                entity.ToTable("dev_sealmanager");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.EnabledDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDatetime).HasColumnType("datetime");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(1000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SealCode)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SealName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevSysemail>(entity =>
            {
                entity.ToTable("dev_sysemail");

                entity.Property(e => e.MailPwd)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SendNickname)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SenderMail)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ServiceName)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevSysmodel>(entity =>
            {
                entity.ToTable("dev_sysmodel");

                entity.Property(e => e.Code)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.Ico)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ModifyDatetime).HasColumnType("datetime");

                entity.Property(e => e.Mpath)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RequestUrl)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevTempNodeArea>(entity =>
            {
                entity.ToTable("dev_temp_node_area");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevTempNodeAreaHist>(entity =>
            {
                entity.ToTable("dev_temp_node_area_hist");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevTempNodeLine>(entity =>
            {
                entity.ToTable("dev_temp_node_line");

                entity.Property(e => e.From)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.M).HasColumnType("float(255,5)");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.To)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevTempNodeLineHist>(entity =>
            {
                entity.ToTable("dev_temp_node_line_hist");

                entity.Property(e => e.From)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.M).HasColumnType("float(255,5)");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StrId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.To)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevUserModule>(entity =>
            {
                entity.ToTable("dev_user_module");
            });

            modelBuilder.Entity<DevUserPession>(entity =>
            {
                entity.ToTable("dev_user_pession");

                entity.Property(e => e.DeptIds)
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FuncCode)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevUserRole>(entity =>
            {
                entity.ToTable("dev_user_role");
            });

            modelBuilder.Entity<DevUserinfo>(entity =>
            {
                entity.ToTable("dev_userinfo");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.EntryDatetime).HasColumnType("datetime");

                entity.Property(e => e.IdNo)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Mobile)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ModifyDatetime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("pwd")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ShowName)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Tel)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.WxCode)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<DevUserminor>(entity =>
            {
                entity.ToTable("dev_userminor");

                entity.Property(e => e.Address)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Minfo)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PhName)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PhPath)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(2000)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
