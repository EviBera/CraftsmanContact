import React from "react";
import { useContext, useState } from "react";
import { OfferedServiceContext } from '../../Pages/OfferedServiceContext';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import './ServiceHandlerTable.css';
import { CardText, CardTitle } from "react-bootstrap";


const ServiceHandlerTable = ({ props: { servicesOfUser, setRemovables, setRecordables, triggerUpdate } }) => {

    const { offeredServices } = useContext(OfferedServiceContext);
    const [toBeRemoved, setToBeRemoved] = useState(null);
    const [toBeRegistered, setToBeRegistered] = useState(null);
    const [showSummary, setShowSummary] = useState(false);
    const [checkedState, setCheckedState] = useState({});

    const listOfServiceIds = servicesOfUser ? servicesOfUser.map(element => element.offeredServiceId) : [];

    const handleSubmit = (e) => {
        e.preventDefault();

        const selectedServices = Object.keys(checkedState).filter(key => checkedState[key]);

        let servicesToRegister = [];
        let servicesToRemove = [];

        selectedServices.forEach(service => {
            service = parseInt(service);
            if (listOfServiceIds.indexOf(service) === -1) {
                servicesToRegister.push(service);
            } else {
                servicesToRemove.push(service);
            }
        });

        console.log("add: " + servicesToRegister);
        console.log("remove: " + servicesToRemove);
        setToBeRemoved(servicesToRemove);
        setToBeRegistered(servicesToRegister);
        setShowSummary(true);
        triggerUpdate();
    }

    const findName = (serviceId) => {

        for (let i = 0; i < offeredServices.length; i++) {
            if (offeredServices[i].offeredServiceId === serviceId)
                return offeredServices[i].offeredServiceName;
        }
    }

    const handleClick = () => {
        console.log('Clicked: OK');
        setRecordables(toBeRegistered);
        setRemovables(toBeRemoved);
        setShowSummary(false);
        triggerUpdate();
        resetCheckboxes();
    }

    const handleCancel = () => {
        console.log('Clicked: Cancel');
        setToBeRegistered(null);
        setToBeRemoved(null);
        setShowSummary(false);
        triggerUpdate();
        resetCheckboxes();
    }

    const resetCheckboxes = () => {
        const resetState = {};
        offeredServices.forEach(service => {
            resetState[service.offeredServiceId] = false;
        });
        setCheckedState(resetState);
    }

    const handleCheckboxChange = (event) => {
        const serviceId = event.target.value;
        setCheckedState(prevState => ({
            ...prevState,
            [serviceId]: event.target.checked,
        }));
    };

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
                                    <input 
                                        type="checkbox" 
                                        id={`service${service.offeredServiceId}`} 
                                        name="service" 
                                        value={service.offeredServiceId}
                                        checked={checkedState[service.offeredServiceId] || false}
                                        onChange={handleCheckboxChange} 
                                    />
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

            {showSummary &&
                <Row className='justify-content-center'>
                    <Card style={{ width: '28rem' }} className='g-4 justify-content-center'>
                        <CardTitle className="card-title">
                            Modifications
                            <hr></hr>
                        </CardTitle>
                        <CardText><strong>Add</strong></CardText>
                        {toBeRegistered.length > 0 ?
                            <>
                                {toBeRegistered.length === 1 ?
                                    <CardText>
                                        You would like to register this service:
                                    </CardText> :
                                    <CardText>
                                        You would like to register these services:
                                    </CardText>}
                                {toBeRegistered.map((service) =>
                                    <CardText key={service}>
                                        {findName(service)}
                                    </CardText>)}
                            </> : <CardText>-</CardText>}
                        <hr></hr>
                        <CardText><strong>Remove</strong></CardText>
                        {toBeRemoved.length > 0 ?
                            <>
                                {toBeRemoved.length === 1 ?
                                    <CardText>
                                        You would like to cancel this service:
                                    </CardText> :
                                    <CardText>
                                        You would like to cancel these services:
                                    </CardText>}
                                {toBeRemoved.map((service) =>
                                    <CardText key={service}>
                                        {findName(service)}
                                    </CardText>
                                )}
                            </> : <CardText>-</CardText>}
                        <Row className='mb-3'>
                            <Col>
                                <Button className='btn' onClick={() => handleClick()}>OK</Button>
                            </Col>
                            <Col>
                                <Button className='btn' onClick={() => handleCancel()}>Cancel</Button>
                            </Col>
                        </Row>
                    </Card>
                </Row>
            }
        </>
    )
}

export default ServiceHandlerTable;