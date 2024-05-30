// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

/**
 * @title A simple voting contract.
 */
contract VotingContract {
    // Struct for holding individual vote details.
    struct Vote {
        uint id;
        string name;
        uint voteCount;
    }

    // Struct for holding voter details.
    struct Voter {
        bool authorized;
        bool voted;
        uint voteId;
    }

    // State variables.
    address public owner;
    string public votingName;
    mapping(address => Voter) public voters;
    Vote[] public votes;

    // Modifier to restrict access to the owner.
    modifier ownerOnly() {
        require(msg.sender == owner, "Only the owner can call this.");
        _;
    }

    /**
     * @dev Constructor for initializing the VotingContract.
     * @param _name Name of the voting event.
     */
    constructor(string memory _name) {
        owner = msg.sender;
        votingName = _name;
    }

    /**
     * @dev Adds a vote option.
     * @param _name Name of the vote option.
     */
    function addVote(string memory _name) external ownerOnly {
        votes.push(Vote(votes.length, _name, 0));
    }

    /**
     * @dev Authorizes a voter.
     * @param _person Address of the voter to authorize.
     */
    function authorize(address _person) external ownerOnly {
        voters[_person].authorized = true;
    }

    /**
     * @dev Records a vote for an authorized voter.
     * @param _voteIndex Index of the vote being cast.
     */
    function vote(uint _voteIndex) external {
        require(!voters[msg.sender].voted, "You already voted.");
        require(voters[msg.sender].authorized, "You are not authorized.");
        require(_voteIndex < votes.length, "Invalid vote option.");

        Voter storage voter = voters[msg.sender];
        
        voter.voteId = _voteIndex;
        voter.voted = true;
        votes[_voteIndex].voteCount++;
    }

    /**
     * @dev Returns total votes for a given vote option.
     * @param _voteIndex Index of the vote option.
     * @return Total number of votes.
     */
    function totalVotesFor(uint _voteIndex) external view returns (uint) {
        require(_voteIndex < votes.length, "No such vote.");
        return votes[_voteIndex].voteCount;
    }

    /**
     * @dev Fetches details of a vote option.
     * @param _voteIndex Index of the vote option.
     * @return Tuple containing vote details (id, name, voteCount).
     */
    function getVote(uint _voteIndex) external view returns (uint, string memory, uint) {
        require(_voteIndex < votes.length, "No such vote.");
        return (votes[_voteIndex].id, votes[_voteIndex].name, votes[_voteIndex].voteCount);
    }

    /**
     * @dev Returns the details of a voter.
     * @param _voter Address of the voter.
     * @return Tuple containing the authorization status, voting status, and voted option index.
     */
    function voterDetails(address _voter) external view returns (bool, bool, uint) {
        Voter memory voter = voters[_voter];
        return (voter.authorized, voter.voted, voter.voteId);
    }
}