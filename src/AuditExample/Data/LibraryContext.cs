using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AuditExample.ElasticSearch;
using AuditExample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Nest;

namespace AuditExample.Data
{
    public class LibraryContext : DbContext
    {
        private readonly IElasticSearch _elasticSearch;

        public LibraryContext(DbContextOptions<LibraryContext> options, IElasticSearch elasticSearch) : base(options)
        {
            _elasticSearch = elasticSearch;
        }
        
        

        public DbSet<Book> Books { get; set; }


        public override int SaveChanges()
        {
            try
            {
                var modifiedEnities = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();
                var now = DateTime.UtcNow;
                foreach (var modifiedEnity in modifiedEnities)
                {
                    var entityName = modifiedEnity.Entity.GetType().Name;
                    var primaryKey = modifiedEnity.OriginalValues.Properties
                        .FirstOrDefault(p => p.IsPrimaryKey() == true)
                        ?.Name;

                    foreach (var property in modifiedEnity.OriginalValues.Properties)
                    {
                        var originalValue = modifiedEnity.OriginalValues[property.Name]?.ToString() ?? "Null Value";
                        var currentValue = modifiedEnity.CurrentValues[property.Name]?.ToString() ?? "Null Value";
                        if (originalValue != currentValue)
                        {
                            ChangeLog log = new ChangeLog()
                            {
                                EntityName = entityName,
                                PrimaryKeyValue = modifiedEnity.OriginalValues[primaryKey].ToString(),
                                PropertyName = property.Name,
                                OldValue = originalValue,
                                NewValue = currentValue,
                                DateChanged = now,
                                State = EnumState.Update
                            };
                            _elasticSearch.CheckExistsAndInsert(log);
                        }
                    }
                }

                return base.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return base.SaveChanges();
        }
    }
}