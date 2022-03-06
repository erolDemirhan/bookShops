import CreateAuthor from "./authors/CreateAuthor";
import EditAuthor from "./authors/EditAuthor";
import IndexAuthors from "./authors/IndexAuthors";
import CreateBook from "./books/CreateBook";
import EditBook from "./books/EditBook";
import FilterBooks from "./books/FilterBooks";
import LandingPage from "./books/LandingPage";
import CreateBookShop from "./bookshops/CreateBookShop";
import EditBookShop from "./bookshops/EditBookShop";
import IndexBookShops from "./bookshops/IndexBookShops";
import CreateGenre from "./genres/CreateGenre";
import EditGenre from "./genres/EditGenre";
import IndexGenres from "./genres/IndexGenres";
import RedirectToLandingPage from "./utils/RedirectToLandingPage";

const routes = [
    {path: '/genres', component: IndexGenres, exact: true},
    {path: '/genres/create', component: CreateGenre},
    {path: '/genres/edit/:id(\\d+)', component: EditGenre},

    {path: '/authors', component: IndexAuthors, exact: true},
    {path: '/authors/create', component: CreateAuthor},
    {path: '/authors/edit/:id(\\d+)', component: EditAuthor},

    {path: '/bookshops', component: IndexBookShops, exact: true},
    {path: '/bookshops/create', component: CreateBookShop},
    {path: '/bookshops/edit/:id(\\d+)', component: EditBookShop},

    {path: '/books/create', component: CreateBook, exact: true},
    {path: '/books/edit/:id(\\d+)', component: EditBook},
    {path: '/books/filter', component: FilterBooks},

    {path: '/', component: LandingPage, exact: true},
    {path: '*', component: RedirectToLandingPage}
];

export default routes;