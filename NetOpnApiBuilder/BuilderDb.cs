using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder
{
    public class BuilderDb : DbContext
    {
        public class Factory : IDesignTimeDbContextFactory<BuilderDb>
        {
            public BuilderDb CreateDbContext(string[] args)
                => new BuilderDb(GetOptions(null));
        }

        /// <summary>
        /// The path to the application database.
        /// </summary>
        public static string DatabasePath => Program.AppDataPath + "/application.db";
        
        /// <summary>
        /// Get the options needed to build the default DB context.
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <returns></returns>
        public static DbContextOptions<BuilderDb> GetOptions(ILoggerFactory loggerFactory)
        {
            var path = DatabasePath;
            var ret = new DbContextOptionsBuilder<BuilderDb>()
                .UseSqlite($"Data Source={path};");
            if (loggerFactory != null) ret.UseLoggerFactory(loggerFactory);
            return ret.Options;
        }

        /// <summary>
        /// Gets the test device.
        /// </summary>
        public TestDevice GetTestDevice()
        {
            var ret = Set<TestDevice>().FirstOrDefault(x => x.ID == 1);

            if (ret is null)
            {
                ret = TestDevice.Default;
                Add(ret);
                SaveChanges();
            }

            return ret;
        }

        /// <summary>
        /// Gets the test device.
        /// </summary>
        /// <returns></returns>
        public async Task<TestDevice> GetTestDeviceAsync()
        {
            var ret = await Set<TestDevice>().FirstOrDefaultAsync(x => x.ID == 1);

            if (ret is null)
            {
                ret = TestDevice.Default;
                Add(ret);
                await SaveChangesAsync();
            }

            return ret;
        }

        public DbSet<ApiSource> ApiSources { get; set; }

        public DbSet<ApiObjectType> ApiObjectTypes { get; set; }

        public DbSet<ApiObjectProperty> ApiObjectProperties { get; set; }

        public DbSet<ApiModule> ApiModules { get; set; }

        public DbSet<ApiController> ApiControllers { get; set; }

        public DbSet<ApiCommand> ApiCommands { get; set; }

        public DbSet<ApiUrlParam> ApiUrlParams { get; set; }

        public DbSet<ApiQueryParam> ApiQueryParams { get; set; }

        
        public BuilderDb(DbContextOptions<BuilderDb> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestDevice>();

            var aotBuilder = modelBuilder.Entity<ApiObjectType>();
            aotBuilder.HasIndex(x => x.Name)
                      .IsUnique();

            var aopBuilder = modelBuilder.Entity<ApiObjectProperty>();
            aopBuilder.HasOne(x => x.ObjectType)
                      .WithMany(x => x.Properties)
                      .HasForeignKey(x => x.ObjectTypeID)
                      .OnDelete(DeleteBehavior.Cascade);
            aopBuilder.HasOne(x => x.DataTypeObjectType)
                      .WithMany()
                      .HasForeignKey(x => x.DataTypeObjectTypeID)
                      .OnDelete(DeleteBehavior.SetNull);
            aopBuilder.HasIndex(x => new {x.ObjectTypeID, x.ApiName})
                      .IsUnique();

            var asBuilder = modelBuilder.Entity<ApiSource>();
            asBuilder.HasIndex(x => x.Name)
                     .IsUnique();

            var amBuilder = modelBuilder.Entity<ApiModule>();
            amBuilder.HasOne(x => x.Source)
                     .WithMany(x => x.Modules)
                     .HasForeignKey(x => x.SourceID)
                     .OnDelete(DeleteBehavior.Cascade);
            amBuilder.HasIndex(x => new {x.SourceID, x.ApiName})
                     .IsUnique();

            var acBuilder = modelBuilder.Entity<ApiController>();
            acBuilder.HasOne(x => x.Module)
                     .WithMany(x => x.Controllers)
                     .HasForeignKey(x => x.ModuleID)
                     .OnDelete(DeleteBehavior.Cascade);
            acBuilder.HasIndex(x => new {x.ModuleID, x.ApiName})
                     .IsUnique();

            var aaBuilder = modelBuilder.Entity<ApiCommand>();
            aaBuilder.HasOne(x => x.Controller)
                     .WithMany(x => x.Commands)
                     .HasForeignKey(x => x.ControllerID)
                     .OnDelete(DeleteBehavior.Cascade);
            aaBuilder.HasIndex(x => new {x.ControllerID, x.ApiName})
                     .IsUnique();
            aaBuilder.HasOne(x => x.PostBodyObjectType)
                     .WithMany()
                     .HasForeignKey(x => x.PostBodyObjectTypeID)
                     .OnDelete(DeleteBehavior.SetNull);
            aaBuilder.HasOne(x => x.ResponseBodyObjectType)
                     .WithMany()
                     .HasForeignKey(x => x.ResponseBodyObjectTypeID)
                     .OnDelete(DeleteBehavior.SetNull);

            var aqpBuilder = modelBuilder.Entity<ApiQueryParam>();
            aqpBuilder.HasOne(x => x.Command)
                      .WithMany(x => x.QueryParams)
                      .HasForeignKey(x => x.CommandID)
                      .OnDelete(DeleteBehavior.Cascade);
            aqpBuilder.HasIndex(x => new {x.CommandID, x.ApiName})
                      .IsUnique();

            var aupBuilder = modelBuilder.Entity<ApiUrlParam>();
            aupBuilder.HasOne(x => x.Command)
                      .WithMany(x => x.UrlParams)
                      .HasForeignKey(x => x.CommandID)
                      .OnDelete(DeleteBehavior.Cascade);
            aupBuilder.HasIndex(x => new {x.CommandID, x.Order})
                      .IsUnique();
        }
    }
}
