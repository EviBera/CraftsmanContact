import { useState, useEffect } from "react";
import Loading from "../Components/Loading";
import NavigationBar from "./NavigationBar";
import DealTable from "../Components/DealTable";


const DealList = () => {

    const storedLoggedInUserString = localStorage.getItem('loggedInUser');
    const storedLoggedInUser = JSON.parse(storedLoggedInUserString);

    const [loading, setLoading] = useState(false);
    const [deals, setDeals] = useState(null);

    const url = "http://localhost:5213/api/deal/byuser/" + storedLoggedInUser.id;

    useEffect(() => {
        setLoading(true);
        const headers = { 'Authorization': 'Bearer ' + storedLoggedInUser.token };
        fetch(url, { headers })
        .then(response => response.json())
        .then(data => setDeals(data))
        .then(() =>
            setLoading(false)
        )
    }, [url]);

    if(loading){
        return < Loading />
    }

    return (
        <>
        <NavigationBar/>
        <DealTable deals = {deals}/>
        </>
    )
}

export default DealList;