import AuthorForm from "./AuthorForm";

export default function CreateAuthor(){
    return(
        <>
            <h3>Create Author</h3>
            <AuthorForm model={{name: '', dateOfBirth: undefined}}
                onSubmit={values => console.log(values)}
            />
        </>
    )
}