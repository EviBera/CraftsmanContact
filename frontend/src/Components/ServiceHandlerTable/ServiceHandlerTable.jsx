import React from "react";
import { useContext } from "react";
import { OfferedServiceContext } from '../../Pages/OfferedServiceContext';
import './ServiceHandlerTable.css';


const ServiceHandlerTable = (servicesOfUser) => {

    const { offeredServices } = useContext(OfferedServiceContext);

    const listOfServices = servicesOfUser.servicesOfUser;
    console.log(listOfServices);
    const listOfServiceIds = [];

    if (listOfServices) {

        listOfServices.forEach(element => {
            listOfServiceIds.push(element.offeredServiceId)
        });

        console.log(listOfServiceIds);
    }

    return (
        <>
            <h1 className="title">My services</h1>
            <table>
                <thead>
                    <tr>
                        <th>Service name</th>
                        <th>Do I offer this service?</th>
                        <th>I'd like to modify</th>
                    </tr>
                </thead>
                <tbody>
                    {offeredServices && offeredServices.map((service) => (
                        <tr key={service.offeredServiceId}>
                            <td>{service.offeredServiceName}</td>
                            <td>{listOfServiceIds.indexOf(service.offeredServiceId) === -1 ? 'No' : 'Yes'}</td>
                            <td>{listOfServiceIds.indexOf(service.offeredServiceId) === -1 ?
                                <button className='servicehandler-btn'>Add service</button> :
                                <button className='servicehandler-btn'>Remove service</button>}</td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <hr></hr>
            {servicesOfUser.servicesOfUser && servicesOfUser.servicesOfUser.map((service) => (
                <div key={`service${service.offeredServiceId}`}>{service.offeredServiceName} .. {service.offeredServiceId}</div>
            ))}
        </>

    )
}

export default ServiceHandlerTable;