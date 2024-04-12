import { useState, useEffect, useContext } from "react";
import Loading from "../Components/Loading";
import CraftsmenByServiceTable from "../Components/CraftsmenByServiceTable";
import OfferedServiceContext from './OfferedServiceContext';


const fetchCraftsmenByService = (url) => {
    return fetch(url).then((res) => res.json());
  };

const CraftsmenByServiceList = () => {

    const [loading, setLoading] = useState(true);
    const [craftsmen, setCraftsmen] = useState(null);

    const { selectedService } = useContext(OfferedServiceContext);

    const url = "http://localhost:5213/api/user/craftsmenbyservice/" + selectedService.offeredServiceId;
    const serviceName = selectedService.offeredServiceName;

    useEffect(() => {
        fetchCraftsmenByService(url)
        .then((craftsmen) => {
            setTimeout(() => setLoading(false), 1000) ;
            setCraftsmen(craftsmen);
        })
    }, [url]);

    if(loading){
        return < Loading />
    }

    return (
        <CraftsmenByServiceTable craftsmen = {{craftsmen, serviceName}}/>
    )
}

export default CraftsmenByServiceList;