import "./OfferedServiceTable.css";
import { useContext } from "react";
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
        </div>
    );

}

export default OfferedServiceTable;