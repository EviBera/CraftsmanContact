import React from "react";
import { useContext, useState } from "react";
import { OfferedServiceContext } from '../../Pages/OfferedServiceContext';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import './ServiceHandlerTable.css';
import Modal from 'react-bootstrap/Modal';


const ServiceHandlerTable = ({ props: { servicesOfUser, setRemovables, setRecordables } }) => {

    const { offeredServices } = useContext(OfferedServiceContext);
    const [toBeRemoved, setToBeRemoved] = useState(null);
    const [toBeRegistered, setToBeRegistered] = useState(null);
    const [show, setShow] = useState(false);
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

        setToBeRemoved(servicesToRemove);
        setToBeRegistered(servicesToRegister);
        setShow(true);
    }

    const findName = (serviceId) => {

        for (let i = 0; i < offeredServices.length; i++) {
            if (offeredServices[i].offeredServiceId === serviceId)
                return offeredServices[i].offeredServiceName;
        }
    }

    const handleClick = () => {
        setRecordables(toBeRegistered);
        setRemovables(toBeRemoved);
        setShow(false);
        resetCheckboxes();
    }

    const handleCancel = () => {
        setToBeRegistered(null);
        setToBeRemoved(null);
        setShow(false);
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

            
                    <Modal show={show}  >
                        <Modal.Header>
                            <Modal.Title >Modifications</Modal.Title>
                            <hr></hr>
                        </Modal.Header>
                        <Modal.Body>
                            <strong>Add</strong>
                        {toBeRegistered && toBeRegistered.length > 0 ?  
                            <>
                                {toBeRegistered.length === 1 ?
                                    <p>
                                        You would like to register this service:
                                    </p> :
                                    <p>
                                        You would like to register these services:
                                    </p>}
                                {toBeRegistered.map((service) =>
                                    <p key={service}>
                                        {findName(service)}
                                    </p>)}
                            </> :
                            <p>-</p> }
                        <hr></hr>
                        <strong>Remove</strong>
                        {toBeRemoved && toBeRemoved.length > 0 ?
                            <>
                                {toBeRemoved.length === 1 ?
                                    <p>
                                        You would like to cancel this service:
                                    </p> :
                                    <p>
                                        You would like to cancel these services:
                                    </p>}
                                {toBeRemoved.map((service) =>
                                    <p key={service}>
                                        {findName(service)}
                                    </p>
                                )}
                            </> : 
                            <p>-</p> }
                        <Row className='m-3'>
                            <Col>
                                <Button className='btn' onClick={() => handleClick()}>OK</Button>
                            </Col>
                            <Col>
                                <Button className='btn' onClick={() => handleCancel()}>Cancel</Button>
                            </Col>
                        </Row>
                        </Modal.Body>
                    </Modal>
            
        </>
    )
}

export default ServiceHandlerTable;