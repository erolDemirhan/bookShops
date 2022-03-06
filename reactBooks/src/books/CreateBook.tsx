import { bookShopDTO } from "../bookshops/bookShops.model";
import { genreDTO } from "../genres/genres.model";
import BookForm from "./BookForm";

export default function CreateBook(){

    const nonSelectedGenres: genreDTO[] = [{id: 1, name: 'Comedy'}, {id: 2, name: 'Drama'}]
    const nonSelectedBookShops: bookShopDTO[] = 
        [{id: 1, name: 'D&R'}, {id: 2, name: 'Agora'}]

    return(
        <>
            <h3>Create Book</h3>
            <BookForm model={{title: '', inShops: false, trailer: ''}}
                onSubmit={values=>console.log(values)}
                nonSelectedGenres={nonSelectedGenres}
                selectedGenres={[]}

                nonSelectedBookShops={nonSelectedBookShops}
                selectedBookShops={[]}
                selectedAuthors={[]}                
            />
        </>
    )
}