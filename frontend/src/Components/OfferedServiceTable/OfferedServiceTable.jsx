import "./OfferedServiceTable.css";
import { useContext } from "react";
import OfferedServiceContext from '../../Pages/OfferedServiceContext';

const OfferedServiceTable = (offeredServices) => {

    let items = Object.values(offeredServices)[0];

    const { setSelectedService } = useContext(OfferedServiceContext);

    return (
        <div>

            <div className="dropdown">
                <button className="dropbtn">What kind of service do you need?</button>
                <div className="dropdown-content">
                    {items.map((offeredService) =>
                    (
                        <div key={offeredService.offeredServiceId}>
                            <button className="dropdown-item" onClick={() => setSelectedService(offeredService)}>{offeredService.offeredServiceName}
                                <span className="description">{offeredService.offeredServiceDescription}</span>
                            </button>
                        </div>
                    ))}
                </div>
                <hr />
            </div>
        </div>
    );

}

export default OfferedServiceTable;