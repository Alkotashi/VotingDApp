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

        Voter storage voter = voters[msg.sender];
        
        voter.voteId = _voteIndex;
        voter.voted = true;
        votes[_voteIndex].voteCount++;

        emit Voted(msg.sender, _voteIndex);
    }
    
    function restartVotingSession(string memory _newVotingName) public ownerOnly {
        for (uint i = 0; i < votes.length; i++) {
            delete votes[i];
        }
        delete votes;
        
        for (uint i = 0; i < votes.length; i++) {
            address voterAddress = address(uint160(i));
            delete voters[voterAddress];
        }
        
        votingName = _newVotingName;
        emit NewVotingSessionStarted(_newVotingName);
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