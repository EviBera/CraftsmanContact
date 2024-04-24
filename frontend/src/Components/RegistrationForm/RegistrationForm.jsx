import React, { useState } from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import FloatingLabel from 'react-bootstrap/FloatingLabel';
import './RegistrationForm.css';
import { Container } from 'react-bootstrap';


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
        <Container className='form-container'>
            <h2>Registration form</h2>
            <Form onSubmit={handleSubmit}>
                <Form.Group>
                    <FloatingLabel
                        controlId="floatingInput3"
                        label="Your firsname"
                        className="mb-3">
                        <Form.Control
                            type="text"
                            name="FirstName"
                            value={userInput.FirstName}
                            onChange={handleChange}
                            placeholder="Enter your firstname" />
                    </FloatingLabel>
                </Form.Group>

                <Form.Group>
                    <FloatingLabel
                        controlId="floatingInput4"
                        label="Your lastname"
                        className="mb-3">
                        <Form.Control
                            type="text"
                            name="LastName"
                            value={userInput.LastName}
                            onChange={handleChange}
                            placeholder="Enter your lasttname" />
                    </FloatingLabel>
                </Form.Group>

                <Form.Group>
                    <FloatingLabel
                        controlId="floatingInput5"
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
                        controlId="floatingInput6"
                        label="Your phone number"
                        className="mb-3">
                        <Form.Control
                            type="text"
                            name="PhoneNumber"
                            value={userInput.PhoneNumber}
                            onChange={handleChange}
                            placeholder="Enter your phone number" />
                    </FloatingLabel>
                </Form.Group>

                <Form.Group>
                    <FloatingLabel
                        controlId="floatingInput7"
                        label="Your password"
                        className="mb-3">
                        <Form.Control
                            type="password"
                            name="Password"
                            value={userInput.Password}
                            onChange={handleChange}
                            placeholder="Enter your password" />
                    </FloatingLabel>
                    <p className='info'>Password must be at least 8 characters, 
                    must contain at least one lower case letter (a-z), 
                    upper case letter (A-Z), digit (0-9) and symbol (#&@+!%).
                    </p>
                </Form.Group>
                <Button variant="primary" type="submit" >
                    Register
                </Button>
            </Form>
        </Container>
    );
}

export default RegistrationForm;