using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data;
using SusuCMS.Data;

namespace SusuCMS
{
    public class DataContext : DbContext
    {
        public DataContext() : base("CmsDataContext") { }

        public DbSet<Site> Sites { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }
        public DbSet<Widget> Widgets { get; set; }
        public DbSet<PageWidget> PageWidgets { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }
        public DbSet<SiteLabel> SiteLabels { get; set; }
        public DbSet<PageLabel> PageLabels { get; set; }
        public DbSet<WidgetLabel> WidgetLabels { get; set; }
        public DbSet<SiteData> SiteData { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>().ToTable("Cms_AdminUser");
            modelBuilder.Entity<AdminRole>().ToTable("Cms_AdminRole");
            modelBuilder.Entity<Site>().ToTable("Cms_Site");
            modelBuilder.Entity<Page>().ToTable("Cms_Page");
            modelBuilder.Entity<Widget>().ToTable("Cms_Widget");
            modelBuilder.Entity<PageWidget>().ToTable("Cms_PageWidget");
            modelBuilder.Entity<Menu>().ToTable("Cms_Menu");
            modelBuilder.Entity<SystemLog>().ToTable("Cms_SystemLog");
            modelBuilder.Entity<SiteLabel>().ToTable("Cms_SiteLabel");
            modelBuilder.Entity<PageLabel>().ToTable("Cms_PageLabel");
            modelBuilder.Entity<WidgetLabel>().ToTable("Cms_WidgetLabel");
            modelBuilder.Entity<SiteData>().ToTable("Cms_SiteData");
            modelBuilder.Entity<Mail>().ToTable("Cms_Mail");
            modelBuilder.Entity<User>().ToTable("Cms_User");

            modelBuilder.Entity<Page>().HasOptional(i => i.Parent).WithMany(i => i.Children).HasForeignKey(i => i.ParentId);
            modelBuilder.Entity<Menu>().HasOptional(i => i.Parent).WithMany(i => i.Children).HasForeignKey(i => i.ParentId);
            modelBuilder.Entity<PageWidget>().HasKey(i => new { i.PageId, i.WidgetId });
        }

        public void Delete(object entity)
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public void Delete<T>(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                Delete(item);
            }
        }
    }
}
