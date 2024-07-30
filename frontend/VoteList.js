import React, { useEffect, useState } from 'react';
import axios from 'axios';

function VoteList() {
  const [votes, setVotes] = useState([]);
  const [error, setError] = useState('');
  const [isLoading, setIsLoading] = useState(true); // Track loading state

  const backendURL = process.env.REACT_APP_BACKEND_URL;

  useEffect(() => {
    async function fetchVotes() {
      setIsLoading(true); // Start loading state
      try {
        const response = await axios.get(`${backendURL}/votes`);
        if (response.data && Array.isArray(response.data)) { // Check if response.data is an array
          setVotes(response.data);
        } else {
          throw new Error('Invalid format of fetched data'); // Handle unexpected format of data
        }
      } catch (error) {
        if (error.response) {
          // The request was made and the server responded with a status code
          // That falls out of the range of 2xx
          console.error('Error data:', error.response.data);
          console.error('Error status:', error.response.status);
          setError(`Server responded with status code: ${error.response.status}. Please try again later.`);
        } else if (error.request) {
          // The request was made but no response was received
          console.error('No response:', error.request);
          setError('Server is not responding. Please check your network connection or try again later.');
        } else {
          // Something else caused the error
          console.error('Error', error.message);
          setError('An unexpected error occurred. Please try again.');
        }
      } finally {
        setIsLoading(false); // End loading state
      }
    }

    fetchVotes();
  }, [backendURL]);

  // Display loading indicator when data is being fetched
  if (isLoading) {
    return <div>Loading votes...</div>;
  }

  return (
    <div>
      <h2>Vote List</h2>
      {error ? (
        <div style={{ color: 'red', marginBottom: '15px' }}>
          <strong>Error: </strong> {error}
        </div>
      ) : votes.length === 0 ? ( // Check if votes array is empty
        <p>No votes found.</p>
      ) : (
        <ul>
          {votes.map((vote) => (
            <li key={vote.id}>
              {vote.name}: {vote.count}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default VoteList;