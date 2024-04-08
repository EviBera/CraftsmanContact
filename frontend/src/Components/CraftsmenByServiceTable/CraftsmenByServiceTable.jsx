const CraftsmenByServiceTable = (craftsmen) => {

    let masters = Object.values(craftsmen);

    return (
        <div>
            <table>
                <thead>
                    <tr>
                        <th>
                            Service : {}
                        </th>
                    </tr>
                    <tr>
                        <th>Name</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                    {masters.map((master) => (
                        <tr key={master.id}>
                            <td>{master.firstname} {master.lastName}</td>
                            <td>{master.email}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default CraftsmenByServiceTable;