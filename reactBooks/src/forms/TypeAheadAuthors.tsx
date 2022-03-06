import { ReactElement, useState } from 'react';
import { Typeahead } from 'react-bootstrap-typeahead'
import { authorBookDTO } from '../authors/author.model'

export default function TypeAheadAuthors(props: typeAheadAuthorsProps) {

    const authors: authorBookDTO[] = [{
        id: 1, name: 'Felipe', character: '', picture: 'https://upload.wikimedia.org/wikipedia/commons/thumb/3/3c/Tom_Holland_by_Gage_Skidmore.jpg/220px-Tom_Holland_by_Gage_Skidmore.jpg'
    },
    {
        id: 2, name: 'Fernando', character: '', picture: 'https://upload.wikimedia.org/wikipedia/commons/thumb/f/f1/Dwayne_Johnson_2%2C_2013.jpg/220px-Dwayne_Johnson_2%2C_2013.jpg'
    },
    {
        id: 3, name: 'Jessica', character: '', picture: 'https://upload.wikimedia.org/wikipedia/commons/thumb/4/46/Meryl_Streep_December_2018.jpg/220px-Meryl_Streep_December_2018.jpg'
    }]

    const selected: authorBookDTO[] = [];

    const [draggedElement, setDraggedElement] = useState<authorBookDTO | undefined>(undefined);

    function handleDragStart(author: authorBookDTO){
        setDraggedElement(author);
    }

    function handleDragOver(author: authorBookDTO){
        if (!draggedElement){
            return;
        }

        if (author.id !== draggedElement.id){
            const draggedElementIndex = props.authors.findIndex(x => x.id === draggedElement.id);
            const authorIndex = props.authors.findIndex(x => x.id === author.id);

            const authors = [...props.authors];
            authors[authorIndex] = draggedElement;
            authors[draggedElementIndex] = author;
            props.onAdd(authors);
        }
    }

    return (
        <div className="mb-3">
            <label>{props.displayName}</label>
            <Typeahead
                id="typeahead"
                onChange={authors => {
                    
                    if (props.authors.findIndex(x => x.id === authors[0].id) === -1){
                        props.onAdd([...props.authors, authors[0]]);
                    }

                    console.log(authors);
                }}
                options={authors}
                labelKey={author => author.name}
                filterBy={['name']}
                placeholder="Write the name of the author..."
                minLength={1}
                flip={true}
                selected={selected}
                renderMenuItemChildren={author => (
                    <>
                        <img alt="author" src={author.picture} 
                            style={{
                                height: '64px',
                                marginRight: '10px',
                                width: '64px'
                            }}
                        />
                        <span>{author.name}</span>
                    </>
                )}
            />

            <ul className="list-group">
                {props.authors.map(author => <li 
                key={author.id}
                    draggable={true}
                    onDragStart={() => handleDragStart(author)}
                    onDragOver={() => handleDragOver(author)}
                    className="list-group-item list-group-item-action"
                >
                    {props.listUI(author)} 
                    <span className="badge badge-primary badge-pill pointer text-dark"
                    style={{marginLeft: '0.5rem'}}
                    onClick={() => props.onRemove(author)}
                    >X</span>
                    </li>)}
            </ul>
        </div>
    )
}

interface typeAheadAuthorsProps {
    displayName: string;
    authors: authorBookDTO[];
    onAdd(authors: authorBookDTO[]): void;
    onRemove(author: authorBookDTO): void;
    listUI(author: authorBookDTO): ReactElement;
}