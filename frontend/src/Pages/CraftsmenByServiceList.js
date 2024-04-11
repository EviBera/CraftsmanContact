import { useState, useEffect, useContext } from "react";
import Loading from "../Components/Loading";
import CraftsmenByServiceTable from "../Components/CraftsmenByServiceTable";
import IDContext from './IDContext';


const fetchCraftsmenByService = (url) => {
    return fetch(url).then((res) => res.json());
  };

const CraftsmenByServiceList = () => {

    const [loading, setLoading] = useState(true);
    const [craftsmen, setCraftsmen] = useState(null);

    const { selectedId } = useContext(IDContext);

    const url = "http://localhost:5213/api/user/craftsmenbyservice/" + selectedId;
    const serviceName = "Specific service";

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