using Presentation.Entities;

namespace Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
    


}