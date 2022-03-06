import { bookDTO } from "./books.model";
import css from './IndividualBook.module.css';

export default function IndividualBook(props: bookDTO){

    const buildLink = () => `/book/${props.id}`    

    return(
        <div className={css.div}>
            <a href={buildLink()}>
                <img alt="Poster" src={props.poster}/>
            </a>
            <p>
                <a href={buildLink()}>{props.title}</a>
            </p>
        </div>
    )
}