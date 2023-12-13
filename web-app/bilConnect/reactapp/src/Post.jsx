import React from 'react';
import { Box, Typography, Card, CardContent, CardMedia, Container, Grid, Paper } from '@mui/material';
import styles from './Post.module.css';

const DetailedPostPage = ({ posts }) => {
    return (
        <Container maxWidth="xl" sx={{ marginTop: 4 }}>
            <Grid container spacing={4} justifyContent="center">
                {posts.map((post) => (
                    <Grid item key={post.id} xs={12} sm={5} md={3}>
                        <Card
                            sx={{
                                height: '400px',
                                display: 'flex',
                                flexDirection: 'column',
                                background: '#121212',
                                color: 'white',
                                position: 'relative',
                                overflow: 'hidden',
                                borderRadius: '10px',
                                transition: 'transform 0.3s ease-in-out',
                            }}
                            className={styles.postCard}
                        >
                            <CardMedia
                                component="img"
                                height="50%"
                                image={post.image}
                                alt="Post Image"
                                sx={{ objectFit: 'fill' }}
                            />
                            <Paper
                                elevation={4}
                                sx={{
                                    position: 'absolute',
                                    top: 0,
                                    left: 0,
                                    right: 0,
                                    bottom: 0,
                                    display: 'flex',
                                    alignItems: 'center',
                                    justifyContent: 'center',
                                    backgroundColor: 'rgba(0, 0, 0, 0.7)',
                                    opacity: 0,
                                    transition: 'opacity 0.3s ease-in-out',
                                }}
                                className={styles.overlay}
                            >
                                <button
                                    onClick={() => window.location.href = `/post/${post.id}`}
                                    className={styles.overlayButton}
                                >
                                    View Details
                                </button>
                            </Paper>
                            <CardContent sx={{ flexGrow: 1, position: 'relative', zIndex: 1 }}>
                                <Typography variant="h5" gutterBottom>
                                    {post.title}
                                </Typography>
                                {post.price && (
                                    <Typography variant="h6" color="primary">
                                        {post.price}
                                    </Typography>
                                )}
                                {post.description && (
                                    <Typography variant="body2" sx={{ mt: 2 }}>
                                        {post.description}
                                    </Typography>
                                )}
                                {post.quota && (
                                    <Typography variant="body2" sx={{ mt: 2 }}>
                                        Quota: {post.quota}
                                    </Typography>
                                )}
                                {post.traveltime && (
                                    <Typography variant="body2" sx={{ mt: 2 }}>
                                        Travel Time: {post.traveltime}
                                    </Typography>
                                )}
                                {post.destination && (
                                    <Typography variant="body2" sx={{ mt: 2 }}>
                                        Destination: {post.destination}
                                    </Typography>
                                )}
                                {post.origin && (
                                    <Typography variant="body2" sx={{ mt: 2 }}>
                                        Origin: {post.origin}
                                    </Typography>
                                )}
                                {post.isfullyvaccinated !== undefined && (
                                    <Typography variant="body2" sx={{ mt: 2 }}>
                                        Fully Vaccinated: {post.isfullyvaccinated ? 'Yes' : 'No'}
                                    </Typography>
                                )}
                                {post.ageinmonths && (
                                    <Typography variant="body2" sx={{ mt: 2 }}>
                                        Age in Months: {post.ageinmonths}
                                    </Typography>
                                )}
                                {post.place && (
                                    <Typography variant="body2" sx={{ mt: 2 }}>
                                        Place: {post.place}
                                    </Typography>
                                )}
                                {post.returndate && (
                                    <Typography variant="body2" sx={{ mt: 2 }}>
                                        Return Date: {post.returndate}
                                    </Typography>
                                )}
                            </CardContent>
                        </Card>
                    </Grid>
                ))}
            </Grid>
        </Container>
    );
};

export default DetailedPostPage;
