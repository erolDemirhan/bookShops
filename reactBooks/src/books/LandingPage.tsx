import { useEffect, useState } from "react";
import { landingPageDTO } from "./books.model";
import BooksList from "./BooksList";

export default function LandingPage(){

    const [books, setBooks] = useState<landingPageDTO>({});

    useEffect(() => {
      const timerId = setTimeout(() => {
        setBooks({
          inShops: [{
            id: 1,
            title: 'Da Vinci Code',
            poster: 'https://upload.wikimedia.org/wikipedia/en/thumb/6/6b/DaVinciCode.jpg/220px-DaVinciCode.jpg'
          },{
            id: 2,
            title: 'Angels and Demons',
            poster: 'https://upload.wikimedia.org/wikipedia/en/5/5f/AngelsAndDemons.jpg'
          }],
          upcomingBooks: [
            {
              id: 3,
              title: 'Deception Point',
              poster: 'https://upload.wikimedia.org/wikipedia/en/4/4a/DeceptionPointDanBrownNovel.jpg?20190607112302'
            }
          ]
        })
      }, 1000);
      return () => clearTimeout(timerId);
    });


    return(
        <>
            <h3>In Shops</h3>
            <BooksList books = {books.inShops}/>
            <h3>Upcoming Books</h3>
            <BooksList books={books.upcomingBooks}/>
        </>
    )
}