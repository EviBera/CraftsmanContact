import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button';
import './CraftsmenByServiceTable.css';

const CraftsmenByServiceTable = (props) => {

    let masters = Object.values(props.craftsmen)[0];
    let first = masters[0];

    return (
        <Container className='cards'>
            <Row className='label mb-3'>
                <Col>Service : {props.craftsmen.serviceName}</Col>
            </Row>
            {first ? (
                <Row xs={1} md={2} className="g-4 justify-content-center mt-3">
                    {masters.map((master) => (
                        <Card style={{ width: '18rem' }} key={master.id} className='card m-2'>
                            <Card.Title className='title'>{master.firstName} {master.lastName}</Card.Title>
                            <ListGroup variant="flush" >
                                <ListGroup.Item>Email: {master.email}</ListGroup.Item>
                                <ListGroup.Item>Phone: {master.phone}</ListGroup.Item>
                                <ListGroup.Item><Button className='btn'>Contact</Button></ListGroup.Item>
                            </ListGroup>
                        </Card>
                    ))}
                </Row>
            ) : (
                <Row>
                    <Col>Sorry, we cannot offer a craftsman.</Col>
                </Row>)}
        </Container>
    )
}

export default CraftsmenByServiceTable;