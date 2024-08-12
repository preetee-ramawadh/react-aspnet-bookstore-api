import Nav from "react-bootstrap/Nav";
import Button from "react-bootstrap/Button";

export default function AddButton({ buttonname, setAddShow }) {
  return (
    <Nav
      className="bg-body-tertiary"
      style={{ marginLeft: "5rem", marginRight: "5rem", padding: "1rem" }}
    >
      <Nav.Item>
        <Nav.Link style={{ height: "auto" }}>
          <Button
            variant="outline-secondary"
            onClick={() => setAddShow(true)}
            className="shadow border border-secondary fw-bold ms-1 rounded-pill"
            style={{ width: "99%" }}
          >
            {buttonname}
          </Button>
        </Nav.Link>
      </Nav.Item>
    </Nav>
  );
}
