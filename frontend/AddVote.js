import React, { useState } from 'react';
import axios from 'axios';

const AddVoteForm = () => {
  const [voteFormData, setVoteFormData] = useState({
    title: '',
    description: '',
  });

  const handleInputChange = (event) => {
    setVoteFormData({ ...voteFormData, [event.target.name]: event.target.value });
  };

  const handleSubmitVote = async (event) => {
    event.preventDefault();
    const VOTE_API_ENDPOINT = `${process.env.REACT_APP_BACKEND_URL}/votes`;
    try {
      await axios.post(VOTE_API_ENDPOINT, voteFormData);
      alert('Vote added successfully!');
      resetFormData();
    } catch (error) {
      console.error('Error adding vote:', error);
      alert('Failed to add vote.');
    }
  };

  const resetFormData = () => {
    setVoteFormData({
      title: '',
      description: '',
    });
  };

  return (
    <form onSubmit={handleSubmitVote}>
      <label htmlFor="title">Title:</label>
      <input
        type="text"
        name="title"
        id="title"
        value={voteFormData.title}
        onChange={handleInputChange}
        required
      />
      <label htmlFor="description">Description:</label>
      <textarea
        name="description"
        id="description"
        value={voteFormData.description}
        onChange={handleInputChange}
        required
      />
      <button type="submit">Add Vote</button>
    </form>
  );
};

export default AddVoteForm;