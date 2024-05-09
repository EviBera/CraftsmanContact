const BASE_URL = "http://localhost:5213/api";

export const URLS = {

    deal: {
        addDeal: `${BASE_URL}/deal`,
        byUser: (userId) => `${BASE_URL}/deal/byuser/${userId}`,
        accept: (userId, dealId) => `${BASE_URL}/deal/accept/${userId}/${dealId}`
    },

    user: {
        craftsmenByService: (serviceId) => `${BASE_URL}/user/craftsmenbyservice/${serviceId}`,
        byUserId: (userId) => `${BASE_URL}/user/${userId}`
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