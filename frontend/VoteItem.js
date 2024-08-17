import React, { useState, useEffect } from 'react';

const VoteComponent = ({ voteDetails, onVote }) => {
  const [selectedOption, setSelectedOption] = useState('');
  const [voteSubmitted, setVoteSubmitted] = useState(false);
  const [voteResults, setVoteResults] = useState({});
  const [error, setError] = useState('');

  useEffect(() => {
    // Reset error when the user selects an option
    if (selectedOption) {
      setError('');
    }
  }, [selectedOption]);

  const handleOptionChange = (event) => {
    setSelectedOption(event.target.value);
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    if (!selectedOption) {
      setError("Please select an option before submitting.");
      return;
    }
    try {
      const newVoteResults = { ...voteResults };
      newVoteResults[selectedOption] = (newVoteResults[selectedOption] || 0) + 1;
      setVoteResults(newVoteResults);

      onVote(selectedOption);
      setVoteSubmitted(true);
    } catch (e) {
      // Log the error to the console for debugging purposes
      console.error("Error submitting the vote: ", e);
      setError("An error occurred while submitting your vote. Please try again.");
    }
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
        {error && <div style={{ color: 'red', marginTop: '10px' }}>{error}</div>}
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