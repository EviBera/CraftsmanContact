import "./DealTable.css";
import { useState, useEffect } from "react";

const DealTable = (props) => {

    const deals = Array.isArray(props.props.deals) ? props.props.deals : [];
    const headers = props.props.headers;

    const [serviceNames, setServiceNames] = useState({});
    const [craftsmenNames, setCraftsmenNames] = useState({});

    useEffect(() => {
        if (deals.length > 0) {

            const fetchServiceNames = async () => {
                let names = {};
                for (const deal of deals) {
                    const url = `http://localhost:5213/api/offeredservice/${deal.offeredServiceId}`;
                    const serviceData = await fetch(url, { headers });
                    const serviceDataJson = await serviceData.json();
                    names[deal.offeredServiceId] = serviceDataJson.offeredServiceName;
                }
                setServiceNames(names);
            };

            const fetchCraftsmenName = async () => {
                let names = {};
                for (const deal of deals){
                    const url = `http://localhost:5213/api/user/${deal.craftsmanId}`;
                    const craftsmanData = await fetch(url, { headers });
                    const craftsmanDataJson = await craftsmanData.json();
                    names[deal.craftsmanId] = craftsmanDataJson.firstName + " " + craftsmanDataJson.lastName;
                }
                setCraftsmenNames(names);
            }

            fetchServiceNames();
            fetchCraftsmenName();
        };
    }, [deals, headers]);

    const DateConverter = (input) => {
        let date = new Date(input);
        let year = date.getFullYear();
        let month = date.getMonth() +1;
        let day = date.getDate();

        return day + "/" + month + "/" + year;
    }


    return (
        <table>
            <thead>
                <tr >
                    <th colSpan="6" className='title'>
                        My deals
                    </th>
                </tr>

                <tr>
                    <th>No.</th>
                    {/* <th>Deal Id</th> */}
                    <th>Craftsman</th>
                    <th>Service</th>
                    <th>Date of my request (d/m/yyyy)</th>
                    <th>Is it accepted by the craftsman?</th>
                </tr>
            </thead>
            <tbody>
                {deals && deals.map((deal) => (
                    <tr key={deal.dealId}>
                        <td>{deals.indexOf(deal) + 1}</td>
                        {/* <td>{deal.dealId}</td> */}
                        <td>{craftsmenNames[deal.craftsmanId]}</td>
                        <td>{serviceNames[deal.offeredServiceId]}</td>
                        <td>{DateConverter(deal.createdAt)}</td>
                        <td>{deal.isAcceptedByCraftsman ? 'Yessss!' : 'Not yet.'}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    )
}

export default DealTable;