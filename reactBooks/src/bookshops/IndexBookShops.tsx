import { Link } from "react-router-dom";

export default function IndexBookShops(){
    return(
        <>
            <h3>Book Shops</h3>
            <Link className="btn btn-primary" to="/bookshops/create">Create book shops</Link>
        </>
    )
}