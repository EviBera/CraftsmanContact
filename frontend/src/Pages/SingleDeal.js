import React from "react";
import { useParams } from "react-router-dom";
import { useOutletContext } from 'react-router-dom';

const SingleDeal = () => {

    const { id } = useParams();
    const selectedDeal = useOutletContext();

    return (
        <>
            <h1>Single Deal page, id: {id}</h1>
            {selectedDeal && <div>
                <p>deal id: {selectedDeal.dealId}</p>
                <p>craftsman: {selectedDeal.craftsmanId}</p>
                <p>client: {selectedDeal.clientId}</p>
                <p>service id: {selectedDeal.offeredServiceId}</p>
                <p>date: {selectedDeal.createdAt}</p>
                <p>accepted? {selectedDeal.isAcceptedByCraftsman ? 'yes' : 'no'}</p>
                <p>CM closed? {selectedDeal.isClosedByCraftsman ? 'yes' : 'no'}</p>
                <p>client closed? {selectedDeal.isClosedByClient ? 'yes' : 'no'}</p>
            </div>
            }

        </>
    )
}

export default SingleDeal;