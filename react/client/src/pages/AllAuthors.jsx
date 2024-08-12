import React from "react";
import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import { useEffect, useState } from "react";
import AuthorDetails from "./AuthorDetails";
import Spinner from "./Spinner";
import AddAuthor from "./AddAuthor";
import EditAuthor from "./EditAuthor";
import DeleteAlertAuthor from "./DeleteAlertAuthor";
import Form from "react-bootstrap/Form";
import Nav from "react-bootstrap/Nav";
import SortAuthorName from "./SortAuthorName";
import EditIcon from "./EditIcon";
import PaginationOnData from "./PaginationOnData";

export default function AllAuthors({
  currentPage,
  setCurrentPage,
  recordsPerPage,
  filteredRecords,
  setFilteredRecords,
  search,
  setSearch,
}) {
  const [listOfAuthors, setListOfAuthors] = useState([]);
  const [loading, setLoading] = useState(true);
  const [selectedauthor, setSelectedAuthor] = useState({});

  const [authoridtodelete, setauthoridtodelete] = useState({});

  const [modalShow, setModalShow] = useState(false);

  const [alertShow, setAlertShow] = useState(false);

  const [addShow, setAddShow] = useState(false);

  const [showEditAuthorModal, setShowEditAuthorModal] = useState(false);

  const [sortAuthorByName, setsortAuthorByName] = useState([]);

  const [sortAuthorByNameStatus, setSortAuthorByNameStatus] = useState(true);

  const [imgURlArray, setImgURLArray] = useState([]);

  useEffect(() => {
    const fetchAuthorsData = async () => {
      try {
        const response = await fetch("https://localhost:7200/api/authors");
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const jsonData = await response.json();
        console.log("jsonData", jsonData);
        setListOfAuthors(jsonData);

        // Create a dictionary of images keyed by book ID
        const imgURLDictionary = jsonData.reduce((acc, author) => {
          acc[author.authorId] = author.imageUrl;
          return acc;
        }, {});

        setImgURLArray(imgURLDictionary);
        console.log("author imgURlArray:", imgURLDictionary);

        setLoading(false);
      } catch (error) {
        console.error("Error fetching authors data:", error);
      }
    };
    fetchAuthorsData();
  }, []);

  //filtering data
  useEffect(() => {
    const filtered = listOfAuthors.filter((author) => {
      return search.toLowerCase() === ""
        ? author
        : author.name.toLowerCase().includes(search);
    });

    setFilteredRecords(filtered);
    setsortAuthorByName(filtered);
  }, [search, listOfAuthors]);

  const indexOfLastRecord = currentPage * recordsPerPage;
  const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
  const currentRecords = filteredRecords.slice(
    indexOfFirstRecord,
    indexOfLastRecord
  );
  const nPages = Math.ceil(filteredRecords.length / recordsPerPage);

  //function to pass to child component to update author
  const updateauthor = (updatedAuthor) => {
    // Find the index of the author with the provided authorId
    const authorIndex = listOfAuthors.findIndex(
      (author) => author.authorId === updatedAuthor.authorId
    );

    // Make sure the task exists
    if (authorIndex !== -1) {
      // Create a copy of the author array
      const updatedAuthorsList = [...listOfAuthors];

      // Update the author with the new data
      updatedAuthorsList[authorIndex] = {
        ...updatedAuthorsList[authorIndex],
        name: updatedAuthor.name,
        biography: updatedAuthor.biography,
      };

      setListOfAuthors(updatedAuthorsList);
    }
  };

  const showAuthorDetails = (author) => {
    console.log("inside showAuthorDetails function", author.authorId);
    setSelectedAuthor(author);
    setModalShow(true);
  };

  const editAuthorDetails = (author) => {
    console.log("inside editAuthorDetails authorId::", author.authorId);
    setSelectedAuthor(author);
    console.log("author to edit:", selectedauthor);
    setShowEditAuthorModal(true); //open Edit Author Modal
  };

  if (listOfAuthors.length > 0) {
    if (loading) {
      return <Spinner />;
    }
  }

  return (
    <div className="row">
      <div className="fixed-container">
        <Nav className="justify-content-end">
          <Nav.Item style={{ width: "auto" }}>
            <Nav.Link eventKey="link-1" style={{ height: "auto" }}>
              {" "}
              <Button
                variant="outline-danger"
                onClick={() => setAddShow(true)}
                className="rounded-circle border fw-bold text-secondary border-secondary"
                style={{ width: "100%" }}
              >
                +
              </Button>
            </Nav.Link>
          </Nav.Item>

          <Nav.Item style={{ width: "auto" }}>
            <Nav.Link eventKey="link-1" style={{ height: "auto" }}>
              {" "}
              <SortAuthorName
                sortAuthorByName={sortAuthorByName}
                setsortAuthorByName={setsortAuthorByName}
                sortAuthorByNameStatus={sortAuthorByNameStatus}
                setSortAuthorByNameStatus={setSortAuthorByNameStatus}
              />
            </Nav.Link>
          </Nav.Item>
          <Nav.Item style={{ width: "50%" }}>
            <Nav.Link eventKey="link-2" style={{ height: "auto" }}>
              <Form.Control
                onChange={(e) => {
                  setSearch(e.target.value);
                }}
                placeholder="Search an Author by Name"
                aria-label="Search an Author by Name"
              />
            </Nav.Link>
          </Nav.Item>
        </Nav>

        <AddAuthor
          addShow={addShow}
          setAddShow={setAddShow}
          listOfAuthors={listOfAuthors}
          setListOfAuthors={setListOfAuthors}
        />

        <PaginationOnData
          nPages={nPages}
          currentPage={currentPage}
          setCurrentPage={setCurrentPage}
        />

        <DeleteAlertAuthor
          alertShow={alertShow}
          setAlertShow={setAlertShow}
          listOfAuthors={listOfAuthors}
          setListOfAuthors={setListOfAuthors}
          authoridtodelete={authoridtodelete}
          value="Author"
        />
      </div>
      {currentRecords?.length > 0 ? (
        currentRecords.map((author, key) => {
          const imgUrl =
            imgURlArray[author.authorId] ||
            "/images/authors/imagesunavailable.jpg";

          return (
            <div key={key} className="col m-2">
              <Card
                className="border rounded-pill text-center overflow-hidden border-2 border-start-0 border-top-0"
                style={{
                  borderRadius: "0 0 4em 0",
                  height: "360px",
                  width: "250px",
                  maxHeight: "500px",
                  maxWidth: "250px",
                }}
              >
                <Card.Img
                  variant="top"
                  //src="/images/authors/imagesunavailable.jpg"
                  //src={author.image_url}
                  src={imgUrl}
                  alt="no image"
                  style={{ maxHeight: "200px", maxWidth: "250px" }}
                />

                <Card.Body className="bg-secondary">
                  <Card.Link
                    id={author.authorId}
                    href="#"
                    onClick={() => {
                      showAuthorDetails(author);
                    }}
                    className="text-light text-capitalize text-center text-decoration-none fs-4"
                  >
                    {author.name}
                  </Card.Link>
                </Card.Body>
                <Card.Footer className="border-0 bg-secondary text-center">
                  <Button
                    variant="outline-primary"
                    onClick={() => {
                      editAuthorDetails(author);
                    }}
                    className="me-3 mb-1 shadow border rounded-pill"
                  >
                    <EditIcon />
                  </Button>
                  <Button
                    variant="outline-danger"
                    onClick={() => {
                      setauthoridtodelete(author.authorId);
                      setAlertShow(true);
                    }}
                    className="rounded-circle border fw-bold mb-1"
                  >
                    {" "}
                    X
                  </Button>
                </Card.Footer>
              </Card>
            </div>
          );
        })
      ) : (
        <h1>No Authors Present!!</h1>
      )}

      <EditAuthor
        show={showEditAuthorModal}
        onHide={() => setShowEditAuthorModal(false)}
        selectedauthor={selectedauthor}
        updateauthor={updateauthor}
      />

      <AuthorDetails
        show={modalShow}
        onHide={() => setModalShow(false)}
        selectedauthor={selectedauthor}
      />
    </div>
  );
}
