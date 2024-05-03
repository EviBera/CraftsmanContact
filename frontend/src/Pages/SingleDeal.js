import React from "react";
import { useParams } from "react-router-dom";

const SingleDeal = () => {

    const { id } = useParams();
    console.log(id);

    return (
        <h1>Single Deal page, id: {id}</h1>
    )
}

export default SingleDeal;