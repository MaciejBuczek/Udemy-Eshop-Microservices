namespace Ordering.Infrastructure.Data.Extensions
{
    internal static class EntityEntryExtensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entityEntry) =>
            entityEntry.References.Any(e => e.TargetEntry != null 
                && e.TargetEntry.Metadata.IsOwned() 
                && (e.TargetEntry.State == EntityState.Added || e.TargetEntry.State == EntityState.Modified));
    }
}
