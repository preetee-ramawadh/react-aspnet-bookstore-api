import { Button } from "react-bootstrap";
import BadgeAlphabetic from "./BadgeAlphabetic";

export default function SortBookTitle(props) {
  const handleBookByNameSort = () => {
    const data = props.sortBookByName;
    if (props.sortBookByNameStatus) {
      let sorted = data.sort((a, b) =>
        a.title?.toLowerCase().localeCompare(b.title?.toLowerCase())
      );
      props.setsortBookByName(sorted);
      props.setSortBookByNameStatus(!props.sortBookByNameStatus);
    } else {
      let sorted = data.sort((a, b) =>
        b.title?.toLowerCase().localeCompare(a.title?.toLowerCase())
      );
      props.setsortBookByName(sorted);
      props.setSortBookByNameStatus(!props.sortBookByNameStatus);
    }
  };
  console.log("::", props.sortBookByName);

  return (
    <Button
      type="button"
      variant="outline-secondary"
      onClick={handleBookByNameSort}
    >
      Name <BadgeAlphabetic />
    </Button>
  );
}
