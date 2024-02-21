import React from "react";
import Header from '../Header.js';
import Footer from '../Footer.js';
import LoggedHeader from "../LoggedHeader.js";
import  { useState, useEffect } from 'react';

function Lokacija(){
    const [isLoggedIn, setIsLoggedIn] = useState('');

  const token = localStorage.getItem('token');

  useEffect(() => {
    if (token) {
      setIsLoggedIn(true);
    }
  }, []);
    
    
    return(
        <div className="glavnidivg">
             {isLoggedIn ? <LoggedHeader /> : <Header />}
            <h1 align="center"> 📍Bulevar vožda Karađorđa 59, TOPOLA</h1>
            <div className="lokacija">      
            <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d178.5978274137721!2d20.681380004014397!3d44.257189724672386!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47574b8f20986185%3A0x136890f5f0dfbc6a!2sHappy%20Kids%20Center!5e0!3m2!1ssr!2srs!4v1706109770033!5m2!1ssr!2srs"
             width="600"
            height="450"
            style={{border:0}} 
            allowfullscreen="" 
            loading="lazy" 
            referrerpolicy="no-referrer-when-downgrade"></iframe>
                </div>
            <Footer></Footer>
          
        </div>
        

    )
}

export default Lokacija;