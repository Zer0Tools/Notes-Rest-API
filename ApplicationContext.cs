
using Zer0Tools.NotesWebAPI.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
    public DbSet<NoteModel> Notes => Set<NoteModel>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

}
