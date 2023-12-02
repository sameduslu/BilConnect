import React, { Component } from 'react';
import styles from './App.module.css';
import SearchBar from './SearchBar.jsx';
import ActionsMenu from './ActionsMenu.jsx';
import Post from './Post.jsx';
export default class App extends Component {
    state = {
        currentIndex: 0,
        isTransitioning: false,
    };

    data = [
        {
            image:
                'https://s26162.pcdn.co/wp-content/uploads/sites/2/2022/05/Book.jpg',
            information: {
                title: 'Title',
                price: '$10.99',
                description: 'Description',
            },
        },
        {
            image:
                'https://www.ikea.com/in/en/images/products/jaettelik-soft-toy-dinosaur-dinosaur-brontosaurus__0804796_pe769337_s5.jpg?f=s',
            information: {
                title: 'Title 2',
                price: '$19.99',
                description: 'Description 2',
            },
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

    render() {
        const { currentIndex, isTransitioning } = this.state;
        return (
            <div className="ms-Grid" dir="ltr">
                <div className="ms-Grid-row">
                    <div className={styles.header}>
                        <button
                            type="button"
                            onClick={this.state.isContentVisible ? this.redirectHome : () => window.location.href = '/'}
                            style={{ background: 'transparent', border: 'none', outline: 'none' }}
                        >
                            <img
                                height={100}
                                src={
                                    'https://github.com/Murat-Cagri/bilconnectImages/blob/main/BILCONNECT_LOGO.png?raw=true'
                                }
                                className={styles.logo}
                            />
                        </button>
                        <div className={styles.SearchBar}>
                            <SearchBar 
                                isContentVisible={this.state.isContentVisible}
                                openTaskbar={this.openTaskbar}
                                />
                        </div>
                        <div className={styles.actionsMenu}>
                            {!this.state.isContentVisible && <ActionsMenu className={styles.actionsMenu} />}
                        </div>
                    </div>
                    <div className={styles.animatedBlockWrapper}>
                        <div className={styles.animatedImageWrapper}>
                            <img
                                src={this.data[currentIndex].image}
                                alt="Post Image"
                                className={`${styles.animatedImage} ${isTransitioning ? styles.fade : ''
                                    }`}
                            />
                        </div>
                        <div className={styles.information}>
                            <div>Title: {this.data[currentIndex].information.title}</div>
                            <div>Price: {this.data[currentIndex].information.price}</div>
                            <div>Description: {this.data[currentIndex].information.description}</div>
                        </div>
                    </div>
                    <div className={styles.navigationButtons}>
                        <button
                            className={styles.navigationButton}
                            onClick={() => this.handleAnimateClick('prev')}
                        >
                            {'<'}
                        </button>
                        <button
                            className={styles.navigationButton}
                            onClick={() => this.handleAnimateClick('next')}
                        >
                            {'>'}
                        </button>
                    </div>
                </div>
                <div className={styles.postsContainer}>
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
        );
    }
}