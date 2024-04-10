import "./OfferedServiceTable.css";
import { useState, useContext } from "react";
import IDContext from '../../Pages/IDContext';

const OfferedServiceTable = (offeredServices) => {

    let items = Object.values(offeredServices)[0];

    const { setSelectedId } = useContext(IDContext);

    return (
        <div>

            <div className="dropdown">
                <button className="dropbtn">What kind of service do you need?</button>
                <div className="dropdown-content">
                    {items.map((offeredService) =>
                    (
                        <div key={offeredService.offeredServiceId}>
                            <button className="dropdown-item" onClick={() => setSelectedId(offeredService.offeredServiceId)}>{offeredService.offeredServiceName}
                                <span className="description">{offeredService.offeredServiceDescription}</span>
                            </button>
                        </div>
                    ))}
                </div>
                <hr />
            </div>

            {/* 
            
            <div class="dropdown-description">
                                <p>{offeredService.offeredServiceDescription}</p>
                            </div>
            
            
            <table>
                <thead>
                    <tr>
                        <th>Offered service</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {items.map((offeredService) => (
                        <tr key={offeredService.offeredServiceId}>
                            <td>{offeredService.offeredServiceName}</td>
                            <td>{offeredService.offeredServiceDescription}</td>
                        </tr>
                    ))}
                </tbody>
            </table> */}
        </div>
    );

}

export default OfferedServiceTable;