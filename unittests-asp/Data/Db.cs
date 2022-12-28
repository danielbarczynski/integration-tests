using Microsoft.EntityFrameworkCore;

class Db : DbContext
{
    public Db(DbContextOptions<Db> options) : base(options)
    {

    }

    DbSet<PersonModel> Persons { get; set; }
}