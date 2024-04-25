import React, { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import ContactForm from "../Components/ContactForm";
import NavigationBar from './NavigationBar';
import { Container } from 'react-bootstrap';
import Loading from "../Components/Loading";
import './App.css';

const Contact = () => {

    const [submitted, setSubmitted] = useState(false);
    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState("");

    const navigate = useNavigate();

    const craftsmanId = localStorage.getItem('craftsmanId');
    const serviceId = localStorage.getItem('selectedServiceId');
    const clientString = localStorage.getItem('loggedInUser');
    const client = JSON.parse(clientString);
    //console.log(craftsmanId + " .. " + serviceId);

    const url = "http://localhost:5213/api/deal";
    const request = {
        "craftsmanId": craftsmanId,
        "clientId": client.id,
        "offeredServiceId": serviceId
    }

    useEffect(() => {
        if (submitted) {

            console.log("Going to fetch...");
            console.log(request);

            setLoading(true);

            const fetchDeal = async () => {
                try {
                    const response = await fetch(url, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': 'Bearer ' + client.token
                        },
                        body: JSON.stringify(request),
                    });

                    console.log(response);

                    if (response.ok) {
                        setMessage("We've sent your demand to the craftsman.");
                    } else {
                        setMessage("Unsuccessful demand. Try again later.");
                    }

                } catch (error) {
                    console.error('Error:', error);
                }
                setLoading(false);
            };
            fetchDeal();

            setTimeout(() => navigate('/'), 1500);
        }
    }, [submitted]);

    if (loading) {
        return < Loading />
    };

    return (
        <>
            <NavigationBar />

            {!message &&
                <ContactForm setSubmitted={setSubmitted} />
            }

            {message &&
                <>
                    <Container className="contact-message">
                        {message}
                    </Container>
                    <Container>
                        <hr />
                    </Container>
                </>
            }

        </>
    )
}

export default Contact;