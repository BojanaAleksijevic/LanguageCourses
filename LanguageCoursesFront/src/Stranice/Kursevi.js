import React, { useState, useEffect } from "react";
import { useLocation } from 'react-router-dom';
import Header from '../Header.js';
import Footer from '../Footer.js';
import '../Stil.css'; // Dodajte ovaj import za CSS

function Kursevi() {
    const location = useLocation();
    const [pretraga, setPretraga] = useState(new URLSearchParams(location.search).get('pretraga') || "");
    
    const [kursevi, setKursevi] = useState([
        { id: 1, 
            slika: "./britanijaKvadrat.jpg",
            naziv: "Engleski jezik - individualna nastava", 
            predavac: "Ivana Djurdjevic i Jelisaveta Miladinovic", 
            trajanje: "40 casova, po dogovoru" ,
            cena: "40 000 din."
        },
        { id: 2, 
            slika: "./britanijaKvadrat.jpg",
            naziv: "Engleski jezik - grupna nastava", 
            predavac: "Ivana Djurdjevic i Jelisaveta Miladinovic", 
            trajanje: "50 casova, 3x2 casa nedeljno" ,
            cena: "15 000 din."
        },
        { id: 3, 
            slika: "./spanijaKvadrat.png",
            naziv: "Spanski jezik - grupna nastava", 
            predavac: "Milica Cirovic", 
            trajanje: "40 casova, 2x2 casa nedeljno" ,
            cena: "14 000 din."
        },
        { id: 4, 
            slika: "./italijaKvadrat.png",
            naziv: "Italijanski jezik - grupna nastava", 
            predavac: "Milena Radojkovic", 
            trajanje: "40 casova, 2x2 casa nedeljno" ,
            cena: "14 000 din."
        },
        { id: 5, 
            slika: "./turskaKvadrat.png",
            naziv: "Turski jezik - grupna nastava", 
            predavac: "Stefan Jovanovic", 
            trajanje: "40 casova, 2x2 casa nedeljno" ,
            cena: "14 000 din."
        }
        
    ]);

    //const [pretraga, setPretraga] = useState(""); // Dodao stanje za pretragu
    useEffect(() => {
        // Ažuriranje rezultata pretrage kada se promeni pretraga u URL-u
        setPretraga(new URLSearchParams(location.search).get('pretraga') || "");
    }, [location.search]);

    const filtriraniKursevi = kursevi.filter(
        kurs => kurs.naziv.toLowerCase().includes(pretraga.toLowerCase())
    );

    return (
        <div className="glavnidivg">
            <Header />
            
            <h1><i>Sacuvaj</i> za kasnije ili se<i> prijavi</i> odmah, a mi ti saljemo mejl sa vise informacija! </h1>
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
                        <div className="kurs">
                        <img src={kurs.slika} alt={`Zastava za ${kurs.naziv}`} className="zastava" />
                                <div className="kurs-info">
                                <h2>{kurs.naziv}</h2>
                                <p>Predavac: {kurs.predavac}</p>
                                <p>Trajanje: {kurs.trajanje}</p>
                                <p>Cena: {kurs.cena}</p>
                            </div>
                            <button onClick={() => console.log(`Prijavi se na kurs ${kurs.naziv}`)}>
                                Pogledaj detaljnije
                            </button>
                            
                        </div>
                    </div>
                ))}
            </div>

            <Footer />
        </div>
    );
}

export default Kursevi;
