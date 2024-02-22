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
    const [recenzije, setRecenzije] = useState([]);

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


        const fetchRecenzije = async () => {
        
            if (token) {
                setIsLoggedIn(true);
              }
            
              if (isloged !== 'yes') {
                // If not logged in, navigate to the login page
                navigate('/uloguj');
            }


            try {
                // Ažuriranje URL-a da uključuje oba parametra
                const response = await axios.get(`https://localhost:5001/api/Review/courseReviews/id:Guid?id=${id}`);
                setRecenzije(response.data);
            } catch (error) {
                console.error('Error fetching reviews:', error);
            }
        };

        fetchKursDetalji(); 
        fetchRecenzije();

    }, [id, isloged, navigate]);
/*
    const handleSubmit = () => {
        if (validateForm()) {
          const url = `https://localhost:5001/api/Course/enrolment/${courseId}`;
    
          axios.post(url)
            .then((response) => {
              alert(response.data); 
            })
            .catch((error) => {
              alert(error);
            });
        }
      };*/

    // Funkcija za formatiranje datuma
    const formatirajDatum = (originalniDatum) => {
        const datum = new Date(originalniDatum);
        return datum.toLocaleDateString('en-US'); 
    };


    // Funkcija za mapiranje broja ocene na simbole
    const zvezdica = (ocena) => {
        const simbol = '⭐'; 
        return simbol.repeat(ocena);
    };

    const handleDelete = async () => {
        try {
            await axios.delete(`https://localhost:5001/api/Course/deleteCourse/${id}`);
            // Redirect or handle the UI update after successful deletion
            navigate('/'); // Redirect to the home page, for example
        } catch (error) {
            console.error('Error deleting course:', error);
            // Handle the error, show a message, etc.
        }
    };
  
    return (
        <div>
              {isLoggedIn ? <LoggedHeader /> : <Header />}
            <div className="detaljan-prikaz-stranica">
                <p className='ime-kursa'>{kursDetalji.name}</p>
                <p className='jezik-kursa'>- {kursDetalji.language} -</p>
                <div className='box-detaljan-prikaz'>
                    <div className='box-detaljan-prikaz-levo'>
                      
                <p className='o-kursu'>O kursu:</p>
                <p className='description'>{kursDetalji.description}</p>
                <img src={`data:image/jpeg;base64,${kursDetalji.picture}`} className='slika-kursa'></img>

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
                {localStorage.getItem('role') === "0" /*|| localStorage.getItem('id') === kurs.professorId*/ && (
                <button /*onClick={handleSubmit}*/ className='button-prijava'>Prijavi se na kurs</button>
                )}


                {localStorage.getItem('role') === "2" /*|| localStorage.getItem('id') === kurs.professorId*/ && (
             <button className='button-prijava' onClick={handleDelete}>Obriši</button>
            )}            

            
            </div>
        </div>
        </div>


            <center>
            <h2>Pogledaj sta su drugi rekli o ovom kursu</h2>
            </center>
            <div className="recenzije">

                {recenzije.map((recenzija, index) => (
                    <div key={index} className="recenzija-box">
                        <div className="slika-osobe">
                            <img src={`data:image/jpg;base64,${recenzija.picture}`} alt={`Slika ${recenzija.id}`} /> 
                        </div>

                        <div class="comment">
                            <p style={{ color: "#00b93b" }}>{recenzija.firstName} {recenzija.lastName}</p>
                            <p>Postavljeno: {formatirajDatum(recenzija.postDate)}</p>
                            <p><i>{recenzija.content}</i></p>
                            <p>Ocena: {zvezdica(recenzija.rating)}</p>
                        </div>
                    </div>
                ))}


            </div>


            <button /*onClick={handleSubmit}*/ className='button-dodaj-recenziju'>Dodaj recenziju</button>
           
            
        <Footer></Footer>
        </div>
    
    );
};

export default DetaljiKursa;