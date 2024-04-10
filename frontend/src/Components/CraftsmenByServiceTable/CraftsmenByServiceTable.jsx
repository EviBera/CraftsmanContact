import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

const CraftsmenByServiceTable = (props) => {

    let masters = Object.values(props.craftsmen)[0];
    let first = masters[0];

    return (
        <Container>
            <Row>
                <Col>Service : {props.craftsmen.name}</Col>
            </Row>
            {first ? (
                <>
                    <Row>
                        <Col>Name</Col>
                        <Col>Email</Col>
                    </Row>

                    {masters.map((master) => (
                        <Row key={master.id}>
                            <Col >{master.firstName} {master.lastName}</Col>
                            <Col>{master.email}</Col>
                        </Row>
                    ))}
                </>
            ) : (
                <Row>
                    <Col>Sorry, we cannot offer a craftsman.</Col>
                </Row>)}
        </Container>
    )
}

export default CraftsmenByServiceTable;