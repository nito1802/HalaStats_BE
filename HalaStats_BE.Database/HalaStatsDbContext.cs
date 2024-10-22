using HalaStats_BE.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace HalaStats_BE.Database
{
    public interface IHalaStatsDbContext
    {
        DbSet<MatchEntity> Matches { get; set; }
        DbSet<PlayerEntity> Players { get; set; }
        DbSet<MatchScheduleEntity> MatchSchedules { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class HalaStatsDbContext : DbContext, IHalaStatsDbContext
    {
        public DbSet<MatchEntity> Matches { get; set; }
        public DbSet<PlayerEntity> Players { get; set; }
        public DbSet<MatchScheduleEntity> MatchSchedules { get; set; }

        public HalaStatsDbContext()
        {
        }

        public HalaStatsDbContext(DbContextOptions<HalaStatsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HalaStats");

            // Konfiguracja dla TeamA
            modelBuilder.Entity<MatchEntity>(entity =>
            {
                entity.OwnsOne(e => e.TeamA, teamA =>
                {
                    teamA.OwnsMany(t => t.Players, p =>
                    {
                        p.WithOwner().HasForeignKey("TeamA_MatchId"); // Klucz obcy do MatchEntity
                        p.ToTable("Matches_TeamA_Players");  // Osobna tabela dla graczy TeamA
                    });
                });
            });

            // Konfiguracja dla TeamB
            modelBuilder.Entity<MatchEntity>(entity =>
            {
                entity.OwnsOne(e => e.TeamB, teamB =>
                {
                    teamB.OwnsMany(t => t.Players, p =>
                    {
                        p.WithOwner().HasForeignKey("TeamB_MatchId"); // Klucz obcy do MatchEntity
                        p.ToTable("Matches_TeamB_Players");  // Osobna tabela dla graczy TeamB
                    });
                });
            });

            // Konfiguracja dla TeamA w MatchScheduleEntity
            modelBuilder.Entity<MatchScheduleEntity>(entity =>
            {
                entity.OwnsOne(e => e.TeamA, teamA =>
                {
                    teamA.OwnsMany(t => t.Players, p =>
                    {
                        p.WithOwner().HasForeignKey("TeamA_MatchScheduleId"); // Klucz obcy do MatchScheduleEntity
                        p.ToTable("MatchSchedules_TeamA_Players");  // Osobna tabela dla graczy TeamA
                    });
                });
            });

            // Konfiguracja dla TeamB w MatchScheduleEntity
            modelBuilder.Entity<MatchScheduleEntity>(entity =>
            {
                entity.OwnsOne(e => e.TeamB, teamB =>
                {
                    teamB.OwnsMany(t => t.Players, p =>
                    {
                        p.WithOwner().HasForeignKey("TeamB_MatchScheduleId"); // Klucz obcy do MatchScheduleEntity
                        p.ToTable("MatchSchedules_TeamB_Players");  // Osobna tabela dla graczy TeamB
                    });
                });
            });

            base.OnModelCreating(modelBuilder);

            /*

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