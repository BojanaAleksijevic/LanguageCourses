import React, { useEffect, useState } from "react";
import { useNavigate, Link } from 'react-router-dom';
import LoggedHeader from '../LoggedHeader.js';
import Footer from '../Footer.js';
import Header from "../Header.js";
import axios from "axios";
import { getByDisplayValue } from "@testing-library/react";

function Profil() {
    const navigate = useNavigate();
    const isLogged = localStorage.getItem('isloged') === 'yes';
    const [kursDostupni, setKursDostupni] = useState([]);
    const [userProfile, setUserProfile] = useState({});


    const [newProfileImage, setNewProfileImage] = useState(null);

    const [isLoggedIn, setIsLoggedIn] = useState('');
    const token = localStorage.getItem('token');





    useEffect(() => {

        if (token) {
            setIsLoggedIn(true);
        }




        const fetchMojiKursevi = async () => {
            try {
                const response = await axios.get('https://localhost:5001/api/Course/userEnrolled');
                setKursDostupni(response.data);
            } catch (error) {
                console.error('Error fetching courses:', error);
            }
        };

        const fetchUserProfile = async () => {
            try {
                const response = await axios.get('https://localhost:5001/api/User');
                setUserProfile(response.data);
            } catch (error) {
                console.error('Error fetching user profile:', error);
            }
        };

        fetchMojiKursevi();
        fetchUserProfile();
    }, []);

    useEffect(() => {
        // Dodajte console.log da pratite promene u isLoggedIn
        console.log('isLoggedIn changed:', isLoggedIn);
    }, [isLoggedIn]);


    const handleImageChange = (e) => {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onloadend = () => {
                setNewProfileImage(reader.result);
            };
            reader.readAsDataURL(file);
        }
    };


    const handleDisabled = () => {
        navigate(`/kurseviNedostupni`);
    };

    const handleAvailable = () => {
        navigate(`/kurseviDostupni`);
    };

    const handleAddProfessor= () => {
        navigate(`/dodajProf`);
    };

    const handleUploadImage = async () => {
        try {
            if (newProfileImage) {
                const file = await fetch(newProfileImage);
                const blob = await file.blob();
                const reader = new FileReader();

                reader.onloadend = async () => {
                    const base64Image = reader.result.split(",")[1]; // Dobijanje samo dela koji predstavlja base64
                    await axios.put(`https://localhost:5001/api/User/changePicture`, {
                        userId: userProfile.id,
                        picture: base64Image
                    });
                    // Osvežite korisnički profil kako biste ažurirali novu sliku
                    //fetchUserProfile();
                    // Opciono: Resetujte newProfileImage na null ako želite omogućiti izbor nove slike
                    setNewProfileImage(null);
                };

                reader.readAsDataURL(blob);
            }
        } catch (error) {
            console.error('Error uploading image:', error);
        }
    };



    if (!isLogged) {
        return null;
    }

    return (
        <div className="main-container">
            <div>

                {isLoggedIn ? <LoggedHeader /> : <Header />}
                <div className="profil-container" style={{ margin: '10px', padding: '10px', }}>

                    <div className="profil-info">
                        <div className="profil-slika">
                            <img
                                src={`data:image/jpeg;base64,${userProfile.picture}`}
                                className='profil-slika'
                                alt={`Image for ${userProfile.firstName}`}
                            />
                            <input type="file" accept="image/*" onChange={handleImageChange} />
                            {newProfileImage && (
                                <button onClick={handleUploadImage}>Promeni sliku</button>
                            )}
                        </div>
                        <div className="profil-podaci">
                            <p><b>{userProfile.firstName} {userProfile.lastName}</b></p>
                            <p>Phone: {userProfile.phone}</p>
                            <p>Email: {userProfile.email}</p>
                            <p>Uloga:
                                {userProfile.role === 0
                                    ? " Student"
                                    : userProfile.role === 1
                                        ? " Professor"
                                        : userProfile.role === 2
                                            ? " Admin"
                                            : ""}
                            </p>
                        </div>

                    </div>

                   
                    </div>
                
                    <div className="available">
                        {localStorage.getItem('role') === "2" && (
                            <h1 >Upravljaj kursevima</h1>
                        )}

                        {localStorage.getItem('role') === "1" && (
                            <h1 >Upravljaj kursevima</h1>
                        )}
                        <div>
                        {localStorage.getItem('role') === "1" && (
                            <button onClick={handleDisabled} className='button-prijava2'>Nedostupni</button>
                        )}

                        {localStorage.getItem('role') === "2" && (
                            <button onClick={handleDisabled} className='button-prijava2'>Nedostupni</button>
                        )}


                        {localStorage.getItem('role') === "1"   && (
                            <button onClick={handleAvailable} className='button-prijava2'>Dostupni</button>
                        )}
                    
                        {localStorage.getItem('role') === "2"   && (
                            <button onClick={handleAvailable} className='button-prijava2'>Dostupni</button>
                        )}
                        
                        {localStorage.getItem('role') === "2" && (
                            <h1 >Dodaj profesora</h1>
                        )}
                        {localStorage.getItem('role') === "2"   && (
                            <button onClick={handleAddProfessor} className='button-prijava2'>Dodaj profesora</button>
                        )}
                        </div>
                </div>
                <h2>Kursevi na koje si prijavljen</h2>
                <div className="container2" style={{ margin: '20px', padding: '10px' }}>
                    

                    {kursDostupni.map((kurs) => (
                        <Link to={`/detaljiKursa/${kurs.id}`} className="" key={kurs.id} style={{ width: '25%' }}>
                            <div className="box-sa-strane" style={{ width: '100%', margin: '20px', padding: '10px' }} >
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
            </div>
            <div className="main-container-dm"></div>
            <Footer />
        </div>
    );
}

export default Profil;
