import { Form, Formik, FormikHelpers } from "formik";
import { bookCreationDTO } from './books.model';
import * as Yup from 'yup'
import Button from '../utils/Button';
import { Link } from 'react-router-dom';
import TextField from '../forms/TextField';
import ImageField from '../forms/ImageField';
import DateField from '../forms/DateField';
import CheckboxField from '../forms/CheckboxField';
import MultipleSelector, { multipleSelectorModel } from '../forms/MultipleSelector';
import { useState } from "react";
import { genreDTO } from "../genres/genres.model";
import { bookShopDTO } from "../bookshops/bookShops.model";
import TypeAheadAuthor from '../forms/TypeAheadAuthors';
import { authorBookDTO } from "../authors/author.model";

export default function BookForm(props: bookFormProps) {

    const [selectedGenres, setSelectedGenres] = useState(mapToModel(props.selectedGenres));
    const [nonSelectedGenres, setNonSelectedGenres] =
        useState(mapToModel(props.nonSelectedGenres));

    const [selectedBookShops, setSelectedBookShops] =
        useState(mapToModel(props.selectedBookShops));
    const [nonSelectedBookShops, setNonSelectedBookShops] =
        useState(mapToModel(props.nonSelectedBookShops));

    const [selectedAuthors, setSelectedAuthors] = useState(props.selectedAuthors);

    function mapToModel(items: { id: number, name: string }[]): multipleSelectorModel[] {
        return items.map(item => {
            return { key: item.id, value: item.name }
        })
    }

    return (
        <Formik
            initialValues={props.model}
            onSubmit={(values, actions) => {
                values.genresIds = selectedGenres.map(item => item.key);
                values.bookShopsIds = selectedBookShops.map(item => item.key);
                values.authors = selectedAuthors;
                props.onSubmit(values, actions)
            }}
            validationSchema={Yup.object({
                title: Yup.string().required('This field is required').firstLetterUppercase()
            })}
        >
            {(formikProps) => (
                <Form>

                    <TextField displayName="Title" field="title" />
                    <CheckboxField displayName="In Shops" field="inShops" />
                    <TextField displayName="Trailer" field="trailer" />
                    <DateField displayName="Release Date" field="releaseDate" />
                    <ImageField displayName="Poster" field="poster"
                        imageURL={props.model.posterURL}
                    />

                    <MultipleSelector
                        displayName="Genres"
                        nonSelected={nonSelectedGenres}
                        selected={selectedGenres}
                        onChange={(selected, nonSelected) => {
                            setSelectedGenres(selected);
                            setNonSelectedGenres(nonSelected);
                        }}
                    />

                    <MultipleSelector
                        displayName="Book Shops"
                        nonSelected={nonSelectedBookShops}
                        selected={selectedBookShops}
                        onChange={(selected, nonSelected) => {
                            setSelectedBookShops(selected);
                            setNonSelectedBookShops(nonSelected);
                        }}
                    />

                    <TypeAheadAuthor displayName="Authors" authors={selectedAuthors} 
                     onAdd={authors => {
                        setSelectedAuthors(authors);
                     }}
                     onRemove={author => {
                         const authors = selectedAuthors.filter(x => x !== author);
                         setSelectedAuthors(authors);
                     }}
                     listUI={(author: authorBookDTO) => 
                     <>
                        {author.name} / <input placeholder="Character" type="text"
                            value={author.character}
                            onChange={e => {
                                const index = selectedAuthors.findIndex(x => x.id === author.id);

                                const authors = [...selectedAuthors];
                                authors[index].character = e.currentTarget.value;
                                setSelectedAuthors(authors);
                            }} />
                     </>
                    }
                    />

                    <Button disabled={formikProps.isSubmitting}
                        type='submit'>Save Changes</Button>
                    <Link className="btn btn-secondary" to="/genres">Cancel</Link>
                </Form>
            )}
        </Formik>
    )
}

interface bookFormProps {
    model: bookCreationDTO;
    onSubmit(values: bookCreationDTO, actions: FormikHelpers<bookCreationDTO>): void;
    selectedGenres: genreDTO[];
    nonSelectedGenres: genreDTO[];
    selectedBookShops: bookShopDTO[];
    nonSelectedBookShops: bookShopDTO[];
    selectedAuthors: authorBookDTO[];
}