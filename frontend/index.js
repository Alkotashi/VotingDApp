import React, { useState, useEffect } from 'react';

const App = () => {
  const [voterCount, setVoterCount] = useState(0);

  const fetchInitialVoterCount = () => {
    return new Promise((resolve) => {
      setTimeout(() => resolve(100), 1000);
    });
  };

  useEffect(() => {
    fetchInitialVoterCount().then(initialCount => {
      setVoterCount(initialCount);
    });
  }, []);

  const handleNewVote = () => {
    setVoterCount(prevCount => prevCount + 1);
  };

  return (
    <div>
      <h1>Welcome to the Voting DApp</h1>
      <p>Current Voter Count: {voterCount}</p>
      <button onClick={handleNewVote}>Cast Vote</button>
    </div>
  );
};
```

```jsx
import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';

const rootElement = document.getElementById('root');
ReactDOM.render(<App />, rootElement);