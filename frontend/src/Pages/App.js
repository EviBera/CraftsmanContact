import 'bootstrap/dist/css/bootstrap.min.css';
import OfferedServiceContext from './OfferedServiceContext';
import { useState } from 'react';
import logo from '../logo.svg';
import './App.css';
//import { Outlet } from 'react-router-dom';
import OfferedServiceList from './OfferedServiceList';
import CraftsmenByServiceList from './CraftsmenByServiceList';
import NavigationBar from './NavigationBar';

function App() {

  const [selectedService, setSelectedService] = useState(null);

  return (
    <div className="App">

      <NavigationBar/>
      
      <div className='main'>
        <OfferedServiceContext.Provider value={{ selectedService, setSelectedService }}>
          <OfferedServiceList />
          {selectedService && <CraftsmenByServiceList />}
        </OfferedServiceContext.Provider>
      </div>
      
    </div>
  );
}

export default App;
