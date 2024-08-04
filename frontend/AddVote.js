import React, { useState } from 'react';
import axios from 'axios';

const AddVoteForm = () => {
  const [voteData, setVoteData] = useState({
    title: '',
    description: '',
  });

  const handleChange = (e) => {
    setVoteData({ ...voteData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post(`${process.env.REACT_APP_BACKEND_URL}/votes`, voteData);
      alert('Vote added successfully!');
      setVoteData({
        title: '',
        description: '',
      });
    } catch (error) {
      console.error('Error adding vote:', error);
      alert('Failed to add vote.');
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <label htmlFor="title">Title:</label>
      <input
        type="text"
        name="title"
        id="title"
        value={voteData.title}
        onChange={handleChange}
        required
      />
      <label htmlFor="description">Description:</label>
      <textarea
        name="description"
        id="description"
        value={voteData.description}
        onChange={handleChange}
        required
      />
      <button type="submit">Add Vote</button>
    </form>
  );
};

export default AddVoteForm;