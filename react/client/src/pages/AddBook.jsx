import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import InputGroup from "react-bootstrap/InputGroup";
import Row from "react-bootstrap/Row";
import Asteric from "./Asteric";
import { Formik } from "formik";
import * as yup from "yup";
import { useEffect, useState } from "react";
import Spinner from "./Spinner";
import { getAPIToken } from "../config";

export default function AddBook({
  addShow,
  setAddShow,
  listOfBooks,
  setListOfBooks,
}) {
  //const { Formik } = formik;
  const [loading, setLoading] = useState(true);

  const [mappedAuthors, setMappedAuthors] = useState([]);

  const [mappedGenres, setMappedGenres] = useState([]);

  //initial value of data
  const initialValues = {
    title: "",
    authorId: "",
    genreId: "",
    price: "",
    publicationDate: "",
  };

  //form validation done by yup library
  const schema = yup.object().shape({
    title: yup.string().required(),
    authorId: yup.string().required("Please select an author"),
    genreId: yup.string().required("Please select a genre"),
    price: yup.number().required(),
    publicationDate: yup.date().required(),
  });

  useEffect(() => {
    const fetchAuthorsData = async () => {
      try {
        const response = await fetch("https://localhost:7200/api/authors");
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const authorsData = await response.json();
        const mappedAuthorslist = authorsData.map((author) => ({
          id: author.authorId,
          name: author.name,
        }));
        setMappedAuthors(mappedAuthorslist);
        setLoading(false);
        console.log("mappedAuthors::", mappedAuthors);
      } catch (error) {
        console.error("Error fetching authors data:", error);
      }
    };
    fetchAuthorsData();
  }, []);

  useEffect(() => {
    const fetchGenresData = async () => {
      try {
        //const response = await fetch("https://localhost:7200/api/genres");
        const token = getAPIToken(); // Retrieve the token
        const response = await fetch('https://localhost:7200/api/genres', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`, // Include the token in the Authorization header
          },
        });
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const genresData = await response.json();
        console.log("genresData", genresData);
        const mappedGenresList = genresData.map((genre) => ({
          id: genre.genreId,
          name: genre.genreName,
        }));
        setMappedGenres(mappedGenresList);
        setLoading(false);
        console.log("mappedGenres::", mappedGenres);
      } catch (error) {
        console.error("Error fetching genres data:", error);
      }
    };
    fetchGenresData();
  }, []);

  const handleSubmit = async (bookData, { resetForm }) => {
    console.log(bookData);
    try {
      const response = await fetch("https://localhost:7200/api/books", {
        method: "POST",
        body: JSON.stringify(bookData),
        headers: {
          "Content-type": "application/json; charset=UTF-8",
        },
      });
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }

      //  after adding new book, add new book data to setlistOfBooks
      setListOfBooks([...listOfBooks, bookData]);

      // After submission, reset the form
      resetForm();
    } catch (error) {
      console.error(
        "There was a problem with the new book POST request:",
        error
      );
    }
  };

  if (loading) {
    return <Spinner />;
  }

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
            className="mt-3 p-2 shadow bg-secondary-subtle"
          >
            <Row className="m-2">
              <Form.Group as={Col} controlId="validationFormik01" md="6">
                <InputGroup hasValidation>
                  <InputGroup.Text id="inputGroupPrependTitle">
                    <Asteric />
                  </InputGroup.Text>

                  <Form.FloatingLabel
                    controlId="floatingTitle"
                    label="Enter Title"
                  >
                    <Form.Control
                      type="text"
                      name="title"
                      value={values.title}
                      aria-describedby="inputGroupPrependTitle"
                      onChange={handleChange}
                      isInvalid={!!errors.title}
                      required
                      autoComplete="off"
                    />
                    <Form.Control.Feedback type="invalid" tooltip>
                      {errors.title}
                    </Form.Control.Feedback>
                  </Form.FloatingLabel>
                </InputGroup>
              </Form.Group>

              <Form.Group as={Col} controlId="validationFormik02">
                <Form.FloatingLabel
                  controlId="floatingAuthor"
                  label="Author"
                  className="mb-3"
                >
                  <Form.Select
                    name="authorId"
                    value={values.authorId}
                    onChange={handleChange}
                    isInvalid={!!errors.authorId}
                  >
                    <option value="">Select an Author</option>
                    {mappedAuthors?.map((author) => {
                      return (
                        <option key={author.id} value={author.id}>
                          {author.name}
                        </option>
                      );
                    })}
                  </Form.Select>
                  <Form.Control.Feedback type="invalid" tooltip>
                    {errors.authorId}
                  </Form.Control.Feedback>
                </Form.FloatingLabel>
              </Form.Group>

              <Form.Group as={Col} controlId="validationFormik03">
                <Form.FloatingLabel
                  controlId="floatingGenre"
                  label="Genre"
                  className="mb-3"
                >
                  <Form.Select
                    name="genreId"
                    value={values.genreId}
                    onChange={handleChange}
                    isInvalid={!!errors.genreId}
                  >
                    <option value="">Select a Genre</option>
                    {mappedGenres?.map((genre) => {
                      return (
                        <option key={genre.id} value={genre.id}>
                          {genre.name}
                        </option>
                      );
                    })}
                  </Form.Select>
                  <Form.Control.Feedback type="invalid" tooltip>
                    {errors.genreId}
                  </Form.Control.Feedback>
                </Form.FloatingLabel>
              </Form.Group>
            </Row>

            <Row className="m-2">
              <Form.Group as={Col} controlId="validationFormik04" md="6">
                <InputGroup hasValidation>
                  <InputGroup.Text id="inputGroupPrependPrice">
                    <Asteric />
                  </InputGroup.Text>

                  <Form.FloatingLabel
                    controlId="floatingPrice"
                    label="Enter Price"
                  >
                    <Form.Control
                      type="text"
                      name="price"
                      value={values.price}
                      aria-describedby="inputGroupPrependPrice"
                      onChange={handleChange}
                      isInvalid={!!errors.price}
                      required
                      autoComplete="off"
                    />
                    <Form.Control.Feedback type="invalid" tooltip>
                      {errors.price}
                    </Form.Control.Feedback>
                  </Form.FloatingLabel>
                </InputGroup>
              </Form.Group>

              <Form.Group as={Col} controlId="validationFormik05" md="6">
                <InputGroup hasValidation>
                  <InputGroup.Text id="inputGroupPrependPublished">
                    <Asteric />
                  </InputGroup.Text>

                  <Form.FloatingLabel
                    controlId="floatingPublished"
                    label="Enter Published On Date"
                  >
                    <Form.Control
                      type="date"
                      name="publicationDate"
                      value={values.publicationDate}
                      aria-describedby="inputGroupPrepend"
                      onChange={handleChange}
                      isInvalid={!!errors.publicationDate}
                      required
                      max={new Date()?.toISOString()?.slice(0, 10)}
                      autoComplete="off"
                    />
                    <Form.Control.Feedback type="invalid" tooltip>
                      {errors.publicationDate}
                    </Form.Control.Feedback>
                  </Form.FloatingLabel>
                </InputGroup>
              </Form.Group>
            </Row>
            <Row className="mt-3 mb-2">
              <Col md="9"></Col>
              <Col>
                <Button
                  type="submit"
                  variant="success"
                  disabled={isSubmitting}
                  className="border border-dark shadow rounded-pill mb-2"
                >
                  Add Book
                </Button>
              </Col>
              <Col>
                <Button
                  onClick={() => setAddShow(false)}
                  variant="dark"
                  className="border border-dark shadow rounded-pill"
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
