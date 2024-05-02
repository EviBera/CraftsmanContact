import "./DealTable.css";


const DealTable = (props) => {

    const deals = Array.isArray(props.props.deals) ? props.props.deals : [];
    const first = deals[0];
    const serviceNames = props.props.serviceNames;
    const craftsmenNames = props.props.craftsmenNames;


    const DateConverter = (input) => {
        let date = new Date(input);
        let year = date.getFullYear();
        let month = date.getMonth() + 1;
        let day = date.getDate();

        return day + "/" + month + "/" + year;
    }


    return (
        <table>
            <thead>
                <tr >
                    <th colSpan="6" className='title'>
                        My deals
                    </th>
                </tr>

                <tr>
                    <th>No.</th>
                    {/* <th>Deal Id</th> */}
                    <th>Craftsman</th>
                    <th>Service</th>
                    <th>Date of my request (d/m/yyyy)</th>
                    <th>Is it accepted by the craftsman?</th>
                </tr>
            </thead>
            <tbody>
                {deals && deals.map((deal) => (
                    <tr key={deal.dealId}>
                        <td>{deals.indexOf(deal) + 1}</td>
                        {/* <td>{deal.dealId}</td> */}
                        <td>{craftsmenNames[deal.craftsmanId]}</td>
                        <td>{serviceNames[deal.offeredServiceId]}</td>
                        <td>{DateConverter(deal.createdAt)}</td>
                        <td>{deal.isAcceptedByCraftsman ? 'Yessss!' : 'Not yet.'}</td>
                    </tr>
                ))}
                {!first &&
                    <tr>
                        <td colSpan="6" className="information">
                            You don't have a deal yet.
                        </td>
                    </tr>}
            </tbody>
        </table>
    )
}

export default DealTable;