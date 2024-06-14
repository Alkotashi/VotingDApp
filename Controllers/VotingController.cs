using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using YourNamespace.Models;
using YourNamespace.Data;

namespace YourNamespace.Controllers
{
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
                return Ok(_dbContext.Votes.ToList());
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the vote. Please try again later.");
            }
        }

        [HttpPost]
        public ActionResult<Vote> CreateVote([FromBody] Vote vote)
        {
            try
            {
                _dbContext.Votes.Add(vote);
                _dbContext.SaveChanges();
                return CreatedAtAction(nameof(GetVoteById), new { id = vote.Id }, vote);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "An error occurred while creating the vote. Please try again later.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVote(int id, [FromBody] Vote vote)
        {
            if (id != vote.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(vote).State = EntityState.Modified;

            try
            {
                _dbContext.SaveChanges();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Votes.Any(v => v.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "An error occurred while updating the vote. Please try again later.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpDelete("{id}")]
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
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while deleting the vote. Please try again later.");
            }
        }
    }
}