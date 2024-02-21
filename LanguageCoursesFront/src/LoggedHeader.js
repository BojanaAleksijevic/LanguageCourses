
import React, { useState } from 'react';
import { Link } from "react-router-dom"; /* Koristi se link da se ne bi refresovala stranica*/
import axios from 'axios';


const LoggedHeader = () =>{

    const [isLogoRotated, setLogoRotation] = useState(false);

  const handleLogoHover = () => {
    setLogoRotation(true);
  };

  const handleLogoLeave = () => {
    setLogoRotation(false);
  };


  const logoutUser=() =>{
    delete axios.defaults.headers.common["Authorization"];
    localStorage.setItem('token', "");
    localStorage.setItem('email', "");
    localStorage.setItem('id', "");
    localStorage.setItem('firstName', "");
    localStorage.setItem('lastName', "");
    localStorage.setItem('role', "");
    localStorage.setItem('isloged', "no");
    window.location.reload(); // Reload the page
}


    return (
        <header>
            <nav className="nav">
                <img src="../../sova.png" width="120px" className={`logo ${isLogoRotated ? 'rotate' : ''}`}
          onMouseEnter={handleLogoHover}
          onMouseLeave={handleLogoLeave}/>
                
                <ul className="nav-items">
                <Link to="/">
                        <button className='button button1'>
                            Pocetna
                        </button>
                    </Link>
                    
                    
                    <button className="button button1"  onClick={logoutUser}>
                    Odjavi se
                    </button>
                    



                    
                    <Link to="/kursevi">
                        <button className='button button1'>
                            Kursevi
                        </button>
                    </Link>

                    

                    <Link to="/lokacija">
                        <button className='button button1'>
                            Lokacija
                        </button>
                    </Link>

                    <Link to="/profil">
                        <button className='button button1'>
                            Profil
                        </button>
                    </Link>
                   
                </ul>
            </nav>
        </header>
    )
}

export default LoggedHeader;  
