import React from "react";
import { useEffect, useState } from "react";
import Loading from "../Components/Loading";
import RegistrationForm from "../Components/RegistrationForm";


const Register = () => {

    //const [loading, setLoading] = useState(true);
    const [request, setRequest] = useState({});
    

    useEffect(() => {
        if (Object.keys(request).length !== 0) {
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
                    const data = await response.json();

                    console.log(data); // Handling the response data
                } catch (error) {
                    console.error('Error:', error);
                }
                //setLoading(false);
            };
            fetchData();
        }
    }, [request]);

    /* if (loading) {
        return < Loading />
    }; */

    return (
        < RegistrationForm setRequest={setRequest} />
    );

};

export default Register;