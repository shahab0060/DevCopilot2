using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.Entities.Entities;
using DevCopilot2.Domain.Entities.GeneralSettings;
using DevCopilot2.Domain.Entities.Languages;
using DevCopilot2.Domain.Entities.Permissions;
using DevCopilot2.Domain.Entities.Projects;
using DevCopilot2.Domain.Entities.Properties;
using DevCopilot2.Domain.Entities.Roles;
using DevCopilot2.Domain.Entities.SiteSettings;
using DevCopilot2.Domain.Entities.Templates;
using DevCopilot2.Domain.Entities.Users;
using DevCopilot2.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DevCopilot2.DataLayer.Context
{
    public class DevCopilot2DbContext : DbContext
    {
        public DevCopilot2DbContext(DbContextOptions<DevCopilot2DbContext> options) : base(options)
        {

        }

        #region entities

        #region site settings

        public virtual DbSet<SiteSetting> SiteSettings { get; set; }        

        #endregion

        #region templates

        public virtual DbSet<Template> Templates { get; set; }

        #endregion

        #region general settings

        public virtual DbSet<GeneralSetting> GeneralSettings { get; set; }

        #endregion

        #region languages

        public virtual DbSet<Language> Languages { get; set; }

        #endregion

        #region projects
        public virtual DbSet<ProjectEnumPropertySelectedLanguage> ProjectEnumPropertySelectedLanguages { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectEnum> ProjectEnums { get; set; }
        public virtual DbSet<ProjectEnumProperty> ProjectEnumProperties { get; set; }
        public virtual DbSet<ProjectArea> ProjectAreas { get; set; }

        public virtual DbSet<ProjectSelectedLanguage> ProjectSelectedLanguages { get; set; }

        #endregion

        #region entities
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<EntityRelation> EntityRelations { get; set; }
        public virtual DbSet<EntitySelectedProjectArea> EntitySelectedProjectAreas { get; set; }
        public virtual DbSet<EntitySelectedProjectAreaSelectedFilter> EntitySelectedProjectAreaSelectedFilters { get; set; }

        public virtual DbSet<EntitySelectedLanguage> EntitySelectedLanguages { get; set; }

        #endregion

        #region properties
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyImageResizeInformation> PropertyImageResizeInformation { get; set; }

        public virtual DbSet<PropertySelectedLanguage> PropertySelectedLanguages { get; set; }

        #endregion

        #region users

        public virtual DbSet<User> Users { get; set; }

        #endregion

        #region Permission

        public virtual DbSet<Permission> Permissions { get; set; }

        #endregion

        #region roles

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleSelectedPermission> RoleSelectedPermissions { get; set; }
        public virtual DbSet<UserSelectedRole> UserSelectedRoles { get; set; }

        #endregion

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region seed data 

            #region users

            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                LatestEditDate = new DateTime(2025, 01, 01),
                EditCounts = 0,
                FirstName = "نام",
                LastName = "نام خانوادگی",
                PhoneNumber = "09121234567",
                Password = "12345678",
                CreateDate = new DateTime(2025, 01, 01),
                IsSuperAdmin = true,
                PhoneNumberActivationCode = "",
                PhoneNumberActivationCodeExpireTime = new DateTime(2025, 01, 01)
            });

            #endregion

            #region site settings

            modelBuilder
                .Entity<SiteSetting>()
                .HasData(new SiteSetting()
                {
                    Id = 1,
                    SiteName = "نام سایت شما",
                    LogoImageName = "logo.png",
                    SMSApiKey = "apiKey",
                    SMSTemplateName = "defaultVerification",
                    CreateDate = new DateTime(2025, 01, 01),
                    LatestEditDate = new DateTime(2025, 01, 01),
                    FavIconName = "favicon.ico"
                });

            #endregion

            #region general settings

            modelBuilder
                .Entity<GeneralSetting>()
                .HasData(new GeneralSetting()
                {
                    Id = 1,
                    CreateDate = new DateTime(2025, 01, 01),
                    LatestEditDate = new DateTime(2025, 01, 01),
                    DefaultSolutionName = "BaseCleanArchitectureTemplate",
                    DefaultSolutionLocation = @"C:\Users\surface\Projects\BaseCleanArchitectureTemplate\BaseCleanArchitectureTemplate.zip",
                });

            #endregion

            #endregion

            #region Cascade

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            #endregion

            RegisterQueryFilter(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        #region query filters

        private static void RegisterQueryFilter(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                // Ensure the type inherits from EntityId<T>
                if (!clrType.BaseType?.IsGenericType ?? true || clrType.BaseType.GetGenericTypeDefinition() != typeof(EntityId<>))
                {
                    continue;
                }

                // Create expression: entity => !entity.IsDelete
                var param = Expression.Parameter(clrType, "entity");
                var prop = Expression.PropertyOrField(param, nameof(SoftDelete.IsDelete));
                var entityNotDeleted = Expression.Lambda(Expression.Equal(prop, Expression.Constant(false)), param);

                // Apply query filter
                modelBuilder.Entity(clrType).HasQueryFilter(entityNotDeleted);
            }
        }

        #endregion

    }

}
