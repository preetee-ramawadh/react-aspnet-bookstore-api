import Carousel from "react-bootstrap/Carousel";
import Image from "react-bootstrap/Image";

function HomePageCarousal() {
  return (
    <Carousel fade interval={2000}>
      <Carousel.Item>
        <Image
          src="/images/intro.jpg"
          fluid
          className="border border-secondary"
        />
        <Carousel.Caption className="text-dark">
          <h2>
            <strong>BookShip</strong>
          </h2>
          <h3>
            <strong>
              <u>Connecting the World through Books.</u>
            </strong>
          </h3>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <Image
          src="/images/bookmission.jpg"
          fluid
          className="border border-secondary"
        />
        <Carousel.Caption>
          <h2>
            <strong>Mission</strong>
          </h2>
          <p>
            "To connect book lovers with the stories and knowledge they seek{" "}
            <br />
            by providing a personalized, accessible, and engaging reading
            experience."
          </p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <Image
          src="/images/bookvision.jpg"
          fluid
          className="border border-secondary"
        />
        <Carousel.Caption>
          <h2>
            <strong>Vision</strong>
          </h2>
          <p>
            "To be the leading digital destination for readers worldwide,
            <br />
            revolutionizing the way people discover, access, and enjoy books."
          </p>
        </Carousel.Caption>
      </Carousel.Item>
    </Carousel>
  );
}

export default HomePageCarousal;
