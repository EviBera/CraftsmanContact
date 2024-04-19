import React, { useState } from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';


const RegistrationForm = ({ setRequest }) => {

    const [userInput, setUserInput] = useState({
        FirstName: '',
        LastName: '',
        Email: '',
        PhoneNumber: '',
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
                <Form.Label>Enter your firstname:</Form.Label>
                <Form.Control 
                    type="text"
                    name="FirstName"
                    value={userInput.FirstName}
                    onChange={handleChange}
                    placeholder="Enter your firstname" />
            </Form.Group>
            <Form.Group>
                <Form.Label>Enter your lastname:</Form.Label>
                <Form.Control 
                    type="text"
                    name="LastName"
                    value={userInput.LastName}
                    onChange={handleChange}
                    placeholder="Enter your lasttname" />
            </Form.Group>
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
                <Form.Label>Enter your phone number:</Form.Label>
                <Form.Control 
                    type="text" 
                    name="PhoneNumber"
                    value={userInput.PhoneNumber}
                    onChange={handleChange}
                    placeholder="Enter your pone number" />
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
                Click here to submit form
            </Button>
        </Form>
    );
}

export default RegistrationForm;