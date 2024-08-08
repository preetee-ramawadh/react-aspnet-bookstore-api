INSERT INTO Genres VALUES('classic','/images/genres/classic.png');
INSERT INTO Genres VALUES('fairytale','/images/genres/fairytale.png');
INSERT INTO Genres VALUES('esoteric','/images/genres/esoteric.png');
INSERT INTO Genres VALUES('autobiography','/images/genres/autobiography.png');
INSERT INTO Genres VALUES('thriller','/images/genres/thriller.png');
INSERT INTO Genres VALUES('fiction','/images/genres/fiction.png');


INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('mahatma gandhi','Mahatma Gandhi, born Mohandas Karamchand Gandhi on October 2, 1869, in Porbandar','/images/authors/mahatma-gandhi.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('sudha murthy','Sudha Murthy, born Sudha Kulkarni on August 19, 1950, in Shiggaon, Karnataka.','/images/authors/sudha-murthy.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('paramhamsa yogananda','Paramhansa Yogananda, born Mukunda Lal Ghosh on January 5, 1893, in Gorakhpur, India, was a revered Indian yogi and spiritual teacher','/images/authors/paramhamsa-yogananda.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('bankim chandra chatterji','Bankim Chandra Chatterjee, also known as Bankim Chandra Chattopadhyay, was a prominent Bengali writer and poet, born on June 27, 1838, in Naihati, West Bengal, India','/images/authors/bankim.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('A. P. J. Abdul Kalam','Dr. A.P.J. Abdul Kalam, born Avul Pakir Jainulabdeen Abdul Kalam on October 15, 1931, in Rameswaram, India, was a renowned Indian scientist and the 11th President of India (2002-2007).','/images/authors/APJAbdulKalam.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('Colleen hoover','Colleen Hoover, born on December 11, 1979, in Sulphur Springs, Texas, USA, is a bestselling American author known for her works in contemporary romance and young adult fiction.','/images/authors/colleen-hoover.jpg');


DELETE FROM Authors;
DELETE FROM Authors WHERE AuthorId = 7;
-- Reset the identity seed value to 1
DBCC CHECKIDENT ('Authors', RESEED, 0);

INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('The Poison Tree',4,7,500,'1884-01-01','/images/books/the-poision-tree.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('The Power of Nonviolence',1,5,200,'1927-01-01','/images/books/my-experiments-with-truth.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('It Starts with Us',6,4,600,'2022-10-18','/images/books/it-starts-with-us.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('It Ends with Us',6,4,600,'2016-08-02','/images/books/it-ends-with-us.jpg');

DELETE FROM Books WHERE BookId = 7;
-- Reset the identity seed value to 1
DBCC CHECKIDENT ('Books', RESEED, 0);