import logo from '../logo.svg';
import './App.css';
import { Outlet } from 'react-router-dom';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <p>
          Welcome to the Craftsman Contact app!
        </p>
      </header>
      <div>
        <Outlet/>
      </div>
    </div>
  );
}

export default App;
