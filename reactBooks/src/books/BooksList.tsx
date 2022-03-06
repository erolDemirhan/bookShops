import { bookDTO } from "./books.model"
import IndividualBook from "./IndividualBook";
import css from './BooksList.module.css';
import Loading from "./../utils/Loading";
import GenericList from "./../utils/GenericList";

export default function BooksList(props: booksListProps){
    return <GenericList list={props.books}>
        <div className={css.div}>
            {props.books?.map(book => <IndividualBook {...book} key={book.id}/>)}
        </div>
    </GenericList>
}

interface booksListProps{
    books?: bookDTO[];
}