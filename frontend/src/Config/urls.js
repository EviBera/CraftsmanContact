const BASE_URL = process.env.REACT_APP_BASE_URL;

export const URLS = {

    deal: {
        addDeal: `${BASE_URL}/deal`,
        byUser: (userId) => `${BASE_URL}/deal/byuser/${userId}`,
        accept: (userId, dealId) => `${BASE_URL}/deal/accept/${userId}/${dealId}`,
        close: (dealId, userId) => `${BASE_URL}/deal/close/${dealId}/${userId}`
    },

    user: {
        craftsmenByService: (serviceId) => `${BASE_URL}/user/craftsmenbyservice/${serviceId}`,
        byUserId: (userId) => `${BASE_URL}/user/${userId}`,
        servicesByUser: (userId) => `${BASE_URL}/user/services/${userId}`,
        registerService: (userId, serviceId) => `${BASE_URL}/user/registerservice/${userId}/${serviceId}`,
        removeService: (userId, serviceId) => `${BASE_URL}/user/removeservice/${userId}/${serviceId}`
    },

    offeredService: {
        all: `${BASE_URL}/offeredservice/all`,
        byServiceId: (serviceId) => `${BASE_URL}/offeredservice/${serviceId}`
    },

    auth: {
        login: `${BASE_URL}/auth/login`,
        registration: `${BASE_URL}/auth/register`
    }
}