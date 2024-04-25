import { useEffect, useState } from "react";
import Loading from "../Components/Loading";
import OfferedServiceTable from "../Components/OfferedServiceTable";


const fetchServices = (url) => {
    return fetch(url).then((res) => res.json());
  };

const OfferedServiceList = () => {
    const [loading, setLoading] = useState(true);
    const [offeredServices, setOfferedServices] = useState(null);
   

    const url = "http://localhost:5213/api/offeredservice/all";

    useEffect(() => {
        fetchServices(url)
        .then((offeredServices) => {
            setTimeout(() => setLoading(false), 1000) ;
            setOfferedServices(offeredServices);
        })
    }, []);

    if(loading){
        return < Loading />
    }

    return (
            <OfferedServiceTable offeredServices = {offeredServices}/>
    );
    
};

export default OfferedServiceList;