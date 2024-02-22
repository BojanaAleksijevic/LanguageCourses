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

        console.log(localStorage.getItem('firstname'));
        console.log(localStorage.getItem('lastname'));
        console.log(localStorage.getItem('email'));
        console.log(localStorage.getItem('token'));
        console.log(localStorage.getItem('role'));

    }, [isloged, navigate]);

    if (isloged !== 'yes') {
        // If not logged in, the component won't be rendered
        return null;
    }

    return (
        <div className="glavnidivg" >
            <LoggedHeader></LoggedHeader>
            
            


    <div className="lista-kurseva2">
    <div className="kurs-box">
        <div className="kurs-info">
            <h2>Moji podaci</h2>
            <p>Ime: {localStorage.getItem('firstName')}</p>
            <p>Prezime: {localStorage.getItem('lastName')}</p>
            
        </div>
    </div>
    </div>

            
            
            <div className="glavnidivg-dm">          
                  
            </div>
            <Footer></Footer>
        </div>
    );
}

export default Profil;
