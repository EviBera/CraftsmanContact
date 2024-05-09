import React, { useEffect } from "react";
import { useParams } from "react-router-dom";
import { useOutletContext } from 'react-router-dom';
import SingleDealCard from "../Components/SingleDealCard";
import { URLS } from "../Config/urls";

const SingleDeal = () => {

    const { id } = useParams();
    const context = useOutletContext();
    const selectedDeal = context.selectedDeal;
    const headers = context.headers;
    const storedLoggedInUser = context.storedLoggedInUser;
    const triggerUpdate = context.triggerUpdate;


    const handleAcceptDeal = () => {
        const request = {
            "craftsmanId": storedLoggedInUser.id,
            "dealId": selectedDeal.dealId
        };
    
        fetch(URLS.deal.accept(storedLoggedInUser.id, selectedDeal.dealId), { 
            method: "PATCH",
            headers,
            body: JSON.stringify(request),
        })
        .then(response => response.text())
        .then(data => {
            console.log("Deal accepted:", data);
            triggerUpdate();
        })
        .catch(error => console.error('Error:', error));

    };

    const handleCloseDeal = () => {
        const request = {
            "dealId": selectedDeal.dealId,
            "userId": storedLoggedInUser.id
        }

        fetch(URLS.deal.close(selectedDeal.dealId, storedLoggedInUser.id), {
            method: "PATCH",
            headers,
            body: JSON.stringify(request),
        })
        .then(response => response.text())
        .then(data => {
            console.log("Deal closed:", data);
            triggerUpdate();
        })
        .catch(error => console.error('Error:', error));
    };


    return (
        <SingleDealCard props={{ id, selectedDeal, handleAcceptDeal, handleCloseDeal }} />
    )
}

export default SingleDeal;