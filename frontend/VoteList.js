import React, { useEffect, useState } from 'react';
import axios from 'axios';

function VoteList() {
  const [votes, setVotes] = useState([]);

  const backendURL = process.env.REACT_APP_BACKEND_URL;

  useEffect(() => {
    async function fetchVotes() {
      try {
        const response = await axios.get(`${backendURL}/votes`);
        setVotes(response.data);
      } catch (error) {
        console.error('Error fetching votes:', error);
      }
    }

    fetchVotes();
  }, [backendURL]);

  return (
    <div>
      <h2>Vote List</h2>
      <ul>
        {votes.map((vote) => (
          <li key={vote.id}>
            {vote.name}: {vote.count}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default VoteList;