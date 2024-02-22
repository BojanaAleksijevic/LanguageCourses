import React, { useState, useEffect } from "react";
import axios from 'axios';
import { useLocation, Link } from 'react-router-dom';
import Header from '../Header.js';
import Footer from '../Footer.js';
import '../Stil.css';
import LoggedHeader from "../LoggedHeader.js";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router-dom";


function Kursevi() {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const { id } = useParams();
    const navigate = useNavigate();
    const isloged = localStorage.getItem('isloged');
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
        kurs => kurs.name.toLowerCase().includes(pretraga.toLowerCase()) ||
         kurs.language.toLowerCase().includes(pretraga.toLowerCase()) ||
        kurs.firstName.toLowerCase().includes(pretraga.toLowerCase())
    ) : [];


   


    return (
        <div className="glavnidivg">
            {isLoggedIn ? <LoggedHeader /> : <Header />}

            <h1>Pogledaj kurseve </h1>
          

        <div className="group">
        <svg className="icon" aria-hidden="true" viewBox="0 0 24 24">
          <g>
            <path d="M21.53 20.47l-3.66-3.66C19.195 15.24 20 13.214 20 11c0-4.97-4.03-9-9-9s-9 4.03-9 9 4.03 9 9 9c2.215 0 4.24-.804 5.808-2.13l3.66 3.66c.147.146.34.22.53.22s.385-.073.53-.22c.295-.293.295-.767.002-1.06zM3.5 11c0-4.135 3.365-7.5 7.5-7.5s7.5 3.365 7.5 7.5-3.365 7.5-7.5 7.5-7.5-3.365-7.5-7.5z"></path>
          </g>
        </svg>
        <input
          placeholder="Pretraži kurseve"
          type="search"
          className="input"
          value={pretraga}
          onChange={(e) => setPretraga(e.target.value)}
        />
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