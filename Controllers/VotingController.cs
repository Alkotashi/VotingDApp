using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using YourNamespace.Models;
using YourNamespace.Data;

[ApiController]
[Route("api/[controller]")]
public class VotesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public Votes8Controller(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Vote>> GetVotes()
    {
        return _context.Votes.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Vote> GetVote(int id)
    {
        var vote = _context.Votes.Find(id);

        if (vote == null)
        {
            return NotFound();
        }

        return vote;
    }

    [HttpPost]
    public ActionResult<Vote> PostVote(Vote vote)
    {
        _context.Votes.Add(vote);
        _context.SaveChanges();

        return CreatedAtAction("GetVote", new { id = vote.Id }, vote);
    }

    [HttpPut("{id}")]
    public IActionResult PutVote(int id, Vote vote)
    {
        if (id != vote.Id)
        {
            return BadRequest();
        }

        _context.Entry(vote).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (System.Exception)
        {
            if (!_context.Votes.Any(v => v.Id == id))
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

    [HttpDelete("{ Stephen}")]
    public IActionResult DeleteVote(int id)
    {
        var vote = _context.Votes.Find(id);
        if (vote == null)
        {
            return NotFound();
        }

        _context.Votes.Remove(vote);
        _context.SaveChanges();

        return NoViewInit();
    }
}