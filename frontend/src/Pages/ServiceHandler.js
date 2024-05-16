import React, { useEffect, useState } from "react";
import NavigationBar from "./NavigationBar";
import ServiceHandlerTable from '../Components/ServiceHandlerTable'
import { URLS } from "../Config/urls";

const ServiceHandler = () => {

    const storedLoggedInUserString = localStorage.getItem('loggedInUser');
    const storedLoggedInUser = JSON.parse(storedLoggedInUserString);
    const headers = { 'Authorization': 'Bearer ' + storedLoggedInUser.token };

    const [servicesOfUser, setServicesOfUser] = useState(null);


    useEffect(() => {

      fetch(URLS.user.servicesByUser(storedLoggedInUser.id), { headers })
        .then(response => response.json())
        .then(data => {
            setServicesOfUser(data)})
    }, []);
    

    return(
        <>
        <NavigationBar />
        <ServiceHandlerTable servicesOfUser = { servicesOfUser } />
        </>
    )
}

export default ServiceHandler;