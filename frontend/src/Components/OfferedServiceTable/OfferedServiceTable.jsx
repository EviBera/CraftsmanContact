import "./OfferedServiceTable.css";
import { useContext } from "react";
import OfferedServiceContext from '../../Pages/OfferedServiceContext';

const OfferedServiceTable = (offeredServices) => {

    let items = Object.values(offeredServices)[0];

    const { setSelectedService } = useContext(OfferedServiceContext);

    const handleClick = (item) => {
        setSelectedService(item);
        localStorage.setItem('selectedServiceId', item.offeredServiceId);
    }

    return (
        <div>

            <div className="dropdown">
                <button className="dropbtn">What kind of service do you need?</button>
                <div className="dropdown-content">
                    {items.map((offeredService) =>
                    (
                        <div key={offeredService.offeredServiceId}>
                            <button className="dropdown-item" onClick={() => handleClick(offeredService)}>{offeredService.offeredServiceName}
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