import { useEffect, useState } from "react";
import Loading from "../Components/Loading";
import OfferedServiceTable from "../Components/OfferedServiceTable";
import { URLS } from "../Config/urls";


const fetchServices = (url) => {
    return fetch(url).then((res) => res.json());
  };

const OfferedServiceList = () => {
    const [loading, setLoading] = useState(true);
    const [offeredServices, setOfferedServices] = useState(null);
   

    useEffect(() => {
        fetchServices(URLS.offeredService.all)
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