import React from "react";
import { useParams } from "react-router-dom";
import { useOutletContext } from 'react-router-dom';
import SingleDealCard from "../Components/SingleDealCard";

const SingleDeal = () => {

    const { id } = useParams();
    const context = useOutletContext();
    const selectedDeal = context.selectedDeal;

    return (
        <SingleDealCard props = {{id, selectedDeal}}/>
    )
}

export default SingleDeal;