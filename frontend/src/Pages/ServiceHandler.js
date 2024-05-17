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
            setServicesOfUser(data)})
    }, []);
    
    console.log(removables);
    console.log(recordables);

    const triggerUpdate = () => {
        setUpdateTrigger(current => current + 1);
    }

    return(
        <>
        <NavigationBar />
        <ServiceHandlerTable props = { { servicesOfUser, setRemovables, setRecordables, triggerUpdate } } />
        </>
    )
}

export default ServiceHandler;