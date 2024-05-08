import { useState, useEffect } from "react";
import { Outlet } from "react-router-dom";
import Loading from "../Components/Loading";
import NavigationBar from "./NavigationBar";
import DealTable from "../Components/DealTable";
import { URLS } from "../Config/urls";


const DealList = () => {

    const storedLoggedInUserString = localStorage.getItem('loggedInUser');
    const storedLoggedInUser = JSON.parse(storedLoggedInUserString);
    const headers = { 'Authorization': 'Bearer ' + storedLoggedInUser.token };

    const [loading, setLoading] = useState(true);
    const [deals, setDeals] = useState(null);
    const [serviceNames, setServiceNames] = useState({});
    const [craftsmenNames, setCraftsmenNames] = useState({});
    const [clientNames, setClientNames] = useState({});
    const [selectedDeal, setSelectedDeal] = useState({});
    const [hasSingleDeal, setHasSingleDeal] = useState(false);


    useEffect(() => {

        fetch(URLS.deal.byUser(storedLoggedInUser.id), { headers })
            .then(response => response.json())
            .then(data => {
                setDeals(data);
                return data;
            })
            .then(deals => {
                const servicePromises = deals.map(deal =>
                    fetch(URLS.offeredService.byServiceId(deal.offeredServiceId), { headers })
                        .then(response => response.json())
                        .then(serviceData => {
                            setServiceNames(prev => ({ ...prev, [deal.offeredServiceId]: serviceData.offeredServiceName }));
                        })
                );

                const craftsmenPromises = deals.map(deal =>
                    fetch(URLS.user.byUserId(deal.craftsmanId), { headers })
                        .then(response => response.json())
                        .then(craftsmanData => {
                            setCraftsmenNames(prev => ({ ...prev, [deal.craftsmanId]: `${craftsmanData.firstName} ${craftsmanData.lastName}` }));
                        })
                );

                const clientPromises = deals.map(deal =>
                    fetch(URLS.user.byUserId(deal.clientId), { headers })
                        .then(response => response.json())
                        .then(clientData => {
                            setClientNames(prev => ({ ...prev, [deal.clientId]: `${clientData.firstName} ${clientData.lastName}` }));
                        })
                );

                return Promise.all([...servicePromises, ...craftsmenPromises, ...clientPromises]);
            })
            .then(() => {
                setLoading(false);
            })
            .catch(error => {
                console.error('Failed to fetch data:', error);
                setLoading(false);
            });
    }, []);

    if (loading) {
        return < Loading />
    }

    return (
        <>
            <NavigationBar />
            <DealTable props=
                {{
                    deals,
                    serviceNames,
                    craftsmenNames,
                    clientNames,
                    storedLoggedInUser,
                    setSelectedDeal,
                    hasSingleDeal,
                    setHasSingleDeal
                }} />
            {selectedDeal && (
                <div className="selected-deal-container">
                    <Outlet context=
                        {{
                            selectedDeal,
                            setSelectedDeal,
                            setHasSingleDeal,
                            storedLoggedInUser
                        }} />
                </div>
            )}
        </>
    )
}

export default DealList;