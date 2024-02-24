
import React, { useState } from 'react';
import { Link } from "react-router-dom"; /* Koristi se link da se ne bi refresovala stranica*/

const Header = () =>{

    const [isLogoRotated, setLogoRotation] = useState(false);

  const handleLogoHover = () => {
    setLogoRotation(true);
  };

  const handleLogoLeave = () => {
    setLogoRotation(false);
  };


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

                 


                    <Link to="/uloguj">
                        <button className='button button1prijava'>
                            Prijava
                        </button>
                    </Link>

                    <Link to="/registruj">
                        <button className='button button1registracija'>
                           Registracija
                        </button>
                    </Link>
                  
                   
                </ul>
            </nav>
        </header>
    )
}

export default Header;  
