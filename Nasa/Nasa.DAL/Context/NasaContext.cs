using Microsoft.EntityFrameworkCore;
using Nasa.DAL.Entities;

namespace Nasa.DAL.Context;

public class NasaContext: DbContext
{
    public NasaContext(DbContextOptions<NasaContext> options): base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
}