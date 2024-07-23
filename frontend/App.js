import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './HomePage';
import AboutPage from './AboutPage';
import NotFoundPage from './NotFoundMenu';

class ErrorBoundary extends React.Component {
  constructor(props) {
    super(props);
    this.state = { hasEncounteredError: false };
  }

  static getDerivedStateFromError(error) {
    return { hasEncounteredError: true };
  }

  componentDidCatch(error, errorInfo) {
    console.error("ErrorBoundary encountered an error", error, errorInfo);
  }

  render() {
    if (this.state.hasEncounteredError) {
      return <h1>Oops, something went wrong.</h1>;
    }

    return this.props.children; 
  }
}

function VotingApp() {
  return (
    <Router>
      <ErrorBoundary>
        <div>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/about" element={<AboutPage />} />
            <Route path="*" element={<NotFoundPage />} />
          </Routes>
        </div>
      </ErrorBoundary>
    </Router>
  );
}

export default VotingApp;