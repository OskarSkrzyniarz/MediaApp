import React, { useState, useEffect } from 'react';
import axios from 'axios';

const People = () => {
    const [people, setPeople] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5003/api/people')
            .then(response => setPeople(response.data))
            .catch(error => console.error('Error fetching people:', error));
    }, []);

    return (
        <section>
            <h2>People</h2>
            <ul>
                {people.map(person => (
                    <li key={person.id}>{person.name}</li>
                ))}
            </ul>
        </section>
    );
};

export default People;
