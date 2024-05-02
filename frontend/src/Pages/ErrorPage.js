import { useRouteError } from "react-router-dom";
import './App.css'
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

const ErrorPage = () => {

  const error = useRouteError();
  console.error(error);

  return (
    <Container className="error-page">
      <Row>
        <Col>
          <h1>Oops!</h1>
        </Col>
      </Row>
      <Row>
        <Col>
          <p>Sorry, an unexpected error has occurred.</p>
          <p>
            <i>{error.statusText || error.message}</i>
          </p>
        </Col>
      </Row>
      <Row>
        <a href="/">Home</a>
      </Row>
    </Container>
  )

}

export default ErrorPage;