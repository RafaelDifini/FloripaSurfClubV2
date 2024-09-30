using Microsoft.EntityFrameworkCore;
using FloripaSurfClubCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using FloripaSurfClubAPI.Models.Account;

namespace FloripaSurfClubCore.Data
{
    public class FloripaSurfClubContextV2 : IdentityDbContext<UsuarioSistema, IdentityRole<Guid>, Guid>
    {
        public FloripaSurfClubContextV2(DbContextOptions<FloripaSurfClubContextV2> options)
            : base(options)
        {
        }

        public DbSet<Aluguel> Alugueis { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Caixa> Caixa { get; set; }
        public DbSet<Atendente> Atendentes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de herança TPT
            modelBuilder.Entity<UsuarioSistema>().ToTable("UsuariosSistema");
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Aluno>().ToTable("Alunos");
            modelBuilder.Entity<Professor>().ToTable("Professores");
            modelBuilder.Entity<Atendente>().ToTable("Atendentes");

            // Configuração das chaves estrangeiras e relacionamentos
            modelBuilder.Entity<Aula>()
                .HasOne(a => a.Professor)
                .WithMany(p => p.Aulas)
                .HasForeignKey(a => a.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Aula>()
                .HasMany(a => a.Alunos)
                .WithMany(al => al.Aulas)
                .UsingEntity<Dictionary<string, object>>(
                    "AulaAluno",
                    j => j.HasOne<Aluno>().WithMany().HasForeignKey("AlunoId"),
                    j => j.HasOne<Aula>().WithMany().HasForeignKey("AulaId"));

            modelBuilder.Entity<Aula>()
             .Property(a => a.DataInicio)
             .HasColumnType("timestamptz");

            modelBuilder.Entity<Aluguel>()
                .HasOne(a => a.Equipamento)
                .WithMany()
                .HasForeignKey(a => a.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Aluguel>()
                .HasOne(a => a.Cliente)
                .WithMany()
                .HasForeignKey(a => a.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class FloripaSurfClubContextV2Factory : IDesignTimeDbContextFactory<FloripaSurfClubContextV2>
    {
        public FloripaSurfClubContextV2 CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FloripaSurfClubContextV2>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return new FloripaSurfClubContextV2(optionsBuilder.Options);
        }
    }
}
