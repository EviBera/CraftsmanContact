import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './Pages/App';
import reportWebVitals from './reportWebVitals';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import ErrorPage from './Pages/ErrorPage';
import Login from './Pages/Login';
import Registration from './Pages/Registration';
import Contact from './Pages/Contact';
import DealList from './Pages/DealList';
import SingleDeal from './Pages/SingleDeal';
import ServiceList from './Pages/ServiceList';


const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <ErrorPage />,
    /* children: [
      {
        path: "/services",
        element: <OfferedServiceList />,
      },
      {
        path: "/craftsmen/:service",
        element: <CraftsmenByServiceList />,
      },
    ] */
  },
  {
    path: "/login",
    element : <Login />,
    errorElement: <ErrorPage />,
  },
  {
    path: "/register",
    element : <Registration />,
    errorElement: <ErrorPage />,
  },
  {
    path: "/contact",
    element : <Contact />,
    errorElement: <ErrorPage />,
  },
  {
    path: "/deals",
    element : <DealList />,
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
    element: <ServiceList />,
    errorElement: <ErrorPage />,
  }
]);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
