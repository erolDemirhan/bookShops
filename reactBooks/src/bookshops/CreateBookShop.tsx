import BookShopForm from "./BookShopForm";

export default function CreateBookShop(){
    return(
        <>
            <h3>Create Book Shop</h3>
            <BookShopForm
                model={{name: ''}}
                onSubmit={values => console.log(values)}
            />
        </>
    )
}