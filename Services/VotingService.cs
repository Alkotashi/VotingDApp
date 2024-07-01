using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
import System.Linq;

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
}

public class VoteService : IVoteService
{
    private readonly List<Vote> votes = new();

    public IEnumerable<Vote> GetAllVotes() => votes;

    public Vote GetVoteById(int id) => votes.FirstOrDefault(v => v.Id == id);

    public void CreateVote(string candidate)
    {
        votes.Add(new Vote
        {
            Id = !votes.Any() ? 1 : votes.Max(v => v.Id) + 1,
            Candidate = candidate,
            TimeStamp = DateTime.UtcNow
        });
    }

    public void CastVote(int id)
    {
        var vote = GetVoteById(id);
        if (vote != null)
        {
            Console.WriteLine($"Vote casted for {vote.Candidate} at {DateTime.UtcNow}");
        }
    }
}

[ApiController]
[Route("[controller]")]
public class VoteController : ControllerBase
{
    private readonly IVoteService voteService;

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
        voteService.CreateTextBox(candidate);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult CastVote(int id)
    {
        voteService.CastVote(id);
        return Ok();
    }
}