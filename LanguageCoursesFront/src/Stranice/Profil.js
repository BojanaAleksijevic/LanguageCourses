import React, { useEffect } from "react";
import { useNavigate } from 'react-router-dom';
import LoggedHeader from '../LoggedHeader.js';
import Footer from '../Footer.js';

function Profil() {
    const navigate = useNavigate();
    const isloged = localStorage.getItem('isloged');

    useEffect(() => {
        if (isloged !== 'yes') {
            // If not logged in, navigate to the login page
            navigate('/uloguj');
        }
    }, [isloged, navigate]);

    if (isloged !== 'yes') {
        // If not logged in, the component won't be rendered
        return null;
    }

    return (
        <div className="glavnidivg">
            <LoggedHeader></LoggedHeader>
            <h1>kursevi na koje je prijavljen korisnik</h1>
            <Footer></Footer>
        </div>
    );
}

export default Profil;
