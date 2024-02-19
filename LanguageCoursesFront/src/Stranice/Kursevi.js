import React, { useState, useEffect } from "react";
import axios from 'axios';
import { useLocation, Link, useParams } from 'react-router-dom';
import Header from '../Header.js';
import Footer from '../Footer.js';
import '../Stil.css'; 

function Kursevi() {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);

    const [pretraga, setPretraga] = useState(queryParams.get('pretraga') || "");
    const [kursevi, setKursevi] = useState([]);

    const fetchData = async () => {
        try {
            const response = await axios.get(`https://localhost:5001/api/Course/available`, {
                params: {
                    pretraga,
                    language: queryParams.get('language'),
                    level: queryParams.get('level'),
                    priceFrom: queryParams.get('priceFrom'),
                    priceTo: queryParams.get('priceTo'),
                    pageNumber: queryParams.get('pageNumber'),
                    pageSize: queryParams.get('pageSize')
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
            <Header />
            
            <h1><i>Sacuvaj</i> za kasnije ili se<i> prijavi</i> odmah, a mi ti saljemo mejl sa vise informacija! </h1>
            <div className="pretrage">
                <div className="search-group">
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

                <div className="dropdown">
                    <button className="dropbtn">Jezik</button>
                    <div className="dropdown-content">
                        <a href="#">Engleski</a>
                        <a href="#">Italijanski</a>
                        <a href="#">Francuski</a>
                        <a href="#">Spanski</a>
                        <a href="#">Turski</a>
                        <a href="#">Ruski</a>
                    </div>
                </div>

                <div className="dropdown">
                    <button className="dropbtn">Nivo</button>
                    <div className="dropdown-content">
                        <a href="#">A1</a>
                        <a href="#">A2</a>
                        <a href="#">B1</a>
                        <a href="#">B2</a>
                        <a href="#">C1</a>
                        <a href="#">C2</a>
                    </div>
                </div>

                <div className="input-cena">
                    <input id="cenaOd" className="cena-input" placeholder="Unesite cenu od" />
                </div>

                <div className="input-cena">
                    <input id="cenaDo" className="cena-input" placeholder="Unesite cenu do" />
                </div>

                <div className="primena-filtera">
                    <button className="primena-btn" onClick={fetchData}>
                        Primeni filtere
                    </button>
                </div>
            </div>

            <div className="lista-kurseva">
                {filtriraniKursevi.map(kurs => (
                    <div key={kurs.id} className="kurs-box">
                        <Link to={`/detaljiKursa/${kurs.id}`} className="kurs">
                            <img src={kurs.picture} alt={`Zastava za ${kurs.name}`} className="zastava" />
                            <div className="kurs-info">
                                <h2>{kurs.name}</h2>
                                <p>Jezik: {kurs.language}</p>
                                <p>Nivo: {kurs.level}</p>
                                <p>Predavac: {kurs.firstName} {kurs.lastName}</p>
                                <p>Tip nastave: {kurs.type}</p>
                                <p>Cena: {kurs.price} din.</p>
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
