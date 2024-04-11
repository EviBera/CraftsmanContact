import React from "react";
import Container from 'react-bootstrap/Container';
import Navbar from 'react-bootstrap/Navbar';
import './App.css';

function NavigationBar() {

    return (
        <Navbar className="navbar">
            <Container>
                <Navbar.Text>Welcome to the Craftsman Contact app!</Navbar.Text>
            </Container>
        </Navbar>
    );
};

export default NavigationBar;