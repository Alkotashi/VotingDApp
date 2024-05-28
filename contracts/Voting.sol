pragma solidity ^0.8.0;

contract VotingContract {
    struct Vote {
        uint id;
        string name;
        uint voteCount;
    }

    struct Voter {
        bool authorized;
        bool voted;
        uint vote;
    }

    address public owner;
    string public votingName;
    mapping(address => Voter) public voters;
    Vote[] public votes;

    modifier ownerOnly() {
        require(msg.sender == owner, "Only the owner can call this.");
        _;
    }

    constructor(string memory _name) {
        owner = msg.sender;
        votingName = _name;
    }

    function addVote(string memory _name) external ownerOnly {
        votes.push(Vote(votes.length, _name, 0));
    }

    function authorize(address _person) external ownerOnly {
        voters[_person].authorized = true;
    }

    function vote(uint _voteIndex) external {
        require(!voters[msg.sender].voted, "You already voted.");
        require(voters[msg.sender].authorized, "You are not authorized.");

        voters[msg.sender].vote = _voteIndex;
        voters[msg.sender].voted = true;

        votes[_voteIndex].voteCount++;
    }

    function totalVotesFor(uint _voteIndex) external view returns (uint) {
        require(_voteIndex < votes.length, "No such vote.");
        return votes[_voteIndex].voteCount;
    }

    function getVote(uint _voteIndex) external view returns (uint, string memory, uint) {
        require(_voteIndex < votes.length, "No such vote.");
        return (votes[_voteIndex].id, votes[_voteIndex].name, votes[_voteIndex].voteCount);
    }

    function voterDetails(address _voter) external view returns (bool, bool, uint) {
        return (voters[_voter].authorized, voters[_voter].voted, voters[_voter].vote);
    }
}