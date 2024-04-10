import 'bootstrap/dist/css/bootstrap.min.css';
import IDContext from './IDContext';
import { useState } from 'react';
import logo from '../logo.svg';
import './App.css';
//import { Outlet } from 'react-router-dom';
import OfferedServiceList from './OfferedServiceList';
import CraftsmenByServiceList from './CraftsmenByServiceList'

function App() {

  const [selectedId, setSelectedId] = useState(null);

  return (
    <div className="App">
      <header className="App-header">
        <p>
          Welcome to the Craftsman Contact app!
        </p>
      </header>
      <body>
        <IDContext.Provider value={{ selectedId, setSelectedId }}>
          <OfferedServiceList />
          {selectedId && <CraftsmenByServiceList />}
        </IDContext.Provider>
      </body>
    </div>
  );
}

export default App;
