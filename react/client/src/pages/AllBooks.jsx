import React from "react";
import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import { useEffect, useState } from "react";
import Spinner from "./Spinner";
import BookDetails from "./BookDetails";
import AddBook from "./AddBook";
import EditBook from "./EditBook";
import DeleteAlertBook from "./DeleteAlertBook";
import SortBookTitle from "./SortBookTitle";
import SortBookPrice from "./SortBookPrice";
import Form from "react-bootstrap/Form";
import Nav from "react-bootstrap/Nav";
import EditIcon from "./EditIcon";
import PaginationOnData from "./PaginationOnData";

export default function AllBooks({
  currentPage,
  setCurrentPage,
  recordsPerPage,
  filteredRecords,
  setFilteredRecords,
  search,
  setSearch,
}) {
  const [listOfBooks, setListOfBooks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [selectedbook, setSelectedBook] = useState({});

  const [modalShow, setModalShow] = useState(false); //modal to show book details

  const [alertShow, setAlertShow] = useState(false);

  const [booktodelete, setbooktodelete] = useState(false);

  const [addShow, setAddShow] = useState(false); //form to add a new book

  const [modalEditShow, setModalEditShow] = useState(false); //modal to edit book detail

  const [sortBookByPrice, setsortBookByPrice] = useState([]);

  const [sortBookByPriceStatus, setSortBookByPriceStatus] = useState(true);

  const [sortBookByName, setsortBookByName] = useState([]);

  const [sortBookByNameStatus, setSortBookByNameStatus] = useState(true);

  const [imgURlArray, setImgURLArray] = useState([]);

  useEffect(() => {
    fetchBooksData();
  }, []);

  //filtering data
  useEffect(() => {
    const filtered = listOfBooks.filter((book) => {
      return search.toLowerCase() === ""
        ? book
        : book.title.toLowerCase().includes(search);
    });

    setFilteredRecords(filtered);
    setsortBookByName(filtered);
    setsortBookByPrice(filtered);
  }, [search, listOfBooks]);

  const indexOfLastRecord = currentPage * recordsPerPage;
  const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
  const currentRecords = filteredRecords.slice(
    indexOfFirstRecord,
    indexOfLastRecord
  );
  const nPages = Math.ceil(filteredRecords?.length / recordsPerPage);

  const fetchBooksData = async () => {
    try {
      const response = await fetch("https://localhost:7200/api/books");
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      const jsonData = await response.json();
      console.log("jsonData", jsonData);
      setListOfBooks(jsonData);

      // Create a dictionary of images keyed by book ID
      const imgURLDictionary = jsonData.reduce((acc, book) => {
        acc[book.bookId] = book.imageUrl;
        return acc;
      }, {});

      setImgURLArray(imgURLDictionary);
      console.log("books imgURlArray:", imgURLDictionary);

      setLoading(false);
    } catch (error) {
      console.error("Error fetching books data:", error);
    }
  };

  if (listOfBooks.length > 0) {
    if (loading) {
      return <Spinner />;
    }
  }

  //function to pass to child component to update author
  const updatebook = (updatedBook) => {
    // Find the index of the task with the provided taskId
    const bookIndex = listOfBooks.findIndex(
      (book) => book.bookId === updatedBook.bookId
    );

    // Make sure the task exists
    if (bookIndex !== -1) {
      // Create a copy of the author array
      const updatedBooksList = [...listOfBooks];

      // Update the author with the new data
      updatedBooksList[bookIndex] = {
        ...updatedBooksList[bookIndex],
        title: updatedBook.title,
        authorId: updatedBook.authorId,
        genreId: updatedBook.genreId,
        price: updatedBook.price,
        publicationDate: updatedBook.publicationDate,
      };

      setListOfBooks(updatedBooksList);
    }
  };

  const showBookDetails = (book) => {
    console.log("inside showBookDetails function", book.bookId);
    setSelectedBook(book);
    setModalShow(true);
  };

  const editBookDetails = (book) => {
    console.log("inside editBookDetails function --bookid::", book.bookId);
    setSelectedBook(book);
    console.log("setSelectedBook:", selectedbook);
    setModalEditShow(true);
  };

  const addBook = () => {
    setAddShow(true);
  };

  return (
    <div className="row">
      <div className="fixed-container fa-secondary">
        <Nav className="justify-content-end">
          <Nav.Item style={{ width: "auto" }}>
            <Nav.Link style={{ height: "auto" }}>
              <Button
                variant="outline-danger"
                onClick={() => addBook()}
                className="rounded-circle border fw-bold text-secondary border-secondary"
                style={{ width: "100%" }}
              >
                +
              </Button>
            </Nav.Link>
          </Nav.Item>
          <Nav.Item style={{ width: "auto" }}>
            <Nav.Link style={{ height: "auto" }}>
              <SortBookPrice
                sortBookByPrice={sortBookByPrice}
                setsortBookByPrice={setsortBookByPrice}
                sortBookByPriceStatus={sortBookByPriceStatus}
                setSortBookByPriceStatus={setSortBookByPriceStatus}
              />
            </Nav.Link>
          </Nav.Item>
          <Nav.Item style={{ width: "auto" }}>
            <Nav.Link eventKey="link-1" style={{ height: "auto" }}>
              {" "}
              <SortBookTitle
                sortBookByName={sortBookByName}
                setsortBookByName={setsortBookByName}
                sortBookByNameStatus={sortBookByNameStatus}
                setSortBookByNameStatus={setSortBookByNameStatus}
              />
            </Nav.Link>
          </Nav.Item>
          <Nav.Item style={{ width: "45%" }}>
            <Nav.Link eventKey="link-2" style={{ height: "auto" }}>
              <Form.Control
                onChange={(e) => {
                  setSearch(e.target.value);
                }}
                placeholder="Search a Book by Title"
                aria-label="Search Book by title"
              />
            </Nav.Link>
          </Nav.Item>
        </Nav>

        <AddBook
          addShow={addShow}
          setAddShow={setAddShow}
          listOfBooks={listOfBooks}
          setListOfBooks={setListOfBooks}
        />
        <PaginationOnData
          nPages={nPages}
          currentPage={currentPage}
          setCurrentPage={setCurrentPage}
        />
        <DeleteAlertBook
          alertShow={alertShow}
          setAlertShow={setAlertShow}
          listOfBooks={listOfBooks}
          setListOfBooks={setListOfBooks}
          booktodelete={booktodelete}
        />
      </div>
      {currentRecords?.length > 0 ? (
        currentRecords.map((book, key) => {
          const imgUrl =
            imgURlArray[book.bookId] || "/images/books/imageunavailable.jpg";

          return (
            <div key={key} className="col mt-3">
              <Card
                style={{
                  borderRadius: "0 2em 0 0",
                  height: "350px",
                  width: "250px",
                  maxHeight: "500px",
                  maxWidth: "280px",
                }}
                className="overflow-hidden border border-2"
              >
                <Card.Img
                  variant="top"
                  src={imgUrl}
                  alt="no image"
                  style={{
                    maxHeight: "250px",
                    width: "250px",
                    height: "200px",
                  }}
                />
                <Card.Body className="text-center bg-secondary">
                  <Card.Link
                    id={book.bookId}
                    href="#"
                    onClick={() => {
                      showBookDetails(book);
                    }}
                    className="text-light text-capitalize text-decoration-none fs-5"
                  >
                    {book.title}
                  </Card.Link>
                </Card.Body>
                <Card.Footer className="border-0 text-center bg-secondary">
                  <Button
                    variant="outline-primary"
                    onClick={() => editBookDetails(book)}
                    className="me-3 mb-1 shadow border rounded-pill"
                  >
                    <EditIcon />
                  </Button>

                  <Button
                    variant="outline-danger"
                    onClick={() => {
                      setbooktodelete(book.bookId);
                      setAlertShow(true);
                    }}
                    className="rounded-circle border fw-bold"
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
        <h1>No books available!!</h1>
      )}
      <EditBook
        show={modalEditShow}
        onHide={() => setModalEditShow(false)}
        selectedbook={selectedbook}
        updatebook={updatebook}
      />
      <BookDetails
        show={modalShow}
        onHide={() => setModalShow(false)}
        selectedbook={selectedbook}
      />
    </div>
  );
}
