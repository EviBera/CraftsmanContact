import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';

const CraftsmenByServiceTable = (props) => {

    let masters = Object.values(props.craftsmen)[0];
    let first = masters[0];

    return (
        <Container>
            <Row>
                <Col>Service : {props.craftsmen.name}</Col>
            </Row>
            {first ? (
                <Row xs={1} md={2} className="g-4">
                    {masters.map((master) => (
                        <Card style={{ width: '18rem' }}>
                        <ListGroup variant="flush" key={master.id}>
                          <ListGroup.Item>Name: {master.firstName} {master.lastName}</ListGroup.Item>
                          <ListGroup.Item>Email: {master.email}</ListGroup.Item>
                          <ListGroup.Item>Phone: {master.phone}</ListGroup.Item>
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