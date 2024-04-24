import React, { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import ContactForm from "../Components/ContactForm";


const fetchDeal = async (request, token) => {

    const url = "http://localhost:5213/api/deal";

    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        },
        body: JSON.stringify(request),
    });
    console.log(response);
    if (response.ok) {
        console.log("Success");
    } else {
        console.log("Failed");
        console.error(response.text());
    }
}

const Contact = () => {

    const [submitted, setSubmitted] = useState(false);
    const navigate = useNavigate();

    const craftsmanId = localStorage.getItem('craftsmanId');
    const serviceId = localStorage.getItem('selectedServiceId');
    const clientString = localStorage.getItem('loggedInUser');
    const client = JSON.parse(clientString);
    //console.log(craftsmanId + " .. " + serviceId);

    const request = {
        "craftsmanId": craftsmanId,
        "clientId": client.id,
        "offeredServiceId": serviceId
    }

    useEffect(() => {
        if (submitted) {
            console.log("Going to fetch...");
            console.log(request);
            fetchDeal(request, client.token);
            setTimeout(() => navigate('/'), 1000);
        }
    }, [submitted]);

    return (
        <>
        <ContactForm setSubmitted={setSubmitted} />
        
        </>
    )
}

export default Contact;