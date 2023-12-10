import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './Login.css';

const Login = () => {
    const [isSignIn, setIsSignIn] = useState(true);
    const navigate = useNavigate();

    const switchForms = () => {
        setIsSignIn(!isSignIn);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        try {
            // Your existing form submission logic

            // Assuming the form submission was successful, navigate to "/main"
            navigate('/main');
        } catch (error) {
            console.error('Form submission error:', error);
            // Handle errors or display a message to the user
        }
    };

    return (
        <div className="main">
            <div className={`container ${isSignIn ? 'a-container' : 'b-container'}`}>
                <form
                    className="form"
                    id={isSignIn ? 'a-form' : 'b-form'}
                    method=""
                    action=""
                    onSubmit={handleSubmit}
                >
                    <h2 className="form_title title">
                        {isSignIn ? 'Sign in' : 'Create Account'}
                    </h2>
                    <input className="form__input" type="text" placeholder="Email" />
                    {isSignIn ? (
                        <>
                            
                            <input
                                className="form__input"
                                type="password"
                                placeholder="Password"
                            />
                            <a className="form__link" href="#">
                                Forgot your password?
                            </a>
                        </>
                    ) : (
                        <>
                            <input className="form__input" type="text" placeholder="Name" />
                            <input
                                className="form__input"
                                type="password"
                                placeholder="Password"
                            />
                        </>
                    )}
                    <button className="form__button button submit" type="submit">
                        {isSignIn ? 'SIGN IN' : 'SIGN UP'}
                    </button>
                </form>
            </div>

            <div className="switch" id="switch-cnt">
                <div className="switch__circle"></div>
                <div
                    className={`switch__circle switch__circle--t ${isSignIn ? '' : 'is-hidden'
                        }`}
                ></div>
                <div
                    className={`switch__container ${isSignIn ? 'is-txl' : 'is-txr'
                        }`}
                    id="switch-c1"
                >
                    <h2 className="switch__title title">Join Us!</h2>
                    <button
                        className="switch__button button switch-btn"
                        onClick={switchForms}
                    >
                        SIGN UP
                    </button>
                </div>

                <div
                    className={`switch__container ${isSignIn ? 'is-txr' : ''}`}
                    id="switch-c2"
                >
                    <h2 className="switch__title title">Welcome!</h2>
                    <button
                        className="switch__button button switch-btn"
                        onClick={switchForms}
                    >
                        SIGN IN
                    </button>
                </div>
            </div>
        </div>
    );
};

export default Login;
