import React from "react";
import { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import Loading from "../Components/Loading";
import LoginForm from "../Components/LoginForm";
import { Container } from 'react-bootstrap';
import './App.css';


const Login = () => {

    const [loading, setLoading] = useState(false);
    const [request, setRequest] = useState({});
    const [success, setSetsuccess] = useState(false);

    const navigate = useNavigate();


    useEffect(() => {
        if (Object.keys(request).length !== 0) {
            setLoading(true);
            const fetchData = async () => {
                try {
                    console.log(request);
                    const response = await fetch('http://localhost:5213/api/auth/login', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(request),
                    });

                    if (response.ok) {
                        setSetsuccess(true);
                        setTimeout(() => navigate('/'), 1300);
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
            {!success && < LoginForm setRequest={setRequest} />}
            {success &&
                <Container fluid className="login-message">
                    <h2 >You are logged in!</h2>
                </Container>}
        </>

    );

};

export default Login;