import { Form, Formik, FormikHelpers } from "formik";
import { Link } from "react-router-dom";
import TextField from "../forms/TextField";
import Button from "../utils/Button";
import { authorCreationDTO } from "./author.model";
import * as Yup from 'yup';
import DateField from "../forms/DateField";
import ImageField from "../forms/ImageField";
import MarkdownField from "../forms/MarkdownField";

export default function AuthorForm(props: authorFormProps) {
    return (
        <Formik
            initialValues={props.model}
            onSubmit={props.onSubmit}
            validationSchema={Yup.object({
                name: Yup.string().required('This field is required').firstLetterUppercase(),
                dateOfBirth: Yup.date().nullable().required('This field is required')
            })}
        >
            {(formikProps) => (
                <Form>
                    <TextField displayName="Name" field="name" />
                    <DateField displayName="Date of Birth" field="dateOfBirth" />
                    <ImageField displayName="Picture" field="picture" imageURL={props.model.pictureURL} />
                    <MarkdownField displayName="Biography" field="biography"/>
                    <Button disabled={formikProps.isSubmitting}
                        type="submit"
                    >Save Changes</Button>
                    <Link to="/authors" className="btn btn-secondary">Cancel</Link>
                </Form>
            )}
        </Formik>
    )
}

interface authorFormProps {
    model: authorCreationDTO;
    onSubmit(values: authorCreationDTO, action: FormikHelpers<authorCreationDTO>): void;
}