import React, { createContext, useState, useEffect } from 'react';
import { URLS } from "../Config/urls";

const OfferedServiceContext = createContext();
const fetchServices = (url) => {
    return fetch(url).then((res) => res.json());
  };
  
  const ServiceProvider = ({ children }) => {
    const [selectedService, setSelectedService] = useState(null);
    const [offeredServices, setOfferedServices] = useState(null);
    const [loading, setLoading] = useState(true);
  
    useEffect(() => {
      fetchServices(URLS.offeredService.all)
        .then((services) => {
          setOfferedServices(services);
          setLoading(false);
        })
        .catch(error => console.error("Failed to fetch services", error));
    }, []);
  
    // Provide loading state as well to allow consumers to handle loading UI
    return (
      <OfferedServiceContext.Provider value={{ selectedService, setSelectedService, offeredServices, loading }}>
        {children}
      </OfferedServiceContext.Provider>
    );
  };
  
  export { OfferedServiceContext, ServiceProvider };