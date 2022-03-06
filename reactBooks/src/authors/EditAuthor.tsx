import AuthorForm from "./AuthorForm";

export default function EditAuthor(){
    return(
        <>
            <h3>Edit Author</h3>
            <AuthorForm model={{name: 'Dan Brown', 
            dateOfBirth: new Date('1945-07-05T00:00:00'), 
            biography: `# kara 
            uzum habbesi le le le le canim`,
            pictureURL: 'https://upload.wikimedia.org/wikipedia/commons/8/8b/Dan_Brown_bookjacket_cropped.jpg'
            }}
                onSubmit={values => console.log(values)}
            />            
        </>
    )
}