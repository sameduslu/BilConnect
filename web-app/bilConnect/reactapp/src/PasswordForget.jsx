// PasswordForget.jsx
import React, { useState } from 'react';
import styles from './PasswordForget.module.css';

const PasswordForget = ({ onClose }) => {
    const [email, setEmail] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        // Add your logic to handle the password reset
        console.log('Reset password logic here');
        onClose(); // Close the pop-up after submission
    };

    return (
        <div className={styles.popup}>
            <form onSubmit={handleSubmit}>
                <h2>Forgot your password?</h2>
                <input
                    type="text"
                    placeholder="Enter your email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                <button type="submit">Submit</button>
            </form>
            <button className={styles.closeButton} onClick={onClose}>
                X
            </button>
        </div>
    );
};

export default PasswordForget;
