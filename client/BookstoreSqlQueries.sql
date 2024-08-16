Delete from Genres;
-- Reset the identity seed value to 1
DBCC CHECKIDENT ('Genres', RESEED, 0);

INSERT INTO Genres VALUES('classic','/images/genres/classic.png');
INSERT INTO Genres VALUES('fairytale','/images/genres/fairytale.png');
INSERT INTO Genres VALUES('esoteric','/images/genres/esoteric.png');
INSERT INTO Genres VALUES('autobiography','/images/genres/autobiography.png');
INSERT INTO Genres VALUES('thriller','/images/genres/thriller.png');
INSERT INTO Genres VALUES('fiction','/images/genres/fiction.png');
INSERT INTO Genres VALUES('biography','/images/genres/biography.png');
INSERT INTO Genres VALUES('sci-fi','/images/genres/sci-fi.png');
INSERT INTO Genres VALUES('science','/images/genres/science.png');
INSERT INTO Genres VALUES('unknown','/images/genres/genreimageunavailable.jpg');


DELETE FROM Authors;
DELETE FROM Authors WHERE AuthorId = 7;
-- Reset the identity seed value to 1
DBCC CHECKIDENT ('Authors', RESEED, 0);

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('mahatma gandhi','Mahatma Gandhi, born Mohandas Karamchand Gandhi on October 2, 1869, in Porbandar, India, was a pivotal leader in the Indian independence movement against British colonial rule. He is renowned for his philosophy of non-violence (ahimsa) and civil disobedience, which he used as powerful tools to fight for justice and social change.
Gandhi studied law in London and practiced as a barrister in South Africa, where he first developed his principles of non-violent resistance while fighting racial discrimination. Returning to India in 1915, he led several movements for social and political reform, including the Non-Cooperation Movement, the Salt March of 1930, and the Quit India Movement of 1942.','/images/authors/mahatma-gandhi.jpg');

INSERT INTO Authors VALUES('sudha murthy','Sudha Murthy, born Sudha Kulkarni on August 19, 1950, in Shiggaon, Karnataka, India, is a prominent Indian author, social worker, and philanthropist. She is best known as the Chairperson of the Infosys Foundation, which supports various initiatives in education, healthcare, and rural development. Sudha Murthy is also a celebrated author, having written numerous books in English and Kannada, including novels, travelogues, and childrens literature. Her writing often reflects her deep concern for social issues and her commitment to making a positive impact on society.
Her contributions to literature and philanthropy have earned her several accolades and recognition, including honorary doctorates and awards for her humanitarian work. Sudha Murthys life and work continue to inspire many for their commitment to social change and the betterment of society.','/images/authors/sudha-murthy.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('paramhamsa yogananda','Paramhansa Yogananda, born Mukunda Lal Ghosh on January 5, 1893, in Gorakhpur, India, was a revered Indian yogi and spiritual teacher renowned for his efforts to introduce Eastern spirituality to the Western world. He is best known for his book "Autobiography of a Yogi," which has inspired millions with its teachings on self-realization and spiritual enlightenment.
Yogananda embarked on a spiritual quest at a young age and became a disciple of the great yogi Sri Yukteswar Giri. He founded the Self-Realization Fellowship (SRF) in 1920, which played a crucial role in spreading the teachings of Kriya Yoga and the principles of self-realization across the globe.','/images/authors/paramhamsa-yogananda.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('bankim chandra chatterji','Bankim Chandra Chatterjee, also known as Bankim Chandra Chattopadhyay, was a prominent Bengali writer and poet, born on June 27, 1838, in Naihati, West Bengal, India. He is celebrated as one of the foremost figures in modern Bengali literature and a key contributor to the Bengali Renaissance.
Chatterjee is best known for his novel "Anandamath," published in 1882, which features the famous patriotic song "Vande Mataram" ("Hail to the Motherland"). This song became a rallying cry during the Indian independence movement and was later adopted as the national song of India. "Anandamath" is notable for its portrayal of the struggles of Indian freedom fighters and its emphasis on nationalistic fervor.','/images/authors/bankim.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('A. P. J. Abdul Kalam','Dr. A.P.J. Abdul Kalam, born Avul Pakir Jainulabdeen Abdul Kalam on October 15, 1931, in Rameswaram, India, was a renowned Indian scientist and the 11th President of India (2002-2007). Often referred to as the "Missile Man of India," he played a pivotal role in Indias space and missile programs, including the development of the countrys ballistic missiles and satellite launch vehicles.
Kalam earned his degree in aeronautical engineering from the Madras Institute of Technology and began his career working on satellite and missile technology at the Indian Space Research Organisation (ISRO) and the Defence Research and Development Organisation (DRDO). His contributions were crucial to Indias successful launch of its first satellite, Aryabhata, and the development of the Agni and Prithvi missiles.','/images/authors/APJAbdulKalam.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('Colleen hoover','Colleen Hoover, born on December 11, 1979, in Sulphur Springs, Texas, USA, is a bestselling American author known for her works in contemporary romance and young adult fiction. She gained widespread recognition with her debut novel, "Slammed", which she self-published in 2012. The books success led to her gaining a large following and establishing herself as a prominent figure in the literary world.
Hoovers writing is characterized by its emotional depth and complex characters. Her notable works include "Hopeless", "Maybe Someday", "Ugly Love", and "It Ends with Us". The latter, in particular, has received significant acclaim and was named a Goodreads Choice Award winner for Best Romance.
In addition to her novels, Hoover is praised for her active engagement with her readers and her philanthropic efforts. She frequently participates in charity events and uses her platform to support various causes.','/images/authors/colleen-hoover.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('David Godman','David Godman is a well-regarded author and teacher known for his writings on Sri Ramana Maharshi and other Indian spiritual teachers. Born on March 8, 1954, in the United Kingdom, Godmans work primarily focuses on the life and teachings of Sri Ramana Maharshi, a revered modern Indian sage. 
Godman has authored several books on Sri Ramana Maharshi and other related topics. His notable works include "Be As You Are: The Teachings of Sri Ramana Maharshi", "The Teachings of Sri Ramana Maharshi in His Own Words", and "Reminiscences of the Early Days with Bhagavan Sri Ramana Maharshi". These books are appreciated for their clarity and insight into Maharshis teachings.','/images/authors/david-godman.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('Paulo Coelho','Paulo Coelho was born on August 24, 1947, in Rio de Janeiro, Brazil. He is a renowned Brazilian author known for his inspirational and philosophical novels. Coelhos works have been translated into more than 80 languages and have sold over 225 million copies worldwide. His most famous book is The Alchemist.

Coelhos early life was marked by a rebellious spirit, and he struggled with a variety of personal issues before turning to writing. His career took off after the publication of "The Alchemist", which became an international bestseller and established him as a significant literary figure. Coelhos writing often explores themes of spirituality, destiny, and self-discovery.','/images/authors/paulo-coelho.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('rk narayanan','R.K. Narayan, full name Rasipuram Krishnaswami Iyer Narayanaswami, was an influential Indian writer known for his contributions to English literature. He was born on October 10, 1906, in Chennai, India, and passed away on May 13, 2001.','/images/authors/rk-narayanan.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('Paul Brunton','Paul Brunton, born Raphael Hurst on December 21, 1898, was a British writer and philosopher renowned for his works on Eastern spirituality and philosophy. He passed away on July 27, 1981.','/images/authors/Paul-Brunton.jpg');

INSERT INTO Authors (Name, Biography, ImageUrl) VALUES('Gyllian Flynn','Gillian Flynn is an acclaimed American author known for her gripping psychological thrillers and screenwriting. Born on February 24, 1971, in Kansas City, Missouri, Flynn has made a significant impact on contemporary literature with her distinctive style and complex characters. 
In addition to her novels, Flynn has worked as a screenwriter. She adapted her own novel "Gone Girl" into a screenplay for the 2014 film, which received critical acclaim and several award nominations.','/images/authors/Gillian_Flynn.jpg');

DELETE FROM Books;

DELETE FROM Books WHERE BookId = 7;
-- Reset the identity seed value to 1
DBCC CHECKIDENT ('Books', RESEED, 0);

INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('The Poison Tree',4,7,500,'1884-01-01','/images/books/the-poision-tree.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('The Power of Nonviolence',1,5,200,'1927-01-01','/images/books/my-experiments-with-truth.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('It Starts with Us',6,4,600,'2022-10-18','/images/books/it-starts-with-us.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('It Ends with Us',6,4,600,'2016-08-02','/images/books/it-ends-with-us.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('Wings of Fire',5,5,300,'1999-03-01','/images/books/wings-of-fire.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('Be as you are',7,'4',600,'1985-09-01','/images/books/be-as-you-are.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('common-uncommon',2,7,300,'2021-05-10','/images/books/common-uncommon.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('Autobiography of a Yogi',3,5,1200,'1946-09-01','/images/books/autobiography-of-a-yogi.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('The Alchemist',8,7,800,'1988-04-01','/images/books/the_alchemist.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('Gone Girl',11,6,350,'2012-06-05','/images/books/gone-girl.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('a search in secret india',11,6,350,'2012-06-05','/images/books/a-search-in-secret-india.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('the magic drum',7,6,250,'2012-06-05','/images/books/the-magic-drum.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('malgudi days123',9,6,350,'2012-06-05','/images/books/malgudi-days.jpg');


INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('The Poison Tree',4,7,500,'1884-01-01','/images/books/the-poision-tree.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('The Power of Nonviolence',1,5,200,'1927-01-01','/images/books/my-experiments-with-truth.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('It Starts with Us',6,4,600,'2022-10-18','/images/books/it-starts-with-us.jpg');
INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('It Ends with Us',6,4,600,'2016-08-02','/images/books/it-ends-with-us.jpg');

INSERT INTO Books (Title, AuthorId, GenreId, Price, PublicationDate, ImageUrl) VALUES('It Ends with Us',6,4,600,'2016-08-02','/images/books/it-ends-with-us.jpg');



-----FINAL DATA----
