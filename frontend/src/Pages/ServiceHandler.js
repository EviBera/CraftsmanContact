import React, { useEffect, useState } from "react";
import NavigationBar from "./NavigationBar";
import ServiceHandlerTable from '../Components/ServiceHandlerTable'
import { URLS } from "../Config/urls";

const ServiceHandler = () => {

    const storedLoggedInUserString = localStorage.getItem('loggedInUser');
    const storedLoggedInUser = JSON.parse(storedLoggedInUserString);
    const headers = { 'Authorization': 'Bearer ' + storedLoggedInUser.token };

    const [servicesOfUser, setServicesOfUser] = useState(null);
    const [removables, setRemovables] = useState(null);
    const [recordables, setRecordables] = useState(null);
    const [updateTrigger, setUpdateTrigger] = useState(0);


    useEffect(() => {

        fetch(URLS.user.servicesByUser(storedLoggedInUser.id), { headers })
            .then(response => response.json())
            .then(data => {
                setServicesOfUser(data)
            })
    }, [updateTrigger]);

    //console.log(removables);
    
    useEffect(() => {
        
        if (recordables) {
            
            console.log(recordables);
            recordables.map((item) => {
                fetch(URLS.user.registerService(storedLoggedInUser.id, item), {
                    method: 'PATCH',
                    headers,
                    body: JSON.stringify({
                        "userId": storedLoggedInUser.id,
                        "serviceId": item
                    }),
                })
                    .then(res => res.text())
                    .then((data) => {
                        console.log(data);
                        triggerUpdate()})
                    .catch(error => {
                        console.error('Failed to fetch data:', error);
                    });
            })
        }

    }, [recordables])


    useEffect(() => {
        
        if (removables) {
            
            console.log(removables);
            removables.map((item) => {
                fetch(URLS.user.removeService(storedLoggedInUser.id, item), {
                    method: 'PATCH',
                    headers,
                    body: JSON.stringify({
                        "userId": storedLoggedInUser.id,
                        "serviceId": item
                    }),
                })
                    .then(res => res.text())
                    .then((data) => {
                        console.log(data);
                        triggerUpdate()})
                    .catch(error => {
                        console.error('Failed to fetch data:', error);
                    });
            })
        }

    }, [removables])


    const triggerUpdate = () => {
        setUpdateTrigger(current => current + 1);
    }

    return (
        <>
            <NavigationBar />
            <ServiceHandlerTable props={{ servicesOfUser, setRemovables, setRecordables }} />
        </>
    )
}

export default ServiceHandler;