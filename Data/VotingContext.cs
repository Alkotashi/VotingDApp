using Microsoft.EntityFrameworkCore;
using System;

public class Vote
{
    public int Id { get; set; }
    public string VoterName { get; set; }
    public DateTime VoteTime { get; set; }
}

public class VotingContext : DbContext
{
    public DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("The database connection string is not configured.");
        }

        options.UseSqlServer(connectionString);
    }
}