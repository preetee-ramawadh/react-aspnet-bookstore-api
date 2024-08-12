import { Button } from "react-bootstrap";
import BadgeNumeric from "./BadgeNumeric";

export default function SortBookPrice(props) {
  const handleBookByPriceSort = () => {
    const data = props.sortBookByPrice;
    if (props.sortBookByPriceStatus) {
      let sorted = data.sort((a, b) => a.price - b.price);
      props.setsortBookByPrice(sorted);
      props.setSortBookByPriceStatus(!props.sortBookByPriceStatus);
    } else {
      let sorted = data.sort((a, b) => b.price - a.price);
      props.setsortBookByPrice(sorted);
      props.setSortBookByPriceStatus(!props.sortBookByPriceStatus);
    }
  };
  console.log("sortBookByPrice::", props.sortBookByPrice);

  return (
    <Button
      type="button"
      variant="outline-secondary"
      onClick={handleBookByPriceSort}
    >
      Price <BadgeNumeric />
    </Button>
  );
}
