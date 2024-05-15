import { useState, useEffect, useContext } from "react";
import Loading from "../Components/Loading";
import CraftsmenByServiceTable from "../Components/CraftsmenByServiceTable";
import { OfferedServiceContext } from './OfferedServiceContext';
import { URLS } from "../Config/urls";

const fetchCraftsmenByService = (url) => {
    return fetch(url).then((res) => res.json());
  };

const CraftsmenByServiceList = () => {

    const [loading, setLoading] = useState(true);
    const [craftsmen, setCraftsmen] = useState(null);
    const { selectedService } = useContext(OfferedServiceContext);

    const serviceName = selectedService.offeredServiceName;

    
    useEffect(() => {

        if(!selectedService){
            setLoading(false);
            return
        }

        setLoading(true);
        fetchCraftsmenByService(URLS.user.craftsmenByService(selectedService.offeredServiceId))
        .then((craftsmen) => {
            setCraftsmen(craftsmen);
            setLoading(false);
        })
    }, [selectedService]);

    if(loading){
        return < Loading />
    }

    return (
        <CraftsmenByServiceTable craftsmen = {{craftsmen, serviceName}}/>
    )
}

export default CraftsmenByServiceList;