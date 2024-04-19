import React from "react";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import './App.css';

function NavigationBar() {

    return (
        <Navbar className="navbar" sticky="top">
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
        </Navbar>
    );
};

export default NavigationBar;