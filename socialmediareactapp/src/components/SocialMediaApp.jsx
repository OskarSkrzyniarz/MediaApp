import React, { useState, useEffect } from 'react';
import axios from 'axios';

const API_BASE = {
    POSTS: 'http://localhost:5002/api/posts',
    PEOPLE: 'http://localhost:5003/api/people'
};

function SocialMediaApp({ token }) {
    const [posts, setPosts] = useState([]);
    const [users, setUsers] = useState([]);
    const [newPost, setNewPost] = useState({ title: '', content: '' });
    const [likes, setLikes] = useState({});

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

    useEffect(() => {
        fetchPosts();
        fetchUsers();
    }, []);

    const fetchPosts = async () => {
        try {
            const response = await axios.get(API_BASE.POSTS);
            setPosts(response.data);
            const initialLikes = response.data.reduce((acc, post) => {
                acc[post.id] = 0;
                return acc;
            }, {});
            setLikes(initialLikes);
        } catch (error) {
            console.error('Error fetching posts:', error);
        }
    };

    const fetchUsers = async () => {
        try {
            const response = await axios.get(API_BASE.PEOPLE);
            setUsers(response.data);
        } catch (error) {
            console.error('Error fetching users:', error);
        }
    };

    const handlePostCreation = async () => {
        if (!newPost.title || !newPost.content) {
            alert('Both title and content are required!');
            return;
        }

        try {
            const response = await axios.post(API_BASE.POSTS, newPost);
            setPosts([...posts, response.data]);
            setNewPost({ title: '', content: '' });
        } catch (error) {
            console.error('Error creating post:', error);
        }
    };

    const handleLike = (postId) => {
        setLikes((prevLikes) => ({
            ...prevLikes,
            [postId]: prevLikes[postId] + 1,
        }));
    };

    return (
        <div style={{ padding: '20px' }}>
            <section className={"post-creation"} style={{ marginBottom: '40px' }}>
                <h2>Create a Post</h2>
                <input
                    type="text"
                    placeholder="Title"
                    value={newPost.title}
                    onChange={(e) => setNewPost({ ...newPost, title: e.target.value })}
                    style={{ display: 'block', marginBottom: '10px', padding: '5px', width: '300px' }}
                />
                <textarea
                    placeholder="Content"
                    value={newPost.content}
                    onChange={(e) => setNewPost({ ...newPost, content: e.target.value })}
                    style={{ display: 'block', marginBottom: '10px', padding: '5px', width: '300px', height: '100px' }}
                ></textarea>
                <button onClick={handlePostCreation} style={{ padding: '10px 20px' }}>CREATE</button>
            </section>

            <h2>Posts</h2>
            <section className="posts" style={{ marginBottom: '40px' }}>
                {posts.map((post) => (
                    <div key={post.id} className="post">
                        <h3>{post.title}</h3>
                        <span className="date-created">{post.createdAt}</span>
                        <p>{post.content}</p>
                        <p>Likes: {likes[post.id]}</p>
                        <button onClick={() => handleLike(post.id)} style={{ padding: '5px 10px' }}>Like</button>
                    </div>
                ))}
            </section>

            <section>
                <h2>All Users</h2>
                <div style={{ display: 'flex', flexWrap: 'wrap', gap: '20px' }}>
                    {users.map((user) => (
                        <div key={user.id} style={{ border: '1px solid #ccc', borderRadius: '8px', padding: '15px', width: '200px', boxShadow: '0 2px 4px rgba(0, 0, 0, 0.1)' }}>
                            <h3 style={{ margin: '0 0 10px 0' }}>{user.name}</h3>
                            <p><strong>Email:</strong> {user.email}</p>
                        </div>
                    ))}
                </div>
            </section>
        </div>
    );
}

export default SocialMediaApp;
