import React from "react";
import { Alert, Button } from "react-bootstrap";

const DeleteAlertAuthor = ({
  alertShow,
  setAlertShow,
  listOfAuthors,
  setListOfAuthors,
  authoridtodelete,
  value,
}) => {
  const deleteAuthor = async (authorId) => {
    console.log("inside deleteAuthor function", authorId);

    try {
      const response = await fetch(
        `https://localhost:7200/api/authors/${authorId}`,
        {
          method: "DELETE",
        }
      );
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }

      // Update listOfAuthors after deletion
      setListOfAuthors(
        listOfAuthors.filter((author) => author.authorId !== authorId)
      );
      setAlertShow(false);
    } catch (error) {
      console.error("There was a problem with the DELETE request:", error);
    }
  };

  if (alertShow) {
    return (
      <Alert variant="danger" onClose={() => setAlertShow(false)} dismissible>
        <Alert.Heading>
          Are you sure you want to delete the {value}?
          <Button
            onClick={() => {
              deleteAuthor(authoridtodelete);
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

export default DeleteAlertAuthor;
