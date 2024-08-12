import Pagination from "react-bootstrap/Pagination";

export default function PaginationOnData({
  nPages,
  currentPage,
  setCurrentPage,
}) {
  let items = [];
  for (let number = 1; number <= 5; number++) {
    items.push(
      <Pagination.Item key={number} active={number === currentPage}>
        {number}
      </Pagination.Item>
    );
  }

  const pageNumbers = [...Array(nPages + 1).keys()].slice(1);

  const goToNextPage = () => {
    if (currentPage !== nPages) setCurrentPage(currentPage + 1);
  };
  const goToPrevPage = () => {
    if (currentPage !== 1) setCurrentPage(currentPage - 1);
  };

  return (
    <nav className="mt-3 fixed-item">
      <ul className="pagination justify-content-end fw-bold mb-0 me-3">
        <li className="page-item">
          <a
            className="page-link border border-secondary border-1 bg-secondary bg-opacity-50"
            onClick={goToPrevPage}
            href="#"
            style={{ height: "auto" }}
            disabled={currentPage === 1}
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="18"
              height="15"
              fill="currentColor"
              viewBox="0 0 15 15"
            >
              <path
                fillRule="evenodd"
                d="M41.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.3 256 246.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160zm352-160l-160 160c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L301.3 256 438.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0z"
                className="fa-secondary"
              />
            </svg>
          </a>
        </li>
        {pageNumbers.map((pgNumber) => (
          <li
            key={pgNumber}
            className={`page-item ${currentPage == pgNumber ? "active" : ""} `}
          >
            <a
              onClick={() => setCurrentPage(pgNumber)}
              className="page-link border border-secondary border-1 bg-secondary bg-opacity-50"
              href="#"
            >
              {pgNumber}
            </a>
          </li>
        ))}
        <li className="page-item">
          <a
            className="page-link border border-secondary border-1 bg-secondary bg-opacity-50"
            onClick={goToNextPage}
            href="#"
            disabled={currentPage === nPages}
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="18"
              height="15"
              fill="currentColor"
              viewBox="0 0 15 15"
            >
              <g className="fa-group">
                <path
                  fillRule="evenodd"
                  d="M470.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-160-160c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L402.7 256 265.4 393.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0l160-160zm-352 160l160-160c12.5-12.5 12.5-32.8 0-45.3l-160-160c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L210.7 256 73.4 393.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0z"
                  className="fa-secondary"
                />
              </g>
            </svg>
          </a>
        </li>
      </ul>
    </nav>
  );
}
