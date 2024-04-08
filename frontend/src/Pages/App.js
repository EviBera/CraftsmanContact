import logo from '../logo.svg';
import './App.css';
//import { Outlet } from 'react-router-dom';
import OfferedServiceList from './OfferedServiceList';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <p>
          Welcome to the Craftsman Contact app!
        </p>
      </header>
      <body>
        <OfferedServiceList />
      </body>
        
      
    </div>
  );
}

export default App;
