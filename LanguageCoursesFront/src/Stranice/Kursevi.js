import React, { useState, useEffect } from "react";
import axios from 'axios';
import { useLocation, Link } from 'react-router-dom';
import Header from '../Header.js';
import Footer from '../Footer.js';
import '../Stil.css';
import LoggedHeader from "../LoggedHeader.js";


function Kursevi() {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);

    const [pretraga, setPretraga] = useState(queryParams.get('pretraga') || "");
    const [kursevi, setKursevi] = useState([]);

    const [isLoggedIn, setIsLoggedIn] = useState('');

    const token = localStorage.getItem('token');

    useEffect(() => {
        if (token) {
            setIsLoggedIn(true);
        }
    }, []);



    const fetchData = async () => {
        try {
            const response = await axios.get('https://localhost:5001/api/Course/available', {
                params: {
                    pretraga,
                    language: queryParams.get('language'),
                    level: queryParams.get('level'),
                    priceFrom: queryParams.get('priceFrom'),
                    priceTo: queryParams.get('priceTo'),
                    pageNumber: queryParams.get('pageNumber') || 1,  // Postavljanje default vrednosti na 1
                    pageSize: queryParams.get('pageSize') || 10,  // Postavljanje default vrednosti na 16
                }
            });

            setKursevi(response.data);
        } catch (error) {
            console.error("Greška prilikom dohvatanja podataka:", error);
        }
    };

    useEffect(() => {
        fetchData();
    }, [location.search, pretraga]);

    const filtriraniKursevi = Array.isArray(kursevi) ? kursevi.filter(
        kurs => kurs.name.toLowerCase().includes(pretraga.toLowerCase())
    ) : [];



    return (
        <div className="glavnidivg">
            {isLoggedIn ? <LoggedHeader /> : <Header />}

            <h1>Pogledaj kurseve </h1>
            <div className="pretrage">
                {/* Ostatak koda ostaje nepromenjen /}
                {/ ... */}
            </div>




            <div className="lista-kurseva">
                {filtriraniKursevi.map(kurs => (
                    <div key={kurs.id} className="kurs-box">
                        <Link to={`/detaljiKursa/${kurs.id}`} className="kurs">
                            <img src={`data:image/jpeg;base64,${kurs.picture}`} alt={`Zastava za ${kurs.language}`} className="zastava" />
                            <div className="kurs-info">
                                <h2>{kurs.name}</h2>
                                <p>Jezik: {kurs.language}</p>
                                <p>Nivo: {kurs.level}</p>
                                <p>Predavac: {kurs.firstName} {kurs.lastName}</p>
                                <p>Tip nastave: {kurs.type === 0 ? 'individualna' : 'grupna'}</p>
                                <p>Cena: {kurs.price}  €</p>
                                
                                
                                {localStorage.getItem('role') === "2" /*|| localStorage.getItem('id') === kurs.professorId*/ && (
                                    <button>Obriši</button>
                                )}


                            </div>
                        </Link>
                    </div>
                ))}
            </div>

            <Footer />
        </div>
    );
}

export default Kursevi;