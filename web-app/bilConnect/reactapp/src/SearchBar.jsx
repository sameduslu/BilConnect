import React from 'react';
import { motion } from 'framer-motion';

class SearchBar extends React.Component {
    render() {
        return (
            <div>
                {this.props.isContentVisible ? (
                    <div
                        style={{
                            display: 'flex',
                            alignItems: 'center',
                            marginLeft: 'auto',
                        }}
                        className="ms-Grid-col"
                    >
                        <motion.div animate={{ x: -20 }}>
                            <div
                                style={{
                                    display: 'flex',
                                    alignItems: 'center',
                                    marginLeft: 'auto',
                                    width: '1000px', // Set the maximum width you desire
                                }}
                            >
                                <input
                                    type="text"
                                    placeholder="Search"
                                    style={{
                                        color: 'black',
                                        display: 'inline-flex',
                                        background: 'white',
                                        border: '2px solid black',
                                        borderRadius: '10px',
                                        height: '30px',
                                        width: '100%', // Take up 100% of the available width
                                        marginLeft: 'auto',
                                        fontFamily:
                                            "'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif",
                                        paddingLeft: '10px',
                                    }}
                                />
                                <button
                                    type="button"
                                    onClick={this.props.search}
                                    style={{
                                        background: 'transparent',
                                        border: 'none',
                                        outline: 'none',
                                    }}
                                >
                                    <img
                                        src={
                                            'https://github.com/Murat-Cagri/bilconnectImages/blob/main/searchIcon.png?raw=true'
                                        }
                                        style={{ width: '30px', height: '30px', marginTop: '5px'}}
                                    />
                                </button>
                            </div>
                        </motion.div>
                        <button
                            type="button"
                            onClick={this.props.openTaskbar}
                            style={{
                                background: 'white',
                                border: '2px solid black',
                                borderRadius: '15px',
                                outline: 'none',
                                color: 'black',
                                marginRight: '10px',
                                display: 'flex',
                                position: 'relative',
                                height: '40px',
                                alignItems: 'center',
                            }}
                        >
                            Cancel
                        </button>
                    </div>
                ) : (
                    <button
                        type="button"
                        onClick={this.props.openTaskbar}
                        style={{ background: 'transparent', border: 'none', outline: 'none' }}
                    >
                        <img
                            src={
                                'https://github.com/Murat-Cagri/bilconnectImages/blob/main/searchIcon.png?raw=true'
                            }
                                style={{
                                    width: '30px', height: '30px', marginTop: '5px'
                                }}
                        />
                    </button>
                )}
            </div>
        );
    }
}

export default SearchBar;