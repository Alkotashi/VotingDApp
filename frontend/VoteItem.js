import React, { useState } from 'react';

const VoteComponent = ({ voteDetails, onVote }) => {
  const [selectedOption, setSelectedOption] = useState('');

  const handleOptionChange = (event) => {
    setSelectedOption(event.target.value);
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    onVote(selectedOption);
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
    </div>
  );
};

export default VoteComponent;