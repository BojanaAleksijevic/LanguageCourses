// DetaljiKursa.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';
import '../Stil.css'; 
import Header from '../Header.js';
import Footer from '../Footer.js';
import { useNavigate } from 'react-router-dom';
import LoggedHeader from '../LoggedHeader.js';


const DetaljiKursa = () => {
    const { id } = useParams();
    const [kursDetalji, setKursDetalji] = useState({});

    const [isLoggedIn, setIsLoggedIn] = useState('');

    const token = localStorage.getItem('token');
    const navigate = useNavigate();
   
    const isloged = localStorage.getItem('isloged');


    useEffect(() => {
    
        const fetchKursDetalji = async () => {
            if (token) {
                setIsLoggedIn(true);
              }

            if (isloged !== 'yes') {
                // If not logged in, navigate to the login page
                navigate('/uloguj');
            }
            if (isloged !== 'yes') {
                // If not logged in, the component won't be rendered
                return null;
            }

            try {
                const response = await axios.get(`https://localhost:5001/api/Course/${id}`);
                setKursDetalji(response.data);
            } catch (error) {
                console.error('Error fetching course details:', error);
            }
        };

        fetchKursDetalji(); 
    }, [id,isloged, navigate]);

    // Stil za pozadinu
    const backgroundImageStyle = {
        backgroundImage: `url(data:image/jpeg;base64,${kursDetalji.picture})`,
        backgroundSize: 'cover',
        backgroundPosition: 'center',
        borderRadius: '10px',
        height: '400px', // Prilagodite visinu prema potrebi
        width: '100%', // Prilagodite širinu prema potrebi
        marginBottom: '20px',
    };

    return (
        <div>
              {isLoggedIn ? <LoggedHeader /> : <Header />}
            <div className="detaljan-prikaz-stranica">
                <p className='ime-kursa'>{kursDetalji.name}</p>
                <p className='jezik-kursa'>- {kursDetalji.language} -</p>

                <div className='box-detaljan-prikaz'>
                    <div className='box-detaljan-prikaz-levo' style={backgroundImageStyle}>
                       
                <p className='o-kursu'>O kursu:</p>
                <p className='description'>{kursDetalji.description}</p>
            </div>

            <div className='box-detaljan-prikaz-desno'>

                <p>Nivo: {kursDetalji.level}</p>
                <p>Tip nastave: {kursDetalji.type === 0 ? 'individualna' : 'grupna'}</p>
                <p>Cena: {kursDetalji.price} €</p>
                <p>Trajanje: {kursDetalji.duration} casova</p>

                <div className='box-profesor'>
                    <img src={`data:image/jpeg;base64,${kursDetalji.professorPicture}`} className='slika-profesora' alt={`Slika za ${kursDetalji.professorFirstName}`} />
                    
                    <div className='profesor-info'>
                        <p>Informacije o profesoru</p>
                        <p>🗣️{kursDetalji.professorFirstName} {kursDetalji.professorLastName}</p>
                        <p>📞{kursDetalji.professorPhone}</p>
                        <p>📧{kursDetalji.professorEmail}</p>
                    </div>
                </div>
            </div>
        </div>
        </div>
        
        <button className='button-prijava'>Prijavi se na kurs</button>
        <p className='ime-kursa'>Pogledaj iskustva drugih</p>
            <Link to={`/recenzije/${id}`}>
                <button className='button-recenzije'>
                Pogledaj recenzije
                </button>
            </Link>
        <Footer></Footer>
        </div>
    );
};

export default DetaljiKursa;