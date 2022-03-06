import { Field, Form, Formik } from "formik";
import { genreDTO } from "../genres/genres.model";
import Button from "../utils/Button";

export default function FilterBooks(){

    const initialValues: filterMoviesForm = {
        title: '',
        genreId: 0,
        upcomingBooks: false,
        inShops: false
    }

    const genres: genreDTO[] = [{id: 1, name: 'Science-Fiction'}, {id: 2, name: 'Comedy'}];

    return(
        <>
        <h3>Filter Books</h3>
        <Formik initialValues={initialValues}
            onSubmit={values => console.log(values)}
        >
            {(formikProps) => (
                <Form>
                    <div className="row gx-3 align-items-center">
                        <div className="col-auto">
                            <input type="text" className="form-control" id="title" placeholder="Title of the book" {...formikProps.getFieldProps("title")}/>
                        </div>
                        <div className="col-auto">
                            <select className="form-select"
                                {...formikProps.getFieldProps("genreId")}
                            >
                                <option value="0">---Choose a genre---</option>
                                {genres.map(genre => <option key={genre.id} value={genre.id}>{genre.name}</option>)}
                            </select>
                        </div>
                        <div className="col-auto">
                            <div className="form-check">
                                <Field className="form-check-input" id="upcomingBooks" name="upcomingBooks" type="checkbox"/>
                                <label className="form-check-label" htmlFor="upcomingBooks">Upcoming Books</label>
                            </div>
                        </div>
                        <div className="col-auto">
                            <div className="form-check">
                                <Field className="form-check-input" id="inShops" name="inShops" type="checkbox"/>
                                <label className="form-check-label" htmlFor="inShops">In Shops</label>
                            </div>
                        </div>
                        <div className="col-auto">
                            <Button className="btn btn-primary"
                            onClick={()=>formikProps.submitForm()}
                            >Filter</Button>
                            <Button className="btn btn-danger ms-3"
                            onClick={()=>formikProps.setValues(initialValues)}
                            >Clear</Button>                            
                        </div>                        
                    </div>
                </Form>
            )
            }
        </Formik>
        </>
    )
}

interface filterMoviesForm{
    title: string;
    genreId: number;
    upcomingBooks: boolean;
    inShops: boolean;
}