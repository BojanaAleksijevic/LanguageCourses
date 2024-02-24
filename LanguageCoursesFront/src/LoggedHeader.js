
import React, { useState } from 'react';
import { Link } from "react-router-dom"; /* Koristi se link da se ne bi refresovala stranica*/
import axios from 'axios';
import { useNavigate } from 'react-router-dom';




const LoggedHeader = () =>{

    const [isLogoRotated, setLogoRotation] = useState(false);

  const handleLogoHover = () => {
    setLogoRotation(true);
  };

  const handleLogoLeave = () => {
    setLogoRotation(false);
  };

  const navigate = useNavigate();


  const logoutUser=() =>{
    delete axios.defaults.headers.common["Authorization"];
    localStorage.setItem('token', "");
    localStorage.setItem('email', "");
    localStorage.setItem('id', "");
    localStorage.setItem('firstName', "");
    localStorage.setItem('lastName', "");
    localStorage.setItem('role', "");
    localStorage.setItem('isloged', "no");
    
    navigate('/');
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

                    {localStorage.getItem('role') === "0" /*|| localStorage.getItem('id') === kurs.professorId*/ && (
                    <Link to="/profil">
                        <button className='button button1'>
                            Profil
                        </button>
                    </Link>
                    )}


                    {localStorage.getItem('role') === "1" && (
                    <Link to="/dodajKurs">
                        <button className='button button1' style={{width: '130px'}}>
                            Dodaj Kurs
                        </button>
                    </Link>
                    )}

                    {localStorage.getItem('role') === "2" && (
                    <Link to="/dodajKurs">
                        <button className='button button1' style={{width: '130px'}}>
                            Dodaj Kurs
                        </button>
                    </Link>
                    )}

                    <button className="button button1odjava"  onClick={logoutUser}>
                        Odjavi se
                    </button>

                </ul>
            </nav>
        </header>
    )
}

export default LoggedHeader;  
