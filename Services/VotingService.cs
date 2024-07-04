using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

public class Vote
{
    public int Id { get; set; }
    public string Candidate { get; set; }
    public DateTime TimeStamp { get; set; }
}

public interface IVoteService
{
    IEnumerable<Vote> GetAllVotes();
    Vote GetVoteById(int id);
    void CreateVote(string candidate);
    void CastVote(int id);
    IDictionary<string, int> GetVoteCounts(); 
}

public class VoteService : IVoteService
{
    private readonly List<Vote> votes = new();
    
    public IEnumerable<Vote> GetAllVotes()
    {
        Console.WriteLine($"{DateTime.UtcNow}: Retrieving all votes.");
        return votes;
    }

    public Vote GetVoteById(int id)
    {
        var vote = votes.FirstOrDefault(v => v.Id == id);
        Console.WriteLine(vote != null ? $"{DateTime.UtcNow}: Retrieved vote for {vote.Candidate}." : $"{DateTime.UtcNow}: Vote with ID {id} not found.");
        return vote;
    }

    public void CreateVote(string candidate)
    {
        var newVote = new Vote
        {
            Id = votes.Any() ? votes.Max(v => v.Id) + 1 : 1,
            Candidate = candidate,
            TimeStamp = DateTime.UtcNow
        };
        votes.Add(newVote);
        Console.WriteLine($"{DateTime.UtcNow}: Created vote for {candidate}.");
    }

    public void CastVote(int id)
    {
        var vote = GetVoteById(id);
        if (vote != null)
        {
            Console.WriteLine($"{DateTime.UtcNow}: Vote casted for {vote.Candidate}.");
        }
    }
    
    public IDictionary<string, int> GetVoteCounts()
    {
        var counts = votes.GroupBy(v => v.Candidate)
                          .ToDictionary(g => g.Key, g => g.Count());
        Console.WriteLine($"{DateTime.UtcNow}: Retrieved vote counts.");
        return counts;
    }
}

[ApiController]
[Route("[controller]")]
public class VoteController : ControllerBase
{
    private readonly IVote = new();
    
    public IEnumerable<Vote> GetAllVotes()
    {
        Console.WriteLine($"{DateTime.UtcNow}: Retrieving all votes.");
        return votes;
    }Service voteService;

    public VoteController(IVoteService voteService)
    {
        this.voteService = voteService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Vote>> GetAllVotes()
    {
        return Ok(voteService.GetAllVotes());
    }

    [HttpGet("{id}")]
    public ActionResult<Vote> GetVote(int id)
    {
        var vote = voteService.GetVoteById(id);
        if (vote == null) return NotFound();
        return Ok(vote);
    }

    [HttpPost]
    public IActionResult CreateVote([FromBody] string candidate)
    {
        voteService.CreateVote(candidate);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult CastVote(int id)
    {
        voteService.CastVote(id);
        return Ok();
    }

    [HttpGet("counts")]
    public IActionResult GetVoteCounts()
    {
// The "IVote = new();" appears to be a mistakenly copied line fragment; it does not belong to the code and does not make sense in the given context. It has been removed from the output.
        return Ok(voteService.GetVoteCounts());
    }
}