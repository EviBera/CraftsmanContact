import { Container } from 'react-bootstrap';
import Table from 'react-bootstrap/Table';
import "./DealTable.css";

const DealTable = (props) => {

    console.log(props);
    const deals = props.deals;
    console.log(deals);

    const ConvertBoolToString = (bool) => {
        if(bool) 
            return 'Yes'
        else
         return 'Not yet.'
    }

    return (
        /*         <>
        {deals && deals.map((deal) => (<p key={deal.dealId}>{deal.dealId}</p>))}
        </>
        */
        <>
            <Container className='title'>
                <h1>My deals</h1>
            </Container>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>No.</th>
                        <th>Craftsman</th>
                        <th>Service</th>
                        <th>Datum of request</th>
                        <th>Is it accepted by the craftsman?</th>
                    </tr>
                </thead>
                <tbody>
                    {deals && deals.map((deal) => (
                        <tr key={deal.dealId}>
                            <td></td>
                            <td>{deal.craftsmanId}</td>
                            <td>{deal.offeredServiceId}</td>
                            <td>{deal.createdAt}</td>
                            <td>{ConvertBoolToString(deal.isAcceptedByCraftsman)}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>

        </>)
}

export default DealTable;