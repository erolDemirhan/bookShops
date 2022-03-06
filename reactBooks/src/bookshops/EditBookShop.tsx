import BookShopForm from "./BookShopForm";

export default function EditBookShop(){
    return(
        <>
            <h3>Edit Book Shops</h3>
            <BookShopForm
                model={{name: 'D&R', 
                latitude:46.345421 , 
                longitude: -54.533243}}
                onSubmit={values => console.log(values)}
            />
        </>
    )
}