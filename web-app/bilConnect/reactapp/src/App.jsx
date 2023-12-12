import React, { Component } from 'react';
import LoginPage from './Login.jsx';
import styles from './App.module.css';
import SearchBar from './SearchBar.jsx';
import ActionsMenu from './ActionsMenu.jsx';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Post from './Post.jsx';
import 'normalize.css';

export default class App extends Component {
    state = {
        currentIndex: 0,
        isTransitioning: false,
        isContentVisible: false,
    };

    data = [
        {
            image:
                'https://cdn.venngage.com/template/thumbnail/small/37de5deb-1ca7-4e60-b254-374b08708817.webp',
        },
        {
            image:
                'https://images.squarespace-cdn.com/content/v1/5aadc54285ede1bd72181a3a/1521339647830-LKHTH62ZRY5TCGVCW81P/shutterstock_538256848.jpg?format=1500w',
        },
    ];

    imagePreloader = new Image();

    componentDidMount() {
        this.startTimer();
    }

    componentWillUnmount() {
        clearInterval(this.intervalId);
    }

    startTimer() {
        this.intervalId = setInterval(() => {
            this.setState(
                (prevState) => ({
                    currentIndex: (prevState.currentIndex + 1) % this.data.length,
                    isTransitioning: true,
                }),
                () => {
                    setTimeout(() => {
                        this.setState({ isTransitioning: false });
                    }, 200);
                }
            );
        }, 7000);
    }

    handleAnimateClick = (direction) => {
        clearInterval(this.intervalId); // Stop the current timer

        const currentIndex = this.state.currentIndex;
        let newIndex;

        if (direction === 'next') {
            newIndex = (currentIndex + 1) % this.data.length;
        } else {
            newIndex = (currentIndex - 1 + this.data.length) % this.data.length;
        }

        // Preload the next image
        this.imagePreloader.src = this.data[newIndex].image;

        this.setState(
            {
                currentIndex: newIndex,
                isTransitioning: true,
            },
            () => {
                setTimeout(() => {
                    this.setState({ isTransitioning: false }, () => {
                        this.startTimer(); // Start a new timer
                    });
                }, 200);
            }
        );
    };

    openTaskbar = () => {
        this.setState((prevState) => ({
            isContentVisible: !prevState.isContentVisible,
        }));
    };

    handleLogin = (username) => {
        this.isEntered = true;
    };

    render() {
        const { currentIndex, isTransitioning, isContentVisible, isEntered = false } = this.state;
        const dots = Array.from({ length: this.data.length }, (_, index) => (
            <span
                key={index}
                className={`${styles.dot} ${currentIndex === index ? styles.activeDot : ''}`}
                onClick={() => this.handleDotClick(index)}
            />
        ));

        return (
            <Router>
                <Routes>
                    {!this.state.isEntered && (
                        <Route
                            path="/"
                            element={<div className={`${styles.centeredContainer}`}><LoginPage onLogin={this.handleLogin} /></div>}
                        />
                    )}
                    <Route
                        path="/main"
                        element={
                            <div className="ms-Grid" dir="ltr">
                                <div className="ms-Grid-row">
                                    <div className={`${styles.header}`}>
                                        <button
                                            type="button"
                                            onClick={() => window.location.href = '/main'}
                                            style={{ background: 'transparent', border: 'none', outline: 'none' }}
                                        >
                                            <img
                                                height={100}
                                                src={
                                                    'https://github.com/Murat-Cagri/bilconnectImages/blob/main/BILCONNECT_LOGO.png?raw=true'
                                                }
                                                className={`${styles.logo}`}
                                            />
                                        </button>
                                        <div className={`${styles.SearchBar}`}>
                                            <SearchBar
                                                isContentVisible={this.state.isContentVisible}
                                                openTaskbar={this.openTaskbar}
                                            />
                                        </div>
                                        <div className={`${styles.actionsMenu}`}>
                                            {!this.state.isContentVisible && <ActionsMenu className={`${styles.actionsMenu}`} />}
                                        </div>
                                    </div>
                                    <div className={`${styles.animatedBlockWrapper}`}>
                                        <div className={`${styles.animatedImageWrapper}`}>
                                            <img
                                                src={this.data[currentIndex].image}
                                                alt="Post Image"
                                                className={`${styles.animatedImage} ${isTransitioning ? styles.fade : ''}`}
                                            />
                                        </div>
                                    </div>
                                    <div className={`${styles.navigationButtons}`}>
                                        <button
                                            className={`${styles.navigationButton}`}
                                            onClick={() => this.handleAnimateClick('prev')}
                                        >
                                            {'<'}
                                        </button>
                                        {dots}
                                        <button
                                            className={`${styles.navigationButton}`}
                                            onClick={() => this.handleAnimateClick('next')}
                                        >
                                            {'>'}
                                        </button>
                                    </div>
                                </div>
                                <div className={`${styles.postsContainer}`}>
                                    <Post
                                        image="https://basakgazetesi.com/resimler/2022-8/13/67785517318056.jpg"
                                        title="MURAT ÇAĞRI KARA"
                                        price="$INFINITY"
                                        description="KARA MURAT İSTANBULDA YAKALANDI!!"
                                    />
                                    <Post
                                        image="https://i.ytimg.com/vi/Kowjp9yccXo/maxresdefault.jpg"
                                        title="DİNAZOR"
                                        price="$999999.99"
                                        description="BU FIRSAT KAÇMAZZZ!!!!"
                                    />
                                    <Post
                                        image="https://www.bestepekoleji.com/wp-content/uploads/ab8bbdd3-099c-4a47-9351-c26500a11e5d-300x225.jpg"
                                        title="Tarihten Murat"
                                        price="$INFINITY"
                                        description="BESTEPE KOLEJİ SÜRÜM 19 YAŞ!!"
                                    />
                                </div>
                            </div>
                        }
                    />
                </Routes>
            </Router>
        );
    }
}