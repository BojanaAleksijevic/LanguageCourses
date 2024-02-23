import React, { useEffect, useState } from "react";
import { useNavigate, Link } from 'react-router-dom';
import LoggedHeader from '../LoggedHeader.js';
import Footer from '../Footer.js';
import Header from "../Header.js";
import axios from "axios";

function Profil() {
    const navigate = useNavigate();
    const isLogged = localStorage.getItem('isloged') === 'yes';
    const [kursDostupni, setKursDostupni] = useState([]);
    const [isLoggedIn, setIsLoggedIn] = useState('');

    const token = localStorage.getItem('token');


    useEffect(() => {

        const fetchDostupno = async () => {
            try {
                const response = await axios.get('https://localhost:5001/api/Course/userEnrolled', {
                   
                });
    

                setKursDostupni(response.data);
            } catch (error) {
                console.error("Greška prilikom dohvatanja podataka:", error);
            }

            console.log("price" , kursDostupni.price);
        };

        fetchDostupno();
    }, [isLogged, navigate]);

    if (!isLogged) {
        // If not logged in, the component won't be rendered
        return null;
    }

    return (
        <div className="glavnidivg">

        <div>
            {isLoggedIn ? <LoggedHeader /> : <Header />}
            <div className="lista-kurseva2">
                <div className="kurs-box">
                    <div className="kurs-info">
                        <h2>Moji podaci</h2>
                        <p>Ime: {localStorage.getItem('firstName')}</p>
                        <p>Prezime: {localStorage.getItem('lastName')}</p>
                    </div>

                    
                </div>

                <div>
                    
                </div>
            


            </div>
            <div className="glavnidivg-dm"></div>
            <Footer />
        </div>
        </div>
    );
}

export default Profil;
