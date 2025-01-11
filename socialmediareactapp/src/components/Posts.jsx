import React, { useEffect, useState } from 'react';
import axios from 'axios';

const Posts = ({ token }) => {
    const [posts, setPosts] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5002/api/posts', {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        })
            .then(response => {
                setPosts(response.data);
            })
            .catch(error => {
                console.error('Failed to fetch posts:', error);
            });
    }, [token]);

    return (
        <div>
            <h2>Posts</h2>
            <div className={"posts"}>
                {posts.map(post => (
                    <div key={post.id} className="post">
                        <p>{post.title}</p>
                        
                        <span>{post.content}</span>
                        <p>Likes: 0</p><button style="padding: 5px 10px;">Like</button>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default Posts;
