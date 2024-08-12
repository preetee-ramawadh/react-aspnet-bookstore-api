import React from "react";
import { Alert, Button } from "react-bootstrap";

const DeleteAlertBook = ({
  alertShow,
  setAlertShow,
  listOfBooks,
  setListOfBooks,
  booktodelete,
}) => {
  const deleteBook = async (bookId) => {
    console.log("bookid::", bookId);
    try {
      const response = await fetch(`https://localhost:7200/api/books/${bookId}`, {
        method: "DELETE",
      });
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }

      // Update listOfBooks after deletion
      setListOfBooks(listOfBooks.filter((book) => book.bookId !== bookId));
      setAlertShow(false);
    } catch (error) {
      console.error("There was a problem with the DELETE request:", error);
    }
  };

  if (alertShow) {
    return (
      <Alert variant="danger" onClose={() => setAlertShow(false)} dismissible>
        <Alert.Heading>
          Are you sure you want to delete the Book?
          <Button
            onClick={() => {
              deleteBook(booktodelete);
            }}
            variant="outline-success"
            className="ms-5"
          >
            Yes
          </Button>
        </Alert.Heading>
      </Alert>
    );
  }

  return null; // Return null if alertShow is false
};

export default DeleteAlertBook;
