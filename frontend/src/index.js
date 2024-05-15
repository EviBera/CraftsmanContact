import React from 'react';
import { ServiceProvider } from './Pages/OfferedServiceContext';
import ReactDOM from 'react-dom/client';
import reportWebVitals from './reportWebVitals';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import App from './Pages/App';
import ErrorPage from './Pages/ErrorPage';
import Login from './Pages/Login';
import Registration from './Pages/Registration';
import Contact from './Pages/Contact';
import DealList from './Pages/DealList';
import SingleDeal from './Pages/SingleDeal';
import ServiceHandler from './Pages/ServiceHandler';
import './index.css';


const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <ErrorPage />,
  },
  {
    path: "/login",
    element: <Login />,
    errorElement: <ErrorPage />,
  },
  {
    path: "/register",
    element: <Registration />,
    errorElement: <ErrorPage />,
  },
  {
    path: "/contact",
    element: <Contact />,
    errorElement: <ErrorPage />,
  },
  {
    path: "/deals",
    element: <DealList />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: ":id",
        element: < SingleDeal />,
      },
    ]
  },
  {
    path: "/services",
    element: <ServiceHandler />,
    errorElement: <ErrorPage />,
  }
]);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <ServiceProvider>
      <RouterProvider router={router} />
    </ServiceProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
