import { authorBookDTO } from '../authors/author.model';
import { genreDTO } from '../genres/genres.model';
import { bookShopDTO } from '../bookshops/bookShops.model';
import BookForm from "./BookForm";

export default function EditBook(){

    const nonSelectedGenres: genreDTO[] = [{id: 2, name: 'Drama'}]
    const selectedGenres: genreDTO[] = [{id: 1, name: 'Comedy'}]

    const nonSelectedBookShops: bookShopDTO[] = 
    [{id: 2, name: 'Agora'}]

    const selectedBookShops: bookShopDTO[] = 
    [{id: 1, name: 'D&R'}]

    const selectedAuthors: authorBookDTO[] = [{
        id: 1, name: 'Felipe', character: 'Geralt', picture: 'https://upload.wikimedia.org/wikipedia/commons/thumb/3/3c/Tom_Holland_by_Gage_Skidmore.jpg/220px-Tom_Holland_by_Gage_Skidmore.jpg'
    }]

    return(
        <>
            <h3>Edit Book</h3>
            <BookForm model={{title: 'Angels & Demons', inShops: false, trailer: 'url',
                releaseDate: new Date('2023-02-02T00:00:00')
            }}
                onSubmit={values=>console.log(values)}
                nonSelectedGenres={nonSelectedGenres}
                selectedGenres={selectedGenres}

                nonSelectedBookShops={nonSelectedBookShops}
                selectedBookShops={selectedBookShops}
                selectedAuthors={selectedAuthors}
            />
        </>
    )
}