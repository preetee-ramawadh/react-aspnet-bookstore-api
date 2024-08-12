import { useEffect, useState } from "react";
import "./App.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import AllBooks from "./pages/AllBooks";
import AllAuthors from "./pages/AllAuthors";
import AllGenres from "./pages/AllGenres";
import SidePanel from "./pages/SidePanel";
import NotFound from "./pages/NotFound";
import { setAPIToken } from "./config";
import { createNewToken } from "./pages/Authenticate";

function App() {
  /** Authentication logic */
  const [token, setToken] = useState(null);

  useEffect(()=>{
    //credentials here
    const credentials = {
    userName: "Preetee",
    password: "RamAwadh",
  };
  createNewToken(credentials)
  .then(data => {
    console.log("Token set:", data);
    setToken(data); 
    setAPIToken(data); // Store the token
  }).catch(error=>{
    console.error("error in fetching token:", error);
  });
  }, []);

  /** pagination logic */

  const [currentPage, setCurrentPage] = useState(1);
  const [recordsPerPage] = useState(10);

  const [filteredRecords, setFilteredRecords] = useState([]);
  const [search, setSearch] = useState("");

  return (
    <div className="containerCustom">
      <SidePanel />
      <main className="scroll-container pt-0">
        <Router>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route
              path="/books"
              element={
                <AllBooks
                  currentPage={currentPage}
                  setCurrentPage={setCurrentPage}
                  recordsPerPage={recordsPerPage}
                  filteredRecords={filteredRecords}
                  setFilteredRecords={setFilteredRecords}
                  search={search}
                  setSearch={setSearch}
                />
              }
            />
            <Route
              path="/authors"
              element={
                <AllAuthors
                  currentPage={currentPage}
                  setCurrentPage={setCurrentPage}
                  recordsPerPage={recordsPerPage}
                  filteredRecords={filteredRecords}
                  setFilteredRecords={setFilteredRecords}
                  search={search}
                  setSearch={setSearch}
                />
              }
            />
            <Route
              path="/genres"
              element={
                <AllGenres
                  currentPage={currentPage}
                  setCurrentPage={setCurrentPage}
                  //recordsPerPage={recordsPerPage}
                  recordsPerPage="10"
                  filteredRecords={filteredRecords}
                  setFilteredRecords={setFilteredRecords}
                  search={search}
                  setSearch={setSearch}
                />
              }
            />
            <Route path="/404" element={<NotFound />} />
            <Route path="*" element={<NotFound />} />
          </Routes>
        </Router>
      </main>
    </div>
  );
}

export default App;
