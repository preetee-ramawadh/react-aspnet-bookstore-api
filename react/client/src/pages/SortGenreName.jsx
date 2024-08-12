import { Button } from "react-bootstrap";
import BadgeAlphabetic from "./BadgeAlphabetic";

export default function SortGenreName(props) {
  const handleGenreByNameSort = () => {
    const data = props.sortGenreByName;
    if (props.sortGenreByNameStatus) {
      let sorted = data.sort((a, b) =>
        a.genre_name?.toLowerCase().localeCompare(b.genre_name?.toLowerCase())
      );
      props.setsortGenreByName(sorted);
      props.setSortGenreByNameStatus(!props.sortGenreByNameStatus);
    } else {
      let sorted = data.sort((a, b) =>
        b.genre_name?.toLowerCase().localeCompare(a.genre_name?.toLowerCase())
      );
      props.setsortGenreByName(sorted);
      props.setSortGenreByNameStatus(!props.sortGenreByNameStatus);
    }
  };
  console.log("::", props.sortGenreByName);

  return (
    <Button
      type="button"
      variant="outline-secondary"
      onClick={handleGenreByNameSort}
    >
      Name <BadgeAlphabetic />
    </Button>
  );
}
