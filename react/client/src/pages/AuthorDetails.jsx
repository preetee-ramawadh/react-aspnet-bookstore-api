import React from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Placeholder from "react-bootstrap/Placeholder";

export default function AuthorDetails(props) {
  return (
    <Modal
      {...props}
      size="lg"
      aria-labelledby="contained-modal-title-vcenter"
      backdrop="static"
      keyboard={false}
      centered
    >
      <Modal.Header closeButton className="bg-info text-dark text-capitalize">
        <Placeholder
          as={Modal.Title}
          animation="glow"
          id="contained-modal-title-vcenter"
        >
          <Placeholder bg="info">
            <strong>{props.selectedauthor.name}</strong>
          </Placeholder>
        </Placeholder>
      </Modal.Header>
      <Modal.Body className="bg-info bg-opacity-25">
        <h3 className="text-center">
          <strong>Biography</strong>
        </h3>
        <p>
          <i>{props.selectedauthor.biography}</i>
        </p>
        <h3 className="text-center">
          <strong>Penned Books</strong>
        </h3>

        <ol className="text-dark">
          <strong>
            {props.selectedauthor.Books?.length > 0 ? (
              props.selectedauthor.Books?.map((book) => (
                <li key={book.book_id}>
                  <h5>
                    <i>{book.title}</i>
                  </h5>
                </li>
              ))
            ) : (
              <h4>
                <i>No Books available here</i>
              </h4>
            )}
          </strong>
        </ol>
      </Modal.Body>
      <Modal.Footer className="bg-info bg-opacity-50">
        <Button variant="outline-dark rounded-pill" onClick={props.onHide}>
          Close
        </Button>
      </Modal.Footer>
    </Modal>
  );
}
