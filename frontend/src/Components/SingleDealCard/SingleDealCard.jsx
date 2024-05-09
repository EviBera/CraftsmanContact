import { useOutletContext } from 'react-router-dom';
import { CardBody, CardText, CardTitle } from 'react-bootstrap';
import Card from 'react-bootstrap/Card';
import CloseButton from 'react-bootstrap/CloseButton';
import './SingleDealCard.css';

const SingleDealCard = (props) => {

    const { id, selectedDeal, handleAcceptDeal, handleCloseDeal } = props.props;

    const context = useOutletContext();
    const setSelectedDeal = context.setSelectedDeal;
    const setHasSingleDeal = context.setHasSingleDeal;
    const storedLoggedInUser = context.storedLoggedInUser;

    const DateConverter = (input) => {
        let date = new Date(input);
        let year = date.getFullYear();
        let month = date.getMonth() + 1;
        let day = date.getDate();

        return day + "/" + month + "/" + year;
    }

    const handleClick = () => {
        setSelectedDeal(null);
        setHasSingleDeal(false);
    }

    const handleAcceptance = () => {
        console.log("Accept button is clicked/should do the patch!!!");
        handleAcceptDeal();
    }

    const handleClose = () => {
        console.log("Close button is clicked");
        handleCloseDeal();
    }

    return (
        <Card style={{ width: '28rem' }} className='single-deal-card'>
            <CardBody>
                <CardTitle>
                    <p>Requested service:</p>
                    <p>{selectedDeal.offeredServiceName}</p>
                    <p>Deal id: {id}</p>
                    <CloseButton onClick={handleClick} />
                    <hr></hr>
                </CardTitle>
                {selectedDeal &&
                    <>
                        <CardText>Craftsman: {selectedDeal.craftsmanName}</CardText>
                        <CardText>Client: {selectedDeal.clientName}</CardText>
                        <CardText>Date of request: {DateConverter(selectedDeal.createdAt)}</CardText>
                        <hr></hr>

                        {selectedDeal.craftsmanId === storedLoggedInUser.id ?
                            <>
                                <CardText>Have I accepted the request? {selectedDeal.isAcceptedByCraftsman ? 'Yes' :
                                    <>
                                        Not yet...
                                        <button className="single-deal-button" onClick={handleAcceptance}>I accept this deal request</button>
                                    </>}
                                </CardText>
                                <CardText>Have I closed this deal? {selectedDeal.isClosedByCraftsman ? 'Yes' :
                                    <>
                                        Not yet...
                                        <button className="single-deal-button" onClick={handleClose}>I close this deal</button>
                                    </>}
                                </CardText>
                            </> :
                            <>
                                <CardText>Has the craftsman accepted the request? {selectedDeal.isAcceptedByCraftsman ? 'Yes' : 'Not yet'}</CardText>
                                <CardText>Has the craftsman closed this deal? {selectedDeal.isClosedByCraftsman ? 'Yes' : 'Not yet'}</CardText>
                            </>
                        }


                        {selectedDeal.clientId === storedLoggedInUser.id ?
                            <>
                                <CardText>Have I closed this deal? {selectedDeal.isClosedByClient ? 'Yes' :
                                    <>
                                        Not yet...
                                        <button className="single-deal-button" onClick={handleClose}>I close this deal</button>
                                    </>}
                                </CardText>
                            </> :
                            <CardText>Has the client closed this deal? {selectedDeal.isClosedByClient ? 'Yes' : 'Not yet'}</CardText>}
                    </>
                }
            </CardBody>

        </Card>
    )
}

export default SingleDealCard;