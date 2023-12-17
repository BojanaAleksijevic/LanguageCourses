import React, { useEffect, useState} from 'react';
import { useLocation } from 'react-router-dom';
import { Link } from "react-router-dom";
import axios from 'axios';
import Header from '../Header.js';
import Footer from '../Footer.js'

function VerifikacijaComponent() {
  
  const [isLogoRotated, setLogoRotation] = useState(false);

  const handleLogoHover = () => {
    setLogoRotation(true);
  };

  const handleLogoLeave = () => {
    setLogoRotation(false);
  };

  
  
  const location = useLocation();

  useEffect(() => {
    // Parsiranje query stringa
    const queryParams = new URLSearchParams(location.search);
    const token = queryParams.get('token');
    console.log('Token:', token);

    // Napravite URL bez dodavanja tokena u query string
    const url = 'https://localhost:5001/api/User/verify?token=' + token;

    // Slanje POST zahteva
    axios.post(url)
      .then((response) => {
        alert(response.data);  // Ispisujemo odgovor na ekranu
        
      })
      .catch((error) => {
        alert(error);
      });
  }, [location]);

  return (
    
    <div className='main'>
      <Header></Header>
    <div className='card3'>
      <h1>Uspesno ste verifikovali svoj nalog, hvala Vam na registraciji!</h1>
      <h3>Dobrodosli u nasu skolu stranih jezika, veoma nam je drago sto ste izabrali bas nas.</h3>
      
      <Link to="/">
      <img src="./sova.png" width="180px" className={`logo ${isLogoRotated ? 'rotate' : ''}`}/>
      </Link>

    
      
      </div>
      <Footer></Footer>
      </div>
  );
}

export default VerifikacijaComponent;
