import React, { useState } from 'react';

const VoteComponent = ({ voteDetails, onVote }) => {
  const [selectedOption, setSelectedOption] = useState('');
  const [voteSubmitted, setVoteSubmitted] = useState(false);
  const [voteResults, setVoteResults] = useState({});

  const handleOptionChange = (event) => {
    setSelectedOption(event.target.value);
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    if (!selectedOption) {
      alert("Please select an option before submitting.");
      return;
    }
    const newVoteResults = { ...voteResults };
    newVoteResults[selectedOption] = (newVoteResults[selectedOption] || 0) + 1;
    setVoteResults(newVoteResults);
    onVote(selectedOption);
    setVoteSubmitted(true);
  };

  return (
    <div>
      <h2>{voteDetails.question}</h2>
      <form onSubmit={handleSubmit}>
        {voteDetails.options.map((option) => (
          <div key={option.id}>
            <input
              type="radio"
              id={option.id}
              name="vote"
              value={option.value}
              onChange={handleOptionChange}
              checked={selectedOption === option.value}
            />
            <label htmlFor={option.id}>{option.label}</label>
          </div>
        ))}
        <button type="submit">Vote</button>
      </form>
      {voteSubmitted && (
        <div>
          <h3>Results:</h3>
          {voteDetails.options.map((option) => (
            <div key={option.id}>{`${option.label}: ${voteResults[option.value] || 0}`}</div>
          ))}
        </div>
      )}
    </div>
  );
};

export default VoteComponent;