import React, { useState } from 'react';
import axios from 'axios';

const AddVoteForm = () => {
  const [newVote, setNewVote] = useState({
    title: '',
    description: '',
  });

  const handleInputChange = (event) => {
    setNewVote({ ...newVote, [event.target.name]: event.target.value });
  };

  const handleVoteSubmission = async (event) => {
    event.preventDefault();
    const VOTE_ENDPOINT = `${process.env.REACT_APP_BACKEND_URL}/votes`;
    try {
      await axios.post(VOTE_ENDPOINT, newVote);
      alert('Vote added successfully!');
      resetVoteForm();
    } catch (error) {
      console.error('Error adding vote:', error);
      alert('Failed to add vote.');
    }
  };

  const resetVoteForm = () => {
    setNewVote({
      title: '',
      description: '',
    });
  };

  return (
    <form onSubmit={handleVoteSubmission}>
      <label htmlFor="title">Title:</label>
      <input
        type="text"
        name="title"
        id="title"
        value={newVote.title}
        onChange={handleInputChange}
        required
      />
      <label htmlFor="description">Description:</label>
      <textarea
        name="description"
        id="description"
        value={newVote.description}
        onChange={handleInputChange}
        required
      />
      <button type="submit">Add Vote</button>
    </form>
  );
};

export default AddVoteForm;