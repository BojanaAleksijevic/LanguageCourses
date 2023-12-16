import React, { useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import axios from 'axios';

function VerifikacijaComponent() {
  const location = useLocation();

  useEffect(() => {
    // Parsiranje query stringa
    const queryParams = new URLSearchParams(location.search);
    const token = queryParams.get('token');
    console.log('Token:', token);

    // Napravite URL bez dodavanja tokena u query string
    const url = 'https://localhost:5001/api/User/verify';

    const data = {
      Token: token
    };
    

    // Slanje POST zahteva
    axios.post(url, token)
      .then((response) => {
        alert(response.data);  // Ispisujemo odgovor na ekranu
      })
      .catch((error) => {
        alert(error);
      });
  }, [location]);

  return (
    <div><h1>aaaaaaaaaaaaaa</h1></div>
  );
}

export default VerifikacijaComponent;
