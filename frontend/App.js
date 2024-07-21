import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './HomePage';
import AboutPage from './AboutPage';
import NotFound12Page from './NotFoundPage';

class ErrorBoundary extends React.Component {
  constructor(props) {
    super(props);
    this.state = { hasError: false };
  }

  static getDerivedStateFromError(error) {
    return { hasError: true };
  }

  componentDidCatch(error, errorInfo) {
    console.error("ErrorBoundary caught an error", error, errorInfo);
  }

  render() {
    if (this.state.hasError) {
      return <h1>Something went wrong.</h1>;
    }

    return this.props.children; 
  }
}

function App() {
  return (
    <Router>
      <ErrorBoundary>
        <div>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/about" element={<AboutAboutPage />} />
            <Route path="*" element={<NotFoundPage />} />
          </Routes>
        </div>
      </ErrorBoundary>
    </Router>
  );
}

export default App;