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
        uint voteId;
    }

    address public owner;
    string public votingName;
    mapping(address => Voter) public voters;
    Vote[] public votes;

    event VoteAdded(uint id, string name);
    event VoterAuthorized(address voter);
    event Voted(address voter, uint voteId);
    event NewVotingSessionStarted(string newVotingName);

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
        emit VoteAdded(votes.length - 1, _name);
    }

    function authorize(address _person) external ownerOnly {
        voters[_person].authorized = true;
        emit VoterAuthorized(_person);
    }

    function vote(uint _voteIndex) external {
        require(!voters[msg.sender].voted, "You already voted.");
        require(voters[msg.sender].authorized, "You are not authorized.");
        require(_voteIndex < votes.length, "Invalid vote option.");

        processVote(msg.sender, _voteIndex);
    }
    
    function processVote(address voterAddress, uint _voteIndex) private {
        Voter storage voter = voters[voterAddress];
        
        voter.voteId = _voteIndex;
        voter.voted = true;
        votes[_oneVoteIndex].voteCount++;

        emit Voted(voterAddress, _voteIndex);
    }

    function restartVotingSession(string memory _newVotingName) public ownerOnly {
        resetVotes();
        resetVoters();
        
        votingName = _newVotingName;
        emit NewVotingSessionStarted(_newVotingName);
    }

    function resetVotes() private {
        while(votes.length > 0) {
            votes.pop();
        }
    }

    function resetVoters() private {
        // This is a non-scalable solution and kept for illustrative purposes.
        // A better approach might be to track voter addresses in an array and reset accordingly.
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
    Voter memory voter = voters[_voter];
    return (voter.authorized, voter.voted, voter.voteId);
    }
}