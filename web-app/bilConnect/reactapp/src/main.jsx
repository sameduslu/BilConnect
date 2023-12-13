import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import App from './App.jsx';
import './index.css';
import LoginPage from './Login';

const root = ReactDOM.createRoot(document.getElementById('root'));

const handleLogin = () => {
  localStorage.setItem('isAuthenticated', 'true');
};

const isAuthenticated = localStorage.getItem('isAuthenticated') === 'true';

root.render(
  <React.StrictMode>
    <Router>
      <Routes>
        {!isAuthenticated && (
          <Route
            path="/"
               element={
              <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh', backgroundColor: '#1c1c1c' }}>
                    <LoginPage onLogin={handleLogin} />
              </div>
            }
          />
        )}
        <Route
            path="/main"
            element={
                <div style={{ backgroundColor: 'white' }}>
                    <App />
                </div>            
            }
          />
      </Routes>
    </Router>
  </React.StrictMode>
);
