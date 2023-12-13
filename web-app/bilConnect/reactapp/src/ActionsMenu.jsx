import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom'; 
import './ActionsMenu.css'; 

const ActionsMenu = () => {
    const [isMenuOpen, setMenuOpen] = useState(false);
    const navigate = useNavigate();

    const handleToggleMenu = () => {
        setMenuOpen(!isMenuOpen);
    };

    const handleCloseMenu = () => {
        setMenuOpen(false);
    };

    const handleNavigate = (route) => {
        localStorage.removeItem('isAuthenticated');
        navigate(route);
        handleCloseMenu(); // Close the menu after navigating
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
                <div className="action-button navigate-button" onClick={() => handleNavigate('/')}>
                    <img src="https://github.com/Murat-Cagri/bilconnectImages/blob/main/logout.png?raw=true" alt="Navigate Button" className="button-logout" />
                    Logout
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