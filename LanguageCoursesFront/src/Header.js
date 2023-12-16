
import React from 'react'
import { Link } from "react-router-dom"; /* Koristi se link da se ne bi refresovala stranica*/

const Header = () =>{
    return (
        <header>
            <nav className="nav">
                <img src="./sova.png" width="120px"className='logo'/>
                
                <ul className="nav-items">
                    <Link to="/uloguj">
                        <button className='button button1'>
                            Prijavi se 
                        </button>
                    </Link>

                    <Link to="/">
                        <button className='button button1'>
                            O nama
                        </button>
                    </Link>
                    
                    <Link to="/kursevi">
                        <button className='button button1'>
                            Kursevi
                        </button>
                    </Link>

                    <Link to="/cenovnik">
                        <button className='button button1'>
                            Cenovnik
                        </button>
                    </Link>

                    <Link to="/kontakt">
                        <button className='button button1'>
                            Kontakt
                        </button>
                    </Link>

                    <Link to="/lokacija">
                        <button className='button button1'>
                            Lokacija
                        </button>
                    </Link>
                   
                </ul>
            </nav>
        </header>
    )
}

export default Header;  
