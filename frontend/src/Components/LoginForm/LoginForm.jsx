import React, { useState } from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import FloatingLabel from 'react-bootstrap/FloatingLabel';
import { Container } from 'react-bootstrap';
import './LoginForm.css';


const LoginForm = ({ setRequest }) => {

    const [userInput, setUserInput] = useState({
        Email: '',
        Password: ''
    });

    const handleChange = (event) => {
        const { name, value } = event.target;
        setUserInput(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        setRequest(userInput);
    }

    return (
        <Container className='form-container'>
            <h2>Login</h2>
            <Form onSubmit={handleSubmit}>

                <Form.Group>
                    <FloatingLabel
                        controlId="floatingInput1"
                        label="Your email address"
                        className="mb-3">

                        <Form.Control
                            type="email"
                            name="Email"
                            value={userInput.Email}
                            onChange={handleChange}
                            placeholder="Enter your your email address" />
                    </FloatingLabel>
                </Form.Group>

                <Form.Group>
                    <FloatingLabel
                        controlId="floatingInput2"
                        label="Your password"
                        className="mb-3">
                        <Form.Control
                            type="password"
                            name="Password"
                            value={userInput.Password}
                            onChange={handleChange}
                            placeholder="Enter your password" />
                    </FloatingLabel>
                </Form.Group>

                <Button variant="primary" type="submit" >
                    Login
                </Button>
            </Form>
        </Container>
    );
}

export default LoginForm;