import "./OfferedServiceTable.css";

const OfferedServiceTable = (offeredServices) => {

    let items = Object.values(offeredServices)[0];

    return (
        <div>

            <div class="dropdown">
                <button class="dropbtn">What kind of service do you need?</button>
                <div class="dropdown-content">
                    {items.map((offeredService) =>
                    (
                        <div key={offeredService.offeredServiceId}>
                            <a href={`/craftsmen/:${offeredService.offeredServiceId}`}>{offeredService.offeredServiceName}
                                <span className="description">{offeredService.offeredServiceDescription}</span>
                            </a>
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