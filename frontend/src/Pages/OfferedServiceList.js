import React, { useContext } from "react";
import { OfferedServiceContext } from "./OfferedServiceContext";
import Loading from "../Components/Loading";
import OfferedServiceTable from "../Components/OfferedServiceTable";


const OfferedServiceList = () => {
    
    const { loading } = useContext(OfferedServiceContext);

    if(loading){
        return < Loading />
    }

    return (
            <OfferedServiceTable />
    );
    
};

export default OfferedServiceList;