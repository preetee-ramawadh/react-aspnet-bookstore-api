import React from "react";
import Card from "react-bootstrap/Card";
import { useEffect, useState } from "react";
import Spinner from "./Spinner";
import GenreDetails from "./GenreDetails";
import AddGenre from "./AddGenre";
import Button from "react-bootstrap/Button";
import PaginationOnData from "./PaginationOnData";
import SortGenreName from "./SortGenreName";
import Form from "react-bootstrap/Form";
import Nav from "react-bootstrap/Nav";
import { getAPIToken } from "../config";

export default function AllGenres({
  currentPage,
  setCurrentPage,
  recordsPerPage,
  filteredRecords,
  setFilteredRecords,
  search,
  setSearch,
}) {
  const [listOfGenres, setListOfGenres] = useState([]);

  const [selectedgenre, setSelectedgenre] = useState({});

  const [loading, setLoading] = useState(true);

  const [modalShow, setModalShow] = useState(false);

  const [addShow, setAddShow] = useState(false);

  const [imgURlArray, setImgURLArray] = useState([]);

  const [sortGenreByName, setsortGenreByName] = useState([]);

  const [sortGenreByNameStatus, setSortGenreByNameStatus] = useState(true);

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
        const jsonData = await response.json();
        console.log("jsonData", jsonData);
        setListOfGenres(jsonData);

        // Create a dictionary of images keyed by genre ID
        const imgURLDictionary = jsonData.reduce((acc, genre) => {
          acc[genre.genreId] = genre.imageUrl;
          return acc;
        }, {});

        setImgURLArray(imgURLDictionary);
        console.log("genres imgURlArray:", imgURLDictionary);

        setLoading(false);
      } catch (error) {
        console.error("Error fetching genres data:", error);
        setLoading(false); // Ensure loading is false on error
      }
    };
    fetchGenresData();
  }, []);

  //filtering data
  useEffect(() => {
   
      // Filter data with at least 3 letters
    const filtered = listOfGenres.filter((genre) => {
      return search.toLowerCase() === ""
        ? genre
        : genre.genreName.toLowerCase().includes(search.toLowerCase());
    });
    setFilteredRecords(filtered);
    setsortGenreByName(filtered);
  }, [search, listOfGenres]);

  const indexOfLastRecord = currentPage * recordsPerPage;
  const indexOfFirstRecord = indexOfLastRecord - recordsPerPage;
  const currentRecords = filteredRecords.slice(
    indexOfFirstRecord,
    indexOfLastRecord
  );
  const nPages = Math.ceil(filteredRecords.length / recordsPerPage);

  const handleShowGenreBooks = async (genreId) => {
    console.log("inside showGenreBooks: " + genreId);
    setModalShow(true);

    try {
      const token = getAPIToken(); // Retrieve the token
      console.log("token::",token);
      const response = await fetch(`https://localhost:7200/api/genres/${genreId}`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`, // Include the token in the Authorization header
        },
      });
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      const selectedGenreData = await response.json();
      console.log("selectedGenreData::", selectedGenreData);
      setSelectedgenre(selectedGenreData);
      setLoading(false);
    } catch (error) {
      console.error("Error fetching genres data:", error);
    }
  };

  if (loading) {
    return <Spinner />;
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
              <SortGenreName
                sortGenreByName={sortGenreByName}
                setsortGenreByName={setsortGenreByName}
                sortGenreByNameStatus={sortGenreByNameStatus}
                setSortGenreByNameStatus={setSortGenreByNameStatus}
              />
            </Nav.Link>
          </Nav.Item>
          <Nav.Item style={{ width: "50%" }}>
            <Nav.Link eventKey="link-2" style={{ height: "auto" }}>
              <Form.Control
                onChange={(e) => {
                  setSearch(e.target.value);
                }}
                placeholder="Search a Genre by Name"
                aria-label="Search an Genre by Name"
              />
            </Nav.Link>
          </Nav.Item>
        </Nav>

        <AddGenre
          addShow={addShow}
          setAddShow={setAddShow}
          listOfGenres={listOfGenres}
          setListOfGenres={setListOfGenres}
        />

        <PaginationOnData
          nPages={nPages}
          currentPage={currentPage}
          setCurrentPage={setCurrentPage}
        />
      </div>
      {currentRecords?.length > 0 ? (
        currentRecords.map((genre, key) => {
          const imgUrl =
            imgURlArray[genre.genreId] ||
            "/images/genres/genreimageunavailable.jpg";

          return (
            <div className="col mt-3">
              <Card
                key={key}
                className="bg-dark border border-2 border-start-0 border-top-0 mt-3"
                style={{ height: "270px", width: "240px" }}
              >
                <Card.Img
                  variant="top"
                  src={imgUrl}
                  alt="genre specific image"
                  style={{ height: "200px" }}
                />
                <Card.Body className="text-center bg-secondary">
                  <Card.Title>
                    <Card.Link
                      id={genre.genreId}
                      href="#"
                      onClick={() => {
                        handleShowGenreBooks(genre.genreId);
                      }}
                      className="text-light text-capitalize text-decoration-none fs-4"
                    >
                      {genre.genreName}
                    </Card.Link>
                  </Card.Title>
                </Card.Body>
              </Card>
            </div>
          );
        })
      ) : (
        <h1>No Genre Present!!</h1>
      )}

      <GenreDetails
        show={modalShow}
        onHide={() => setModalShow(false)}
        selectedgenre={selectedgenre}
      />
    </div>
  );
}
