using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Vote
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Title { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
    public List<VoteOption> Options { get; set; } = new List<VoteOption>();
}

public class VoteOption
{
    public int Id { get; set; }
    [Required]
    [StringLength(200)]
    public string Text { get; set; }
    public int VoteId { get; set; }
    public Vote Vote { get; set; }
}