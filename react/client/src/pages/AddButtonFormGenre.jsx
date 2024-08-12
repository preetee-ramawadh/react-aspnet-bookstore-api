import { useState } from "react";
import AddGenre from "./AddGenre";
import AddButton from "./AddButton";
export default function AddButtonFormGenre({ listOfGenres, setListOfGenres }) {
  const [addShow, setAddShow] = useState(false);
  const btnName = "~~~~~~~~~~~~~ADD A GENRE~~~~~~~~~~~~~";
  return (
    <>
      <AddButton setAddShow={setAddShow} buttonname={btnName} />

      <AddGenre
        addShow={addShow}
        setAddShow={setAddShow}
        listOfGenres={listOfGenres}
        setListOfGenres={setListOfGenres}
      />
    </>
  );
}
