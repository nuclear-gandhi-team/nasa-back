using Microsoft.EntityFrameworkCore;
using Nasa.DAL.Entities;

namespace Nasa.DAL.Context;

public class NasaContext: DbContext
{
    public NasaContext(DbContextOptions<NasaContext> options): base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Settlement> Settlements => Set<Settlement>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Role> Roles => Set<Role>();
}