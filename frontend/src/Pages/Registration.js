import React from "react";
import { useEffect, useState } from "react";
import Loading from "../Components/Loading";
import RegistrationForm from "../Components/RegistrationForm";
import { Container } from 'react-bootstrap';
import Alert from 'react-bootstrap/Alert';
import Button from 'react-bootstrap/Button';
import './App.css';



const Register = () => {

    const [loading, setLoading] = useState(false);
    const [request, setRequest] = useState({});
    const [success, setSetsuccess] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");


    useEffect(() => {
        if (Object.keys(request).length !== 0) {
            setLoading(true);
            const fetchData = async () => {
                try {
                    console.log(request);
                    const response = await fetch('http://localhost:5213/api/auth/register', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(request),
                    });

                    if (response.ok) {
                        setSetsuccess(true);
                    } else {
                        setErrorMessage(await response.text());
                    }

                    const data = await response.json();

                    console.log(data);
                } catch (error) {
                    console.error('Error:', error);
                }
                setLoading(false);
            };
            fetchData();
        }
    }, [request]);

    if (loading) {
        return < Loading />
    };

    return (
        <>
            {!success && < RegistrationForm setRequest={setRequest} />}
            {success &&
                <Container fluid className="login-message">
                    <h2 >Successful registration</h2>
                    <h2> If you would like to log in, click <a href="/login">here</a>!</h2>
                </Container>}
            {errorMessage &&
                <Container className="alert-container">
                    <Alert className="alert">
                        <Alert.Heading>Oh snap! You got an error!</Alert.Heading>
                        <p>{errorMessage}</p>
                        <hr />
                        <div className="d-flex justify-content-end">
                            <Button onClick={() => setErrorMessage("")} variant="outline-success">
                                Close
                            </Button>
                        </div>
                    </Alert>
                </Container>}
        </>
    );

};

export default Register;