import React from "react";
import { useEffect, useState } from "react";
import { Form, Row, Col } from "react-bootstrap";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Spinner from "./Spinner";
import { getAPIToken } from "../config";

export default function EditBook(props) {
  const [loading, setLoading] = useState(true);

  const [mappedAuthors, setMappedAuthors] = useState([]);

  const [mappedGenres, setMappedGenres] = useState([]);

  const [updatedBookData, setUpdatedBookData] = useState({});
  const [errors, setErrors] = useState({});

  useEffect(() => {
    setUpdatedBookData(props.selectedbook);
  }, [props.selectedbook]);

  console.log("updatedBookData", updatedBookData);

  /** Fetching authors list for populating authors dropdown */

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

  /** Fetching genres list for populating genres dropdown */

  useEffect(() => {
    const fetchGenresData = async () => {
      try {
        //const response = await fetch("https://localhost:7200/api/genres");

        const token = getAPIToken(); // Retrieve the token
        console.log("token::",token);
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

  /** Function to validate updated Book */

  const validateUpdatedBook = (data) => {
    const errors = {};
    // regular expression for a number with up to two decimal places
    const numberWithTwoDecimalsRegex = /^\d+(\.\d{1,2})?$/;

    if (!data.title.trim()) {
      errors.title = "Book name is required";
    }
    if (!data.price) {
      errors.price = "Price is required";
    }
    if (!numberWithTwoDecimalsRegex.test(data.price)) {
      errors.price = "Price should be a number with up to two decimal places";
    }
    if (!data.publicationDate.trim()) {
      errors.publicationDate = "publication date is required";
    }

    return errors;
  };

  /** Function to Edit Book Details */

  const handleSave = async (e) => {
    //alert("inside handleSubmit");
    console.log("Submitted values:::");
    e.preventDefault();

    const newErrors = validateUpdatedBook(updatedBookData);
    setErrors(newErrors);

    console.log("Submitted values:", JSON.stringify(updatedBookData));

    if (Object.keys(newErrors).length === 0) {
      try {
        const response = await fetch(
          `https://localhost:7200/api/books/${props.selectedbook.bookId}`,
          {
            method: "PUT",
            headers: {
              "Content-type": "application/json; charset=UTF-8",
            },
            body: JSON.stringify(updatedBookData),
          }
        );
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        //update state
        props.updatebook(updatedBookData);

        console.log("Form submitted successfully!");

        props.onHide(); // Close the modal after submission
      } catch (error) {
        console.error(
          "There was a problem with the Book edit (PUT) request:",
          error
        );
      }
    } else {
      console.log(`Form submission failed
       due to validation errors.`);
    }
  };

  if (loading) {
    return <Spinner />;
  }

  return (
    <Modal
      {...props}
      size="md"
      aria-labelledby="contained-modal-title-vcenter"
      backdrop="static"
      keyboard={false}
      centered
    >
      <Modal.Header closeButton className="bg-primary bg-opacity-75">
        <Modal.Title id="contained-modal-title-vcenter">
          Edit Book Details
        </Modal.Title>
      </Modal.Header>
      <Modal.Body className="bg-primary bg-opacity-25">
        <Form
          noValidate
          onSubmit={handleSave}
          className="m-1 p-1 bg-primary bg-opacity-10"
        >
          <Row className="m-2">
            <Form.Group as={Col} controlId="validationFormik01">
              <Form.FloatingLabel controlId="floatingTitle" label="Book Title">
                <Form.Control
                  type="text"
                  name="title"
                  aria-describedby="inputGroupPrependTitle"
                  // value={formikProps.values.title}
                  // onChange={formikProps.handleChange}
                  // isInvalid={
                  //   formikProps.touched.title && !!formikProps.errors.title
                  // }
                  value={updatedBookData.title}
                  onChange={(e) =>
                    setUpdatedBookData({
                      ...updatedBookData,
                      title: e.target.value,
                    })
                  }
                  isInvalid={!!errors.title}
                  required
                  autoComplete="off"
                  //className="shadow"
                />
                <Form.Control.Feedback type="invalid" tooltip>
                  {/* {formikProps.errors.title} */}
                  {errors.title}
                </Form.Control.Feedback>
              </Form.FloatingLabel>
            </Form.Group>
          </Row>
          <Row className="m-2">
            <Form.Group as={Col} controlId="validationFormik02">
              <Form.FloatingLabel controlId="floatingAuthor" label="Author">
                <Form.Select
                  name="authorId"
                  value={updatedBookData.authorId}
                  onChange={(e) =>
                    setUpdatedBookData({
                      ...updatedBookData,
                      authorId: e.target.value,
                    })
                  }
                  isInvalid={!!errors.authorId}
                  // className="shadow"
                >
                  {mappedAuthors?.map((author) => {
                    return (
                      <option
                        key={author.id}
                        value={author.id}
                        // className="bg-primary bg-opacity-50"
                      >
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
              <Form.FloatingLabel controlId="floatingGenre" label="Genre">
                <Form.Select
                  name="genreId"
                  value={updatedBookData.genreId}
                  onChange={(e) =>
                    setUpdatedBookData({
                      ...updatedBookData,
                      genreId: e.target.value,
                    })
                  }
                  isInvalid={!!errors.genreId}
                  //className="shadow"
                >
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
              <Form.FloatingLabel controlId="floatingPrice" label="Price">
                <Form.Control
                  type="text"
                  name="price"
                  aria-describedby="inputGroupPrependPrice"
                  value={updatedBookData.price}
                  onChange={(e) =>
                    setUpdatedBookData({
                      ...updatedBookData,
                      price: e.target.value,
                    })
                  }
                  isInvalid={!!errors.price}
                  required
                  autoComplete="off"
                  //className="shadow"
                />
                <Form.Control.Feedback type="invalid" tooltip>
                  {errors.price}
                </Form.Control.Feedback>
              </Form.FloatingLabel>
            </Form.Group>

            <Form.Group as={Col} controlId="validationFormik05" md="6">
              <Form.FloatingLabel
                controlId="floatingPublished"
                label="Enter Published On Date"
              >
                <Form.Control
                  type="date"
                  name="publicationDate"
                  aria-describedby="inputGroupPrepend"
                  value={updatedBookData.publicationDate}
                  onChange={(e) =>
                    setUpdatedBookData({
                      ...updatedBookData,
                      publicationDate: e.target.value,
                    })
                  }
                  isInvalid={!!errors.publicationDate}
                  required
                  max={new Date()?.toISOString()?.slice(0, 10)}
                  autoComplete="off"
                  //className="shadow"
                />
                <Form.Control.Feedback type="invalid" tooltip>
                  {errors.publicationDate}
                </Form.Control.Feedback>
              </Form.FloatingLabel>
            </Form.Group>
          </Row>
        </Form>
      </Modal.Body>
      <Modal.Footer className="bg-primary bg-opacity-50">
        <Button
          variant="outline-success"
          type="submit"
          onClick={handleSave}
          className="border shadow rounded-pill"
        >
          Save Changes
        </Button>
        <Button
          variant="outline-dark"
          onClick={props.onHide}
          className="border shadow rounded-pill"
        >
          Close
        </Button>
      </Modal.Footer>
    </Modal>
  );
}
