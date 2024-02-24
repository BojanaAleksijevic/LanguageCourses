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

        const fetchMojiKursevi = async () => {
            try {
                const response = await axios.get('https://localhost:5001/api/Course/userEnrolled');
                setKursDostupni(response.data);
            } catch (error) {
                console.error('Error fetching courses:', error);
            }
        };

        fetchMojiKursevi();
    }, []);


    if (!isLogged) {
        // If not logged in, the component won't be rendered
        return null;
    }

    return (
        <div className="glavnidivg">

        <div>
            {isLoggedIn ? <LoggedHeader /> : <Header />}
                <div className="profil-box" style={{ margin:'20px', padding: '10px' }}>
                <h2>Licni podaci</h2>
                    <div className="profil-info">
                        <div className="profil-slika">
                        <img src='' className='profil-slika' alt={`Slika za ${localStorage.getItem('firstName')}`} />
                        </div>

                        <div className="profil-podaci">
                            <p>Ime: {localStorage.getItem('firstName')}</p>
                            <p>Prezime: {localStorage.getItem('lastName')}</p>
                            <p>Broj telefona: {localStorage.getItem('Phone')}</p>
                        </div>
                    </div>

                    
                </div>

                <div style={{ margin:'20px', padding: '10px' }}>
                <h2>Kursevi na koje si prijavljen</h2>

                {kursDostupni.map((kurs) => (
                        <Link to={`/detaljiKursa/${kurs.id}`} className="kurs" key={kurs.id} style={{ width: '25%'}}>
                            <div className="box-sa-strane" style={{ width: '25%', margin:'20px', padding: '10px' }} >
                                <div className="slova">
                                    <p style={{ fontSize: '25px' }}>{kurs.name}</p>
                                    <p>Jezik: {kurs.language}</p>
                                    <p>Nivo: {kurs.level}</p>
                                    <p>Predavac: {kurs.firstName} {kurs.lastName}</p>
                                </div>
                                <div >
                                    <img src={`data:image/jpeg;base64,${kurs.picture}`} className='picture' alt={`Slika za ${kurs.name}`} />
                                </div>
                            </div>
                        </Link>
                    ))}
                </div>
            


            <div className="glavnidivg-dm"></div>
            <Footer />
        </div>
        </div>
    );
}

export default Profil;
