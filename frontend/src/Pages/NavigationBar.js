import React from "react";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import './App.css';

function NavigationBar() {

    const storedLoggedInUserString = localStorage.getItem('loggedInUser');
    const storedLoggedInUser = JSON.parse(storedLoggedInUserString);

    const handleLogout = () => {
        localStorage.removeItem('loggedInUser');
        localStorage.removeItem('selectedServiceId');
        localStorage.removeItem('serviceName');
        localStorage.removeItem('craftsmanName');
    }

    return (
        <Navbar className="navbar" sticky="top">
            {!storedLoggedInUser &&
                <Container className="nav-container">
                    <Nav className="me-auto custom-nav">
                        <Nav.Link href="/login" className="nav-link">Login</Nav.Link>
                        <Nav.Link href="/register" className="nav-link">Register</Nav.Link>
                    </Nav>
                    <Navbar.Text className="navbar-center-text">Welcome to the Craftsman Contact app!</Navbar.Text>
                    <Nav className="ms-auto custom-nav invisible-spacer"> {/* Invisible Spacer */}
                        <Nav.Link href="#" className="nav-link invisible">Login</Nav.Link>
                        <Nav.Link href="#" className="nav-link invisible">Register</Nav.Link>
                    </Nav>
                </Container>
            }
            {storedLoggedInUser && <Container className="nav-container">
                <Nav className="me-auto custom-nav">
                    <Nav.Link href="/" className="nav-link" onClick={handleLogout}>Logout</Nav.Link>
                </Nav>
                <Navbar.Text className="navbar-center-text">Nice to see you, {storedLoggedInUser.firstName}!</Navbar.Text>
                <Nav className="ms-auto custom-nav invisible-spacer"> {/* Invisible Spacer */}
                    <Nav.Link href="#" className="nav-link invisible">Login</Nav.Link>
                </Nav>
            </Container>}
        </Navbar>
    );
};

export default NavigationBar;