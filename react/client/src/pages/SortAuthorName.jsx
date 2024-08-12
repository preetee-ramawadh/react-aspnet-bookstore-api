import { Button } from "react-bootstrap";
import BadgeAlphabetic from "./BadgeAlphabetic";

export default function SortAuthorName(props) {
  const handleAuthorByNameSort = () => {
    const data = props.sortAuthorByName;
    if (props.sortAuthorByNameStatus) {
      let sorted = data.sort((a, b) =>
        a.name?.toLowerCase().localeCompare(b.name?.toLowerCase())
      );
      props.setsortAuthorByName(sorted);
      props.setSortAuthorByNameStatus(!props.sortAuthorByNameStatus);
    } else {
      let sorted = data.sort((a, b) =>
        b.name?.toLowerCase().localeCompare(a.name?.toLowerCase())
      );
      props.setsortAuthorByName(sorted);
      props.setSortAuthorByNameStatus(!props.sortAuthorByNameStatus);
    }
  };
  console.log("::", props.sortAuthorByName);

  return (
    <Button
      type="button"
      variant="outline-secondary"
      onClick={handleAuthorByNameSort}
    >
      Name <BadgeAlphabetic />
    </Button>
  );
}
