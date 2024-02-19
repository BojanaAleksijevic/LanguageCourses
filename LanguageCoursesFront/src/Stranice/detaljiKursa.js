// DetaljiKursa.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import '../Stil.css'; 
import Header from '../Header.js';
import Footer from '../Footer.js';


const DetaljiKursa = () => {
    const { id } = useParams();
    const [kursDetalji, setKursDetalji] = useState({});

    useEffect(() => {
        const fetchKursDetalji = async () => {
            try {
                const response = await axios.get(`https://localhost:5001/api/Course/${id}`);
                setKursDetalji(response.data);
            } catch (error) {
                console.error('Error fetching course details:', error);
            }
        };

        fetchKursDetalji(); 
    }, [id]);

    return (
        <div>
        <Header></Header>

        <div className="detaljan-prikaz-stranica">
        <p className='ime-kursa'>{kursDetalji.name}</p>
        <p className='jezik-kursa'>- {kursDetalji.language} -</p>

        <div className='box-detaljan-prikaz'>
            <div className='box-detaljan-prikaz-levo'>
            
                <p>O kursu:</p>
                <p>{kursDetalji.description}</p>
                <br></br>
                <button className='button-prijava'>Prijavi se na kurs</button>
            </div>

            <div className='box-detaljan-prikaz-desno'>
                <img src={kursDetalji.picture} className='slika-kursa' alt={`Slika za ${kursDetalji.name}`} />

                <p>Nivo: {kursDetalji.level}</p>
                <p>Tip nastave: {kursDetalji.type}</p>
                <p>Cena: {kursDetalji.price} din.</p>
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
        <Footer></Footer>
        </div>
    );
};

export default DetaljiKursa;