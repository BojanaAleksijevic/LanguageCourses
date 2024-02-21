import React from "react";
import LoggedHeader from '../LoggedHeader.js';
import Footer from '../Footer.js';

function Profil(){
    return(
        <div className="glavnidivg">
            <LoggedHeader></LoggedHeader>
            <h1>kursevi na koje je prijavljen korisnik</h1>
            <Footer></Footer>
        </div>
    )
}

export default Profil;