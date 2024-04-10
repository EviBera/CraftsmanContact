import { useContext } from "react";
import IDContext from '../../Pages/IDContext';

const CraftsmenByServiceTable = (craftsmen) => {

    let masters = Object.values(craftsmen)[0];
    let first = masters[0];

    return (
        <div>
            <table>
                <thead>
                    <tr>
                        <th>
                            Service : { }
                        </th>
                    </tr>
                    <tr>
                        <th>Name</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                    {first ? (
                        masters.map((master) => (
                            <tr key={master.id}>
                                <td>{master.firstName} {master.lastName}</td>
                                <td>{master.email}</td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="2">Sorry, we cannot offer a craftsman.</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    )
}

export default CraftsmenByServiceTable;