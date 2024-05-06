import { CardBody, CardText, CardTitle } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import CloseButton from 'react-bootstrap/CloseButton';
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

    const handleClick = () => {
        console.log("Close button clicked");
    }

    return (
        <Card style={{ width: '28rem' }} className='single-deal-card'>
            <CardBody>
                <CardTitle>
                    Single Deal page, id: {id}
                    <CloseButton onClick={() => handleClick()} />
                </CardTitle>
                {selectedDeal &&
                    <>
                        <CardText>(deal id: {selectedDeal.dealId})</CardText>
                        <CardText>Craftsman: {selectedDeal.craftsmanId}</CardText>
                        <CardText>Client: {selectedDeal.clientId}</CardText>
                        <CardText>Requested service: {selectedDeal.offeredServiceId}</CardText>
                        <CardText>Date of request: {DateConverter(selectedDeal.createdAt)}</CardText>
                        <CardText>Has the craftsman accepted the request? {selectedDeal.isAcceptedByCraftsman ? 'yes' : 'no'}</CardText>
                        <CardText>Has the craftsman closed this deal? {selectedDeal.isClosedByCraftsman ? 'yes' : 'no'}</CardText>
                        <CardText>Has the client closed this deal? {selectedDeal.isClosedByClient ? 'yes' : 'no'}</CardText>
                    </>
                }
            </CardBody>

        </Card>
    )
}

export default SingleDealCard;