
const OfferedServiceTable = (offeredServices) => {

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
                    {offeredServices.map((offeredService) => (
                        <tr key={offeredService.OfferedServiceId}>
                            <td>{offeredService.OfferedServiceName}</td>
                            <td>{offeredService.OfferedServiceDescription}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );

}

export default OfferedServiceTable;