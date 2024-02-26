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

    const [selectedLanguage, setSelectedLanguage] = useState("");
    const [selectedLevel, setSelectedLevel] = useState("");

    //pagin
    const [currentPage, setCurrentPage] = useState(Number(queryParams.get('pageNumber')) || 1);
    const [totalPages, setTotalPages] = useState(0);

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
              language: selectedLanguage,  // Promenjeno sa queryParams.get('language') na selectedLanguage
              level: selectedLevel,        // Promenjeno sa queryParams.get('level') na selectedLevel
              priceFrom: document.getElementById('cenaOd').value,  // Dobavljanje vrednosti od input polja
              priceTo: document.getElementById('cenaDo').value,    // Dobavljanje vrednosti od input polja
              pageNumber: currentPage || 1,
              pageSize: queryParams.get('pageSize') || 8,
            }
          });
      
          setKursevi(response.data);
        } catch (error) {
          console.error("Greška prilikom dohvatanja podataka:", error);
        }
      };
      
      const handleApplyFilters = () => {
        fetchData();
      /*
        // Opciono: Očisti polja pretrage
        setPretraga("");
        setSelectedLanguage("");
        setSelectedLevel("");
        document.getElementById('cenaOd').value = "";
        document.getElementById('cenaDo').value = "";*/
      };
      

    useEffect(() => {
        fetchData();
    }, [location.search, pretraga]);

    const filtriraniKursevi = Array.isArray(kursevi) ? kursevi.filter(
        kurs => kurs.name.toLowerCase().includes(pretraga.toLowerCase()) ||
         kurs.language.toLowerCase().includes(pretraga.toLowerCase()) ||
        kurs.firstName.toLowerCase().includes(pretraga.toLowerCase())
    ) : [];



    const handleChangePage = (direction) => {
        const newPage = direction === 'next' ? currentPage + 1 : currentPage - 1;

        const queryParams = new URLSearchParams(location.search);
        queryParams.set('pageNumber', newPage);
        navigate(`?${queryParams.toString()}`);

        setCurrentPage(newPage);
    };
  


    const handleLanguageChange = (language) => {
        setSelectedLanguage(language);
      };
    
      const handleLevelChange = (level) => {
        setSelectedLevel(level);
      };

    return (
        <div className="glavnidivg">
            {isLoggedIn ? <LoggedHeader /> : <Header />}

            <h1>Pogledaj dostupne kurseve </h1>
          
            <div className="pretrage">
                


        <div className="custom-dropdown">
        <label htmlFor="language" className="dropdown-label"></label>
        <select
            id="language"
            className="dropdown-select"
            value={selectedLanguage}
            onChange={(e) => handleLanguageChange(e.target.value)}
        >
            <option value="">Izaberi jezik</option>
            <option value="Engleski">Engleski</option>
            <option value="Italijanski">Italijanski</option>
            <option value="Francuski">Francuski</option>
            <option value="Spanski">Spanski</option>
            <option value="Turski">Turski</option>
            <option value="Ruski">Ruski</option>
        </select>
        </div>

        <div className="custom-dropdown">
        <label htmlFor="level" className="dropdown-label"></label>
        <select
            id="level"
            className="dropdown-select"
            value={selectedLevel}
            onChange={(e) => handleLevelChange(e.target.value)}
        >
            <option value="">Izaberi nivo</option>
            
            <option value="Početni">Početni</option>
            <option value="Osnovni">Osnovni</option>
            <option value="Srednji">Srednji</option>
            <option value="Intermedijski">Intermedijski</option>
            <option value="Napredni">Napredni</option>
        </select>
        </div>


                <div className="input-cena">
                    <input id="cenaOd" className="cena-input" placeholder="Unesite cenu od" />
                </div>

                <div className="input-cena">
                    <input id="cenaDo" className="cena-input" placeholder="Unesite cenu do" />
                </div>

                <div className="primena-filtera">
                <button className="primena-btn" onClick={handleApplyFilters}>
                    Primeni filtere
                </button>

                </div>
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


            <div className="pagination">
                <button
                    onClick={() => handleChangePage('previous')}
                    disabled={currentPage === 1}
                >
                    Prethodna
                </button>
                <button
                    onClick={() => handleChangePage('next')}
                    disabled={currentPage === totalPages}
                    >
                        Sledeća
                    </button>
                </div>
    
    
                <Footer />
            </div>
        );
    }
    
    export default Kursevi;