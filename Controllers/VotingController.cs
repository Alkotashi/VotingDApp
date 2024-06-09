using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using YourNamespace.Models;
using YourNamespace.Data;

[ApiController]
[Route("api/[controller]")]
public class VotesController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public VotesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Vote>> GetAllVotes()
    {
        return _dbContext.Votes.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Vote> GetVoteById(int id)
    {
        var vote = _dbContext.Votes.Find(id);

        if (vote == null)
        {
            return NotFound();
        }

        return vote;
    }

    [HttpPost]
    public ActionResult<Vote> CreateVote(Vote vote)
    {
        _dbContext.Votes.Add(vote);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetVoteById), new { id = vote.Id }, vote);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateVote(int id, Vote vote)
    {
        if (id != vote.Id)
        {
            return BadRequest();
        }

        _dbContext.Entry(vote).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        try
        {
            _dbContext.SaveChanges();
        }
        catch (System.Exception)
        {
            if (!_dbContext.Votes.Any(v => v.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteVote(int id)
    {
        var vote = _dbContext.Votes.Find(id);
        if (vote == null)
        {
            return NotFound();
        }

        _dbContext.Votes.Remove(vote);
        _dbContext.SaveChanges();

        return NoContent();
    }
}