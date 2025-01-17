namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private static void UpdateEntities(DbContext? dbContext)
        {
            if (dbContext is null) 
            {
                return;
            }

            foreach(var entry in dbContext.ChangeTracker.Entries<IEntity>())
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = "Admin";
                    entry.Entity.CreatedAt= DateTime.UtcNow;
                    entry.Entity.ModifiedBy = "Admin";
                    entry.Entity.LastModified = DateTime.UtcNow;
                }
                else
                {
                    if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                    {
                        entry.Entity.ModifiedBy = "Admin";
                        entry.Entity.LastModified = DateTime.UtcNow;
                    }
                }
            }
        }
    }
}
