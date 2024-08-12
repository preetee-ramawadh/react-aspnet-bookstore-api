import React from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Placeholder from "react-bootstrap/Placeholder";

export default function GenreDetails(props) {
  return (
    <Modal
      {...props}
      size="md"
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
            <strong>{props.selectedgenre.genre_name}</strong>
          </Placeholder>
        </Placeholder>
      </Modal.Header>
      <Modal.Body className="bg-info bg-opacity-25">
        <ol className="text-dark text-capitalize">
          <strong>
            {props.selectedgenre.Books?.length > 0 ? (
              props.selectedgenre.Books?.map((book) => (
                <li key={book.book_id}>
                  <h5>{book.title}</h5>
                </li>
              ))
            ) : (
              <h5>No Books available in this genre</h5>
            )}
          </strong>
        </ol>
      </Modal.Body>
      <Modal.Footer className="bg-info bg-opacity-50">
        <Button
          variant="outline-dark"
          onClick={props.onHide}
          className="rounded-pill"
        >
          Close
        </Button>
      </Modal.Footer>
    </Modal>
  );
}
