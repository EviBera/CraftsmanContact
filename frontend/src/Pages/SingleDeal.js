import React from "react";
import { useParams } from "react-router-dom";
import { useOutletContext } from 'react-router-dom';
import SingleDealCard from "../Components/SingleDealCard";

const SingleDeal = () => {

    const { id } = useParams();
    const selectedDeal = useOutletContext();

    return (
        <SingleDealCard props = {{id, selectedDeal}}/>
    )
}

export default SingleDeal;