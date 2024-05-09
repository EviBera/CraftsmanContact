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
        })
        .catch(error => console.error('Error:', error));
    };


    return (
        <SingleDealCard props={{ id, selectedDeal, handleAcceptDeal}} />
    )
}

export default SingleDeal;