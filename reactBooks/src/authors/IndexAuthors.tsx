import { Link } from "react-router-dom";

export default function IndexAuthors(){
    return(
        <>
            <h3>Authors</h3>
            <Link className="btn btn-primary" to="/authors/create">Create Author</Link>
        </>
    )
}