import { useContext } from "react";
import { OfferedServiceContext } from './OfferedServiceContext';
import NavigationBar from "./NavigationBar";
import ServiceHandlerTable from '../Components/ServiceHandlerTable'

const ServiceHandler = () => {

    const { offeredServices } = useContext(OfferedServiceContext);

    return(
        <>
        <NavigationBar />
        {offeredServices && offeredServices.map((service) => (
            <div key={service.offeredServiceId}>{service.offeredServiceName}</div>
        ))}
        <ServiceHandlerTable />
        </>
    )
}

export default ServiceHandler;