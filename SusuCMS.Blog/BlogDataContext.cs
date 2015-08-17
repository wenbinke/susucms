using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data;
using SusuCMS.Blog.Data;

namespace SusuCMS.Blog
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ArticleDraft> ArticleDrafts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().ToTable("Blog_Article");
            modelBuilder.Entity<Category>().ToTable("Blog_Category");
            modelBuilder.Entity<Tag>().ToTable("Blog_Tag");
            modelBuilder.Entity<Comment>().ToTable("Blog_Comment");
            modelBuilder.Entity<Contact>().ToTable("Blog_Contact");
            modelBuilder.Entity<ArticleDraft>().ToTable("Blog_ArticleDraft");

            modelBuilder.Entity<Category>().HasOptional(i => i.Parent).WithMany(i => i.Children).HasForeignKey(i => i.ParentId);
            modelBuilder.Entity<Comment>().HasOptional(i => i.Parent).WithMany(i => i.Children).HasForeignKey(i => i.ParentId);

            modelBuilder.Entity<Article>().HasMany(i => i.Tags).WithMany(i => i.Articles).Map(
                i =>
                {
                    i.MapLeftKey("ArticleId");
                    i.MapRightKey("TagId");
                    i.ToTable("Blog_ArticleTag");
                });

        }

        public void Delete(object entity)
        {
            Entry(entity).State = EntityState.Deleted;
        }
    }
}
