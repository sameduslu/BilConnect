import React, { useState } from 'react';
import './ActionsMenu.css'; 

const ActionsMenu = () => {
    const [isMenuOpen, setMenuOpen] = useState(false);

    const handleToggleMenu = () => {
        setMenuOpen(!isMenuOpen);
    };

    const handleCloseMenu = () => {
        setMenuOpen(false);
    };

    return (
        <div className={`actions-menu ${isMenuOpen ? 'open' : ''}`}>
            <div className="menu-frame" onClick={handleCloseMenu}>
                {/* User Profile Button (Top Center) */}
                <div className="user-profile-button" onClick={() => window.location.href = '/user-profile'}>
                    <img src="https://github.com/Murat-Cagri/bilconnectImages/blob/main/userr.png?raw=true" alt="User Profile" />
                </div>

                {/* Show All Chats Button */}
                <div className="action-button show-all-chats" onClick={() => window.location.href = '/show-all-chats'}>
                    <img src="https://github.com/Murat-Cagri/bilconnectImages/blob/main/chat.png?raw=true" alt="Show All Chats" className="button-image" />
                    Show All Chats
                </div>

                {/* Favorite Posts Button */}
                <div className="action-button favorite-posts" onClick={() => window.location.href = '/favorite-posts'}>
                    <img src="https://github.com/Murat-Cagri/bilconnectImages/blob/main/fovorite_posts.png?raw=true" alt="Favorite Posts" className="button-image" />
                    Favorite Posts
                </div>
            </div>
            <div className={`menu-icon ${isMenuOpen ? 'open' : ''}`} onClick={handleToggleMenu}>
                <span className={`bar ${isMenuOpen ? 'rotate-up' : ''}`}></span>
                <span className={`bar ${isMenuOpen ? 'fade-out' : ''}`}></span>
                <span className={`bar ${isMenuOpen ? 'rotate-down' : ''}`}></span>
            </div>
        </div>
    );
};

export default ActionsMenu;