using BilgeCollege.DAL.Configs;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BilgeCollege.DAL.Context
{
    public class CollegeContext : IdentityDbContext<User>
    {
        public CollegeContext()
        {
            
        }

        public CollegeContext(DbContextOptions<CollegeContext> options) : base(options)
        {
            
        }

        DbSet<AltTopic> AltTopics { get; set; }
        DbSet<Classroom> Classrooms { get; set; }
        DbSet<Guardian> Guardians { get; set; }
        DbSet<MainTopic> MainTopics { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<DaySchedule> DaySchedules { get; set; }
        DbSet<DaySchedule_AltTopic> DaySchedule_AltTopics { get; set; }

        string connectionString = "Server=DESKTOP-F5HL1HE;Database=BilgeCollegeDB;Trusted_Connection=true;TrustServerCertificate=true;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if(!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new UserRoleConfig());
            builder.ApplyConfiguration(new AltTopicConfig());
            builder.ApplyConfiguration(new ClassroomConfig());
            builder.ApplyConfiguration(new GuardianConfig());
            builder.ApplyConfiguration(new MainTopicConfig());
            builder.ApplyConfiguration(new StudentConfig());
            builder.ApplyConfiguration(new TeacherConfig());
            builder.ApplyConfiguration(new DaySchedule_AltTopicConfig());
            builder.ApplyConfiguration(new DayScheduleConfig());
        }
    }
}
