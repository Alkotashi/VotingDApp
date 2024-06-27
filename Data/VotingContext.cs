using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        try
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("The database connection string is not configured.");
            }

            options.UseSqlServer(connectionString);
        }
        catch (Exception ex)
        {
            // Log the exception (Consider using a logging library)
            Console.Error.WriteLine($"An error occurred while configuring the database: {ex.Message}");
            throw; // Re-throwing the exception to make it clear that the application cannot continue without proper database configuration
        }
    }
}

public static class SimpleCache
{
    private static Dictionary<string, object> _cache = new Dictionary<string, object>();

    public static T GetOrAdd<T>(string key, Func<T> addItemCallback)
    {
        try
        {
            if (_cache.ContainsKey(key))
            {
                return (T)_cache[key];
            }

            T item = addItemCallback();
            _cache[key] = item;
            return item;
        }
        catch (Exception ex)
        {
            // Log the exception (Consider using a logging framework here)
            Console.Error.WriteLine($"An error occurred in the caching mechanism: {ex.Message}");
            throw; // Consider whether you want to throw the exception further or handle it gracefully
        }
    }
}

public class VotingService
{
    private VotingContext _context;

    public VotingService(VotingContext context)
    {
        _context = context;
    }

    public int GetTotalVotes()
    {
        string cacheKey = "totalVotes";
        try
        {
            return SimpleCache.GetOrAdd(cacheKey, () =>
            {
                return _context.Votes.Count();
            });
        }
        catch (Exception ex)
        {
            // Log the detailed error message, consider using a logging library
            Console.Error.WriteLine($"An error occurred when trying to get the total number of votes: {ex.Message}");
            // Depending on your use case, you might want to return a default value or rethrow the exception
            throw; // Here is shown as re-throwing, adjust according to your error handling policy
        }
    }
}