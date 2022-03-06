import { NavLink } from "react-router-dom";

export default function Menu(){
    return(
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="container-fluid">
                <NavLink className="navbar-brand" to="/">React Books</NavLink>
                <div className="collapse navbar-collapse">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/genres">
                                Genres
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/books/filter">
                                Filter Books
                            </NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/authors">
                                Authors
                            </NavLink>
                        </li>                                                
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/bookshops">
                                Book Shops
                            </NavLink>
                        </li>                        
                        <li className="nav-item">
                            <NavLink className="nav-link" to="/books/create">
                                Create a Book
                            </NavLink>
                        </li>                        
                    </ul>
                </div>
            </div>
        </nav>
    )
}