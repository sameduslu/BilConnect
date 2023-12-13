// Login.jsx
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import styles from './Login.module.css';
import PasswordForget from './PasswordForget.jsx';
import './animation.css';

const Login = () => {
    const [isSignIn, setIsSignIn] = useState(true);
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [isPasswordForgetVisible, setPasswordForgetVisible] = useState(false);
    const switchForms = () => {
        setIsSignIn(!isSignIn);
    };
    const handleScreen = () => {
        return isPasswordForgetVisible && (
            <PasswordForget onClose={() => setPasswordForgetVisible(false)} />
        );
    };
    const handleSubmit = (e) => {
        e.preventDefault();
        try {

            // Check if the email is from "@ug.bilkent.edu.tr" and password is "1"
            if (email === 'ozan' && password === 'ozan221') {
                navigate('/main');
            } else {
                // Handle invalid login (show error message, etc.)
                console.log('Invalid email or password');
            }
        } catch (error) {
            console.error('Form submission error:', error);
            // Handle errors or display a message to the user
        }
    };

    return (
        <div className={styles.main}>
            <div className={`${styles.container} ${isSignIn ? styles.aContainer : styles.bContainer}`}>
                <form
                    className={styles.form}
                    id={isSignIn ? styles.aForm : styles.bForm}
                    method=""
                    action=""
                    onSubmit={handleSubmit}
                >
                    <h2 className={`${styles.formTitle} ${styles.title}`}>
                        {isSignIn ? 'Sign in' : 'Create Account'}
                    </h2>
                    <input
                        className={styles.formInput}
                        type="text"
                        placeholder="Email"
                        name="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                    {isSignIn ? (
                        <>
                            <input
                                className={styles.formInput}
                                type="password"
                                placeholder="Password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                            />
                            <a className={styles.formLink} onClick={() => setPasswordForgetVisible(true)}>
                                Forgot your password?
                            </a>
                        </>
                    ) : (
                        <>
                            <input className={styles.formInput} type="text" placeholder="Name" name="name" />
                            <input
                                className={styles.formInput}
                                type="password"
                                placeholder="Password"
                                name="password"
                            />
                        </>
                    )}
                    <button
                        className={`${styles.formButton} ${styles.button} ${styles.submit}`}
                        type="submit"
                    >
                        {isSignIn ? 'SIGN IN' : 'SIGN UP'}
                    </button>
                </form>
            </div>
            {handleScreen()}
            <div className={`${styles.switch} `} id="switch-cnt">
                <div className={`${styles.coolShape}`} />
                <div className={`${styles.switch__circle} `}></div>
                {isSignIn ? (
                    <div
                        className={`${styles.switch__container}`}
                        id="switch-c1"
                        style={{ animation: `fade-out var(--transition)` }}
                    >
                        <h2 className={`${styles.switch__title} ${styles.title}`}>Join Us!</h2>
                        <button
                            className={`${styles.switch__button} ${styles.button} ${styles.switchBtn}`}
                            onClick={switchForms}
                        >
                            SIGN UP
                        </button>
                    </div>
                ) : (
                    <div
                        className={`${styles.switch__container}`}
                        id="switch-c1"
                        style={{ animation: `fade-in var(--transition)` }}
                    >
                        <h2 className={`${styles.switch__title} ${styles.title}`}>Welcome!</h2>
                        <button
                            className={`${styles.switch__button} ${styles.button} ${styles.switchBtn}`}
                            onClick={switchForms}
                        >
                            SIGN IN
                        </button>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Login;
