using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalaStats_BE.Database.Entities;

namespace HalaStats_BE.Database
{
    public interface IHalaStatsDbContext
    {
        /*
        DbSet<ExcerciseEntity> Excercises { get; set; }
        DbSet<ExcerciseTypeEntity> ExcerciseTypes { get; set; }
        DbSet<GoogleDriveImageEntity> GoogleDriveImages { get; set; }
        DbSet<LanguageEntity> Languages { get; set; }
        DbSet<SentenceEntity> Sentences { get; set; }
        DbSet<PhraseEntity> Phrases { get; set; }
        DbSet<PriorityEntity> Priorities { get; set; }
        DbSet<TagEntity> Tags { get; set; }
        DbSet<StatEntity> Stats { get; set; }
        DbSet<SendMethodEntity> SendMethods { get; set; }
        DbSet<LogEntity> Logs { get; set; }
        */

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class HalaStatsDbContext : DbContext, IHalaStatsDbContext
    {
        /*
        public DbSet<ExcerciseEntity> Excercises { get; set; }
        public DbSet<ExcerciseTypeEntity> ExcerciseTypes { get; set; }
        public DbSet<GoogleDriveImageEntity> GoogleDriveImages { get; set; }
        public DbSet<LanguageEntity> Languages { get; set; }
        public DbSet<SentenceEntity> Sentences { get; set; }
        public DbSet<PhraseEntity> Phrases { get; set; }
        public DbSet<PriorityEntity> Priorities { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<StatEntity> Stats { get; set; }
        public DbSet<SendMethodEntity> SendMethods { get; set; }
        public DbSet<LogEntity> Logs { get; set; }
        */

        public HalaStatsDbContext()
        {
        }

        public HalaStatsDbContext(DbContextOptions<HalaStatsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HalaStats");

            base.OnModelCreating(modelBuilder);

            /*
            // Konfiguracja wartości obiektowej
            modelBuilder.Entity<ExcerciseEntity>(entity =>
            {
                entity.OwnsOne(e => e.ExcerciseResult);
            });

            // Relacja wiele-do-jeden między PhraseEntity a LanguageEntity
            modelBuilder.Entity<SentenceEntity>()
                .HasOne(p => p.Language)
                .WithMany()
                .HasForeignKey(p => p.LanguageId);

            // Relacja wiele-do-jeden między ExcerciseEntity a ExcerciseTypeEntity
            modelBuilder.Entity<ExcerciseEntity>()
                .HasOne(p => p.ExcerciseType)
                .WithMany()
                .HasForeignKey(p => p.ExcerciseTypeId);

            // Relacja wiele-do-jeden między PhraseEntity a PriorityEntity
            modelBuilder.Entity<SentenceEntity>()
                .HasOne(p => p.Priority)
                .WithMany()
                .HasForeignKey(p => p.PriorityId);

            modelBuilder.Entity<SentenceEntity>()
                .HasOne(p => p.SendMethod)
                .WithMany()
                .HasForeignKey(p => p.SendMethodId);
            //.OnDelete(DeleteBehavior.Cascade); // Dodanie zachowania przy usuwaniu=

            modelBuilder.Entity<SentenceEntity>()
               .HasOne(p => p.Phrase)
               .WithMany()
               .HasForeignKey(p => p.PhraseId)
               .IsRequired(false); // Ustawienie relacji jako opcjonalnej

            modelBuilder.Entity<ExcerciseEntity>()
               .HasOne(p => p.Phrase)
               .WithMany()
               .HasForeignKey(p => p.PhraseId)
               .IsRequired(false); // Ustawienie relacji jako opcjonalnej
            */
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
                return;
            }

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json");

            IConfiguration configuration = configurationBuilder.Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("HalaStatsDatabase"));
            //optionsBuilder.UseSqlServer(@"Data Source=.\sqlexpress;Initial Catalog=ApkaMichalaJarka;Integrated Security=True");

            base.OnConfiguring(optionsBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.CreatedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        item.Entity.ModifiedAt = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        item.Entity.ModifiedAt = DateTime.Now;
                        item.State = EntityState.Deleted;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}