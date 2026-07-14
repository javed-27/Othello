import { useState, useEffect } from 'react';
import './App.css';

function App() {
  const [backendConnected, setBackendConnected] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    // Check backend health
    fetch('http://localhost:5000/api/health')
      .then(response => response.json())
      .then(data => {
        if (data.status === 'OK') {
          setBackendConnected(true);
        }
        setLoading(false);
      })
      .catch(error => {
        console.error('Failed to connect to backend:', error);
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <div className="app">Loading...</div>;
  }

  return (
    <div className="app">
      <h1>Othello</h1>
      {backendConnected ? (
        <p>Backend Connected</p>
      ) : (
        <p>Backend Connection Failed</p>
      )}
    </div>
  );
}

export default App;