import React, { useState } from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';


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
        <Form onSubmit={handleSubmit}>
            
            <Form.Group>
                <Form.Label>Enter your email address:</Form.Label>
                <Form.Control 
                    type="email"
                    name="Email"
                    value={userInput.Email}
                    onChange={handleChange}
                    placeholder="Enter your your email address" />
            </Form.Group>
            
            <Form.Group>
                <Form.Label>Enter your password:</Form.Label>
                <Form.Control 
                    type="text"
                    name="Password"
                    value={userInput.Password}
                    onChange={handleChange}
                    placeholder="Enter your password" />
            </Form.Group>
            <Button variant="primary" type="submit" >
                Login
            </Button>
        </Form>
    );
}

export default LoginForm;