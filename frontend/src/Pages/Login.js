import React from "react";
import { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import Loading from "../Components/Loading";
import LoginForm from "../Components/LoginForm";
import { Container } from 'react-bootstrap';
import Alert from 'react-bootstrap/Alert';
import Button from 'react-bootstrap/Button';
import './App.css';
import { URLS } from "../Config/urls";


const Login = () => {

    const [loading, setLoading] = useState(false);
    const [request, setRequest] = useState({});
    const [success, setSetsuccess] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");

    const navigate = useNavigate();


    useEffect(() => {
        if (Object.keys(request).length !== 0) {
            setLoading(true);
            const fetchData = async () => {
                try {
                    const response = await fetch(URLS.auth.login, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(request),
                    });

                    if (response.ok) {
                        setSetsuccess(true);
                        setTimeout(() => navigate('/'), 1300);
                    } else {
                        setErrorMessage(await response.text());
                    }

                    const data = await response.json();
                    localStorage.setItem('loggedInUser', JSON.stringify(data));

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
                {!success && < LoginForm setRequest={setRequest} />}
                {success &&
                    <Container fluid className="login-message">
                        <h2 >You are logged in!</h2>
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

export default Login;