import React, { useEffect, useState} from 'react';
import { useLocation } from 'react-router-dom';
import { Link } from "react-router-dom";
import axios from 'axios';
import Header from '../Header.js';
import Footer from '../Footer.js'

function Zaboravljena() {
  
    const [isLogoRotated, setLogoRotation] = useState(false);
  
    const handleLogoHover = () => {
      setLogoRotation(true);
    };
  
    const handleLogoLeave = () => {
      setLogoRotation(false);
    };
  
    
    

    return (
      
      <div className='main'>
        <Header></Header>
      <div className='card3'>
        <h1>Uspesno ste promenili lozinku!</h1>
        <h3>Dobrodosli u nasu skolu stranih jezika, veoma nam je drago sto ste izabrali bas nas.</h3>
        
        <Link to="/">
        <img src="./sova.png" width="180px" className={`logo ${isLogoRotated ? 'rotate' : ''}`}/>
        </Link>
  
      
        
        </div>
        <Footer></Footer>
        </div>
    );
  }
  
  export default Zaboravljena;