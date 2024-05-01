
using Zer0Tools.NotesWebAPI.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
    public DbSet<NoteModel> Notes => Set<NoteModel>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<NoteModel>()
        //     .Property(b => b.Id)
        //     .HasDefaultValueSql("NewGuid()");
        // modelBuilder.Entity<UserModel>()
        //     .Property(b => b.Id)
        //     .HasDefaultValueSql("NewGuid()");    
    }

}