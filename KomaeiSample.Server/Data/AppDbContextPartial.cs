namespace KomaeiSample.Server.Data;
public partial class AppDbContext
{
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        TrimStringProperties();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void TrimStringProperties()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            foreach (var property in entry.Properties)
            {
                if (property.CurrentValue is string stringValue)
                {
                    property.CurrentValue = stringValue.Trim();
                }
            }
        }
    }
}