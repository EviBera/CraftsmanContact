import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import './ContactForm.css';
import { useNavigate } from 'react-router-dom';

const ContactForm = (props) => {

    const navigate = useNavigate();

    const serviceName = localStorage.getItem('serviceName');
    const craftsmanName = localStorage.getItem('craftsmanName');

    const handleCancel = () => {
        navigate('/');
    }

    const handleSubmit = () => {
        props.setSubmitted(true);
        console.log("Submitted");
    }


    return (

        <Row className='g-4 justify-content-center contact-row'>
            <Card className='contact-card'>
                <Card.Title className='mt-3'>Contact information</Card.Title>
                <Card.Text className='mt-1'>Desired service: {serviceName}</Card.Text>
                <Card.Text className='mt-1'>Craftsman: {craftsmanName}</Card.Text>
                <hr/>
                <Row className='mb-3'>
                    <Col>
                        <Button className='btn' onClick={() => handleSubmit()}>Contact</Button>
                    </Col>
                    <Col>
                        <Button className='btn' onClick={() => handleCancel()}>Cancel</Button>
                    </Col>
                </Row>

            </Card>
        </Row>

    )
}

export default ContactForm;