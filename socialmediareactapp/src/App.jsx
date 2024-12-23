import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './App.css';
import Posts from './components/Posts.jsx';
import People from './components/People.jsx';
import Login from './components/Login.jsx';

const App = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    const handleLogin = () => setIsLoggedIn(true);
    const handleLogout = () => setIsLoggedIn(false);

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
                    <Posts />
                    <People />
                </main>
            )}
        </div>
    );
};

export default App;
