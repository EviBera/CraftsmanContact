import React from "react";
import Container from 'react-bootstrap/Container';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';
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
                <Container fluid className="nav-container">
                    <Row>
                        <Col xs={6} sm={6} md={3} lg={2} order={{ xs: 2, md: 1 }} className="auth-links">
                            <Nav>
                                <Nav.Link href="/login" className="nav-link">Login</Nav.Link>
                                <Nav.Link href="/register" className="nav-link">Register</Nav.Link>
                            </Nav>
                        </Col>
                        <Col xs={12} sm={12} md={6} lg={8} order={{ xs: 1, md: 2 }}>
                            <Navbar.Text className="navbar-center-text">Welcome to the Craftsman Contact app!</Navbar.Text>
                        </Col>
                        <Col xs={6} sm={6} md={3} lg={2} className="auth-links" order={{ xs: 3, md: 3 }}>
                            <Nav> {/* Invisible Spacer */}
                                <Nav.Link href="#" className="nav-link-invisible">Login</Nav.Link>
                                <Nav.Link href="#" className="nav-link-invisible">Register</Nav.Link>
                            </Nav>
                        </Col>
                    </Row>
                </Container>
            }
            {storedLoggedInUser &&
                <Container fluid className="nav-container">
                    <Row>
                        <Col xs={6} sm={6} md={3} lg={3} order={{ xs: 2, md: 1 }}>
                            <Nav>
                                <Nav.Link href="/" className="nav-link" onClick={handleLogout}>Logout</Nav.Link>
                            </Nav>
                        </Col>
                        <Col xs={12} sm={12} md={6} lg={6} order={{ xs: 1, md: 2 }} className="navbar-welcome-text">
                            <Navbar.Text className="navbar-center-text">Nice to see you, {storedLoggedInUser.firstName}!</Navbar.Text>
                        </Col>
                        <Col>
                            <Nav xs={6} sm={6} md={3} lg={3} order={{ xs: 3, md: 3 }} className="links-on-right">
                                <Nav.Link href="/" className="nav-link">Home</Nav.Link>
                                <Nav.Link href="/deals" className="nav-link">My deals</Nav.Link>
                            </Nav>
                        </Col>
                    </Row>
                </Container>
            }
        </Navbar>
    );
};

export default NavigationBar;