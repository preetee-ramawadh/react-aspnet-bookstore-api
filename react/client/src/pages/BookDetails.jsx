import React from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Placeholder from "react-bootstrap/Placeholder";
import Table from "react-bootstrap/Table";

export default function BookDetails(props) {
  return (
    <Modal
      {...props}
      size="md"
      aria-labelledby="contained-modal-title-vcenter"
      backdrop="static"
      keyboard={false}
      centered
    >
      <Modal.Header closeButton className="bg-info text-capitalize">
        <Placeholder
          as={Modal.Title}
          animation="glow"
          id="contained-modal-title-vcenter"
          className="text-dark"
        >
          <Placeholder bg="info">
            <strong>{props.selectedbook.title}</strong>
          </Placeholder>
        </Placeholder>

        <Modal.Title id="contained-modal-title-vcenter"></Modal.Title>
      </Modal.Header>
      <Modal.Body className="bg-info bg-opacity-25">
        <Table variant="info" className="border border-info">
          <tbody>
            <tr>
              <td>
                <strong>Author</strong>
              </td>
              <td>{props.selectedbook.author?.name}</td>
            </tr>
            <tr>
              <td>
                <strong>Genre</strong>
              </td>
              <td>{props.selectedbook.genre?.genreName}</td>
            </tr>
            <tr>
              <td>
                <strong>Price</strong>
              </td>
              <td colSpan={2}>{props.selectedbook.price}</td>
            </tr>
            <tr>
              <td>
                <strong>Published On</strong>
              </td>
              <td colSpan={2}>{props.selectedbook.publicationDate}</td>
            </tr>
          </tbody>
        </Table>
      </Modal.Body>
      <Modal.Footer className="bg-info bg-opacity-50">
        <Button variant="outline-dark rounded-pill" onClick={props.onHide}>
          Close
        </Button>
      </Modal.Footer>
    </Modal>
  );
}
