import React from 'react';
import styles from './Post.module.css';

const Post = ({ image, title, price, description }) => {
    return (
        <div className={styles.post} onClick={() => window.location.href = '/that_post'}>
            <img src={image} alt="Post" className={styles.postImage} />
            <div className={styles.postContent}>
                <h3 className={styles.postTitle}>{title}</h3>
                <p className={styles.postPrice}>{price}</p>
                <p className={styles.postDescription}>{description}</p>
            </div>
        </div>
    );
};

export default Post;