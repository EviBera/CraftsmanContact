import 'bootstrap/dist/css/bootstrap.min.css';
import React, { useContext } from 'react';
import { OfferedServiceContext } from './OfferedServiceContext';
import './App.css';
import OfferedServiceList from './OfferedServiceList';
import CraftsmenByServiceList from './CraftsmenByServiceList';
import NavigationBar from './NavigationBar';

function App() {

  const { selectedService } = useContext(OfferedServiceContext);

  return (
    <div className="App">
      <NavigationBar />
      <div className='main'>
        <OfferedServiceList />
        {selectedService &&
          <CraftsmenByServiceList />
        }
      </div>
    </div>
  );
}

export default App;
