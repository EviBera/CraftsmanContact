import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import Button from 'react-bootstrap/Button';
import './CraftsmenByServiceTable.css';
import { useNavigate } from 'react-router-dom';

const CraftsmenByServiceTable = (props) => {

    let masters = Object.values(props.craftsmen)[0];
    let first = masters[0];

    const storedLoggedInUserString = localStorage.getItem('loggedInUser');

    const navigate = useNavigate();

    const handleClick = (id, serviceName, craftsmanName) => {

        console.log(id);
        localStorage.setItem('craftsmanId', id);
        localStorage.setItem('serviceName', serviceName);
        localStorage.setItem('craftsmanName', craftsmanName);
        navigate("/contact");
    }

    return (
        <Container className='cards'>
            <Row className='label mb-3'>
                <Col>Service : {props.craftsmen.serviceName}</Col>
            </Row>
            {first ? (
                <>
                    <Row xs={1} md={2} className="g-4 justify-content-center mt-3">
                        {masters.map((master) => (
                            <Card style={{ width: '18rem' }} key={master.id} className='card m-2'>
                                <Card.Title className='title'>{master.firstName} {master.lastName}</Card.Title>
                                {storedLoggedInUserString &&
                                    <ListGroup variant="flush" >
                                        <ListGroup.Item>Email: {master.email}</ListGroup.Item>
                                        <ListGroup.Item>Phone: {master.phone}</ListGroup.Item>
                                        <ListGroup.Item><Button className='btn' onClick={() => 
                                            handleClick(master.id, props.craftsmen.serviceName, master.firstName + " " + master.lastName)}>Contact</Button></ListGroup.Item>
                                    </ListGroup>
                                }
                            </Card>
                        ))}

                    </Row>
                    {!storedLoggedInUserString &&
                        <Container>
                            If you would like to contact the craftsman, you have to log in.
                        </Container>
                    }
                </>
            ) : (
                <Row>
                    <Col>Sorry, we cannot offer a craftsman.</Col>
                </Row>)}
        </Container>
    )
}

export default CraftsmenByServiceTable;