import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import InputGroup from "react-bootstrap/InputGroup";
import Row from "react-bootstrap/Row";
import Asteric from "./Asteric";
import { Formik } from "formik";
import * as yup from "yup";

export default function AddGenre({
  addShow,
  setAddShow,
  listOfGenres,
  setListOfGenres,
}) {
  //initial value of data
  const initialValues = {
    genreName: "",
  };

  //form validation done by yup library
  const schema = yup.object().shape({
    genreName: yup.string().required(),
  });

  const handleSubmit = async (genreData, { resetForm }) => {
    console.log(genreData);
    try {
      const response = await fetch("https://localhost:7200/api/genres", {
        method: "POST",
        body: JSON.stringify(genreData),
        headers: {
          "Content-type": "application/json; charset=UTF-8",
        },
      });
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      //  add new book data to setlistOfBooks after adding new book
      setListOfGenres([...listOfGenres, genreData]);

      // Reset the form after submission
      resetForm();
      //setSubmitting(false);
    } catch (error) {
      console.error(
        "There was a problem with the new Author POST request:",
        error
      );
    }
  };

  if (addShow) {
    return (
      <Formik
        initialValues={initialValues}
        onSubmit={handleSubmit}
        validationSchema={schema}
      >
        {({ handleSubmit, handleChange, isSubmitting, values, errors }) => (
          <Form
            noValidate
            onSubmit={handleSubmit}
            className="mt-3 p-2 outline-secondary shadow bg-secondary-subtle"
          >
            <Row className="mb-3 mt-2">
              <Col md="4"></Col>
              <Form.Group as={Col} controlId="validationFormik01" md="4">
                <InputGroup hasValidation>
                  <InputGroup.Text id="inputGroupPrependName">
                    <Asteric />
                  </InputGroup.Text>

                  <Form.FloatingLabel
                    controlId="floatingName"
                    label="Enter Name"
                  >
                    <Form.Control
                      type="text"
                      name="genreName"
                      value={values.genreName}
                      aria-describedby="inputGroupPrependName"
                      onChange={handleChange}
                      isInvalid={!!errors.genreName}
                      required
                      autoComplete="off"
                    />
                    <Form.Control.Feedback type="invalid" tooltip>
                      {errors.genreName}
                    </Form.Control.Feedback>
                  </Form.FloatingLabel>
                </InputGroup>
              </Form.Group>
              <Col md="4"></Col>
            </Row>

            <Row className="mt-3 mb-2">
              <Col md="9"></Col>
              <Col>
                <Button
                  type="submit"
                  variant="success"
                  className="border-dark shadow rounded-pill"
                  disabled={isSubmitting}
                >
                  Add Genre
                </Button>
              </Col>
              <Col>
                <Button
                  variant="dark"
                  onClick={() => setAddShow(false)}
                  className="border-dark shadow rounded-pill"
                >
                  Close
                </Button>
              </Col>
            </Row>
          </Form>
        )}
      </Formik>
    );
  }
}
