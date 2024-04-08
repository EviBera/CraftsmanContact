import { useState, useEffect } from "react";
import Loading from "../Components/Loading";
import CraftsmenByServiceTable from "../Components/CraftsmenByServiceTable";

const fetchCraftsmenByService = (url) => {
    return fetch(url).then((res) => res.json());
  };

const CraftsmenByServiceList = (serviceId) => {

    const [loading, setLoading] = useState(true);
    const [craftsmen, setCraftsmen] = useState(null);

    const url = "http://localhost:5213/api/user/craftsmenbyservice/" + serviceId;

    useEffect(() => {
        fetchCraftsmenByService(url)
        .then((craftsmen) => {
            setTimeout(() => setLoading(false), 2000) ;
            setCraftsmen(craftsmen);
        })
    }, [url]);

    if(loading){
        return < Loading />
    }

    return (
        <CraftsmenByServiceTable craftsmen = {craftsmen}/>
    )
}

export default CraftsmenByServiceList;