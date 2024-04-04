
const OfferedServiceTable = (offeredServices) => {

    console.log("services:" + Object.keys(offeredServices));
    let items = Object.values(offeredServices)[0];
    console.log(items);
    console.log(items[0]);

    return (
        <div>
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
            </table>
        </div>
    );

}

export default OfferedServiceTable;