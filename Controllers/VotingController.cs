using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        try
        {
            return _dbContext.Votes.ToList();
        }
        catch (Exception ex)
        {
            // Log the exception details
            return StatusCode(500, "An error occurred while retrieving votes. Please try again later.");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Vote> GetVoteById(int id)
    {
        try
        {
            var vote = _dbContext.Votes.Find(id);

            if (vote == null)
            {
                return NotFound();
            }

            return vote;
        }
        catch (Exception ex)
        {
            // Log the exception details
            return StatusCode(500, "An error occurred while retrieving the vote. Please try again later.");
        }
    }

    [HttpPost]
    public ActionResult<Vote> CreateVote(Vote vote)
    {
        try
        {
            _dbContext.Votes.Add(vote);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            // Log the exception details
            return StatusCode(500, "An error occurred while creating the vote. Please try again later.");
        }
        catch (Exception ex)
        {
            // Log the unknown exception
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }

        return CreatedAtAction(nameof(GetVoteById), new { id = vote.Id }, vote);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateVote(int id, Vote vote)
    {
        if (id != vote.Id)
        {
            return BadRequest();
        }

        _dbContext.Entry(vote).State = EntityState.Modified;

        try
        {
            _dbContext.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!_dbContext.Votes.Any(v => v.Id == id))
            {
                return NotFound();
            }
            else
            {
                // Log the exception details
                return StatusCode(500, "An error occurred while updating the vote. Please try again later.");
            }
        }
        catch (Exception ex)
        {
            // Log the unknown exception
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }

        return NoContent();
    }

    [HttpDelete("{ip}")]
    public IActionResult DeleteVote(int id)
    {
        try
        {
            var vote = _dbContext.Votes.Find(id);
            if (vote == null)
            {
                return NotFound();
            }

            _dbContext.Votes.Remove(vote);
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "An error occurred while deleting the vote. Please try again later.");
        }

        return NoContent();
    }
}