import ClimbingBoxLoader from "react-spinners/ClimbingBoxLoader";
import "./Loading.css";

function Loading(){
    return (
        <div className="spinner">
            <ClimbingBoxLoader
                color = {"#f8bc63"}
            />
        </div>
    )
}

export default Loading;