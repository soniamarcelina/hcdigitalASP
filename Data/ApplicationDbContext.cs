using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hcdigital.Models;

namespace hcdigital.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        //grade = new DbSet<Grade>(); // Inisialisasi properti Grades
        //tkjp = new DbSet<TKJP>();
    
        }



        // Definisikan setiap tabel yang ada dalam database Anda di sini sebagai DbSet.
        public DbSet<Grade>? grade { get; set; }
        public DbSet<TKJP>? tademployee { get; set; }
        public DbSet<Contractor>? contractor { get; set; }
        public DbSet<Competency>? technical_comp {get; set;}
        public DbSet<Position>? tadposition {get; set;}
        public DbSet<DirectPos>? masteremployee {get; set;}
        public DbSet<OCF>? ocf {get; set;}
        public DbSet<MRF>? mrf {get; set;}
        public DbSet<Approval>? Approval {get; set;}
        public DbSet<AO>? assignmentorder {get; set;}
        public DbSet<JobRoles>? jobroles {get; set;}
        public DbSet<Candidate>? mrf_candidate {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Di sini Anda dapat menentukan konfigurasi tambahan untuk model database Anda,
            // seperti indeks, kunci asing, dll.
            modelBuilder.Entity<TKJP>()
                .HasKey(t => t.Nopek);
            modelBuilder.Entity<Contractor>()
                .HasKey(c => c.contractNo);
            modelBuilder.Entity<Position>()
            .HasKey(p => p.Id_Position);
            modelBuilder.Entity<DirectPos>()
            .HasKey(d => d.ID_Position);
            modelBuilder.Entity<AO>()
            .HasKey(a => a.id);
            modelBuilder.Entity<OCF>()
            .HasKey(o => o.id);
            modelBuilder.Entity<MRF>()
            .HasKey(m => m.id_mrf);
            modelBuilder.Entity<Approval>()
            .HasKey(v => v.id);
             modelBuilder.Entity<JobRoles>()
            .HasKey(r => r.id);
            modelBuilder.Entity<Candidate>()
            .HasKey(c => c.id);
            // modelBuilder.Entity<Position>()
            // .HasOne(p => p.masteremployee)
            // .WithMany()
            // .HasForeignKey(p => p.ID_Position) // Kolom dalam tabel Position
            // .HasPrincipalKey(d => d.ID_Position); // Kolom dalam tabel DirectPos
            
            
        }
    }
}