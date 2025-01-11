import React, { useState } from 'react';
import './App.css';
import SocialMediaApp from './components/SocialMediaApp.jsx';
import Login from './components/Login.jsx';

const App = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [token, setToken] = useState('');

    const handleLogin = (jwtToken) => {
        setIsLoggedIn(true);
        setToken(jwtToken);
    };

    const handleLogout = () => {
        setIsLoggedIn(false);
        setToken('');
    };

    return (
        <div className="App">
            <header>
                <h1>Social Media App</h1>
                {isLoggedIn ? (
                    <button onClick={handleLogout}>Logout</button>
                ) : (
                    <Login onLogin={handleLogin} />
                )}
            </header>
            {isLoggedIn && (
                <main>
                    <SocialMediaApp token={token} />
                </main>
            )}
        </div>
    );
};

export default App;