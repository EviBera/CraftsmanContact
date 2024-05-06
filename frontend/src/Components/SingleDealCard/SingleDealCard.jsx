import { CardBody, CardText, CardTitle } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import './SingleDealCard.css';

const SingleDealCard = (props) => {

    const id = props.props.id;
    const selectedDeal = props.props.selectedDeal;

    const DateConverter = (input) => {
        let date = new Date(input);
        let year = date.getFullYear();
        let month = date.getMonth() + 1;
        let day = date.getDate();

        return day + "/" + month + "/" + year;
    }

    return (
        <Card style={{ width: '28rem' }} className='single-deal-card'>
            <CardBody>
                <CardTitle>Single Deal page, id: {id}</CardTitle>
                {selectedDeal &&
                    <CardText>
                        <p>(deal id: {selectedDeal.dealId})</p>
                        <p>Craftsman: {selectedDeal.craftsmanId}</p>
                        <p>Client: {selectedDeal.clientId}</p>
                        <p>Requested service: {selectedDeal.offeredServiceId}</p>
                        <p>Date of request: {DateConverter(selectedDeal.createdAt)}</p>
                        <p>Has the craftsman accepted the request? {selectedDeal.isAcceptedByCraftsman ? 'yes' : 'no'}</p>
                        <p>Has the craftsman closed this deal? {selectedDeal.isClosedByCraftsman ? 'yes' : 'no'}</p>
                        <p>Has the client closed this deal? {selectedDeal.isClosedByClient ? 'yes' : 'no'}</p>
                    </CardText>
                }
            </CardBody>

        </Card>
    )
}

export default SingleDealCard;