import "./DealTable.css";
import Accordion from 'react-bootstrap/Accordion';

const DealTable = (props) => {

    const deals = Array.isArray(props.props.deals) ? props.props.deals : [];
    const first = deals[0];
    const serviceNames = props.props.serviceNames;
    const craftsmenNames = props.props.craftsmenNames;
    const clientNames = props.props.clientNames;
    const storedLoggedInUser = props.props.storedLoggedInUser;


    const DateConverter = (input) => {
        let date = new Date(input);
        let year = date.getFullYear();
        let month = date.getMonth() + 1;
        let day = date.getDate();

        return day + "/" + month + "/" + year;
    }


    return (
        <>
            <h1 className="title">My deals</h1>
            <div className="suggestion">
                <p>
                    If you would like to check the details, click the number at the beginning of the row!
                </p>
            </div>
            <Accordion className="accordion">
                <Accordion.Item eventKey="0" className="accordion-item">
                    <Accordion.Header className="accordion-header">Every deal</Accordion.Header>
                    <Accordion.Body>
                        <table>
                            <thead>
                                <tr>
                                    <th>No.</th>
                                    <th>Craftsman</th>
                                    <th>Client</th>
                                    <th>Service</th>
                                    <th>Date of my request (d/m/yyyy)</th>
                                    <th>Is it accepted by the craftsman?</th>
                                </tr>
                            </thead>
                            <tbody>
                                {deals && deals.map((deal) => (
                                    <tr key={deal.dealId}>
                                        <td><a href={`/deals/${deal.dealId}`} className="link-to-deal">{deals.indexOf(deal) + 1}</a></td>
                                        <td>{craftsmenNames[deal.craftsmanId]}</td>
                                        <td>{clientNames[deal.clientId]}</td>
                                        <td>{serviceNames[deal.offeredServiceId]}</td>
                                        <td>{DateConverter(deal.createdAt)}</td>
                                        <td>{deal.isAcceptedByCraftsman ? 'Yessss!' : 'Not yet.'}</td>
                                    </tr>
                                ))}
                                {!first &&
                                    <tr>
                                        <td colSpan="6" className="information">
                                            You don't have a deal yet.
                                        </td>
                                    </tr>}
                            </tbody>
                        </table>
                    </Accordion.Body>
                </Accordion.Item>
                <Accordion.Item eventKey="1">
                    <Accordion.Header>I am the provider</Accordion.Header>
                    <Accordion.Body>
                        <table>
                            <thead>
                                <tr>
                                    <th>No.</th>
                                    <th>Client</th>
                                    <th>Service</th>
                                    <th>Date of my request (d/m/yyyy)</th>
                                    <th>Is it accepted by me?</th>
                                </tr>
                            </thead>
                            <tbody>
                                {deals && deals.filter((deal) => deal.craftsmanId === storedLoggedInUser.id).map((deal) => (
                                    <tr key={deal.dealId}>
                                        <td><a href={`/deals/${deal.dealId}`} className="link-to-deal">{deals.filter((deal) => deal.craftsmanId === storedLoggedInUser.id).indexOf(deal) + 1}</a></td>
                                        <td>{clientNames[deal.clientId]}</td>
                                        <td>{serviceNames[deal.offeredServiceId]}</td>
                                        <td>{DateConverter(deal.createdAt)}</td>
                                        <td>{deal.isAcceptedByCraftsman ? 'Yessss!' : 'Not yet.'}</td>
                                    </tr>
                                ))}
                                {(!first || deals.filter((deal) => deal.craftsmanId === storedLoggedInUser.id).length === 0) &&
                                    <tr>
                                        <td colSpan="6" className="information">
                                            You don't have such a deal yet.
                                        </td>
                                    </tr>}
                            </tbody>
                        </table>
                    </Accordion.Body>
                </Accordion.Item>
                <Accordion.Item eventKey="2">
                    <Accordion.Header>I am the client</Accordion.Header>
                    <Accordion.Body>
                        <table>
                            <thead>
                                <tr>
                                    <th>No.</th>
                                    <th>Craftsman</th>
                                    <th>Service</th>
                                    <th>Date of my request (d/m/yyyy)</th>
                                    <th>Is it accepted by the craftsman?</th>
                                </tr>
                            </thead>
                            <tbody>
                                {deals && deals.filter((deal) => deal.clientId === storedLoggedInUser.id).map((deal) => (
                                    <tr key={deal.dealId}>
                                        <td><a href={`/deals/${deal.dealId}`} className="link-to-deal">{deals.filter((deal) => deal.clientId === storedLoggedInUser.id).indexOf(deal) + 1}</a></td>
                                        <td>{craftsmenNames[deal.craftsmanId]}</td>
                                        <td>{serviceNames[deal.offeredServiceId]}</td>
                                        <td>{DateConverter(deal.createdAt)}</td>
                                        <td>{deal.isAcceptedByCraftsman ? 'Yessss!' : 'Not yet.'}</td>
                                    </tr>
                                ))}
                                {(!first || deals.filter((deal) => deal.clientId === storedLoggedInUser.id).length === 0) &&
                                    <tr>
                                        <td colSpan="6" className="information">
                                            You don't have such a deal yet.
                                        </td>
                                    </tr>}
                            </tbody>
                        </table>
                    </Accordion.Body>
                </Accordion.Item>
            </Accordion>
        </>
    )
}

export default DealTable;