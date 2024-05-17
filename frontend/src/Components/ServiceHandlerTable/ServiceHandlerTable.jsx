import React from "react";
import { useContext } from "react";
import { OfferedServiceContext } from '../../Pages/OfferedServiceContext';
import './ServiceHandlerTable.css';


const ServiceHandlerTable = (servicesOfUser) => {

    const { offeredServices } = useContext(OfferedServiceContext);

    const listOfServices = servicesOfUser.servicesOfUser;
    //console.log(listOfServices);
    const listOfServiceIds = [];

    if (listOfServices) {

        listOfServices.forEach(element => {
            listOfServiceIds.push(element.offeredServiceId)
        });

        //console.log(listOfServiceIds);
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log("clicked");

        const formData = new FormData(e.target);
        const selectedServices = formData.getAll('service');

        console.log('Selected services:', selectedServices);

        let servicesToRegister = [];
        let servicesToRemove = [];

        selectedServices.forEach(service => {
            service = parseInt(service);
            if(listOfServiceIds.indexOf(service) === -1){
                servicesToRegister.push(service);
            } else {
                servicesToRemove.push(service);
            }
        });

        console.log("add: " + servicesToRegister);
        console.log("remove: " + servicesToRemove);
    }

    return (
        <>
            <h1 className="title">My services</h1>
            <form onSubmit={(e) => handleSubmit(e)}>
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
                                <td>
                                    <input type="checkbox" id={`service${service.offeredServiceId}`} name="service" value={service.offeredServiceId} />
                                </td>
                            </tr>
                        ))}
                        <tr className="lastRow">
                            <td colSpan="2"></td>
                            <td >
                                <input type="submit" value="Submit" className="submit-btn"></input>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </form>
            <hr></hr>
            {servicesOfUser.servicesOfUser && servicesOfUser.servicesOfUser.map((service) => (
                <div key={`service${service.offeredServiceId}`}>{service.offeredServiceName} .. {service.offeredServiceId}</div>
            ))}
        </>

    )
}

export default ServiceHandlerTable;