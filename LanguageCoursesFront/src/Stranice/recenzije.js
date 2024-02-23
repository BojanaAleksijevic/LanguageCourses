import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import '../Stil.css'; 
import Header from '../Header.js';
import Footer from '../Footer.js';
import LoggedHeader from '../LoggedHeader.js';
import { useNavigate } from 'react-router-dom';

const Recenzije = () => {
    const { id, reviewId } = useParams(); // Dohvati oba parametra
    const [recenzije, setRecenzije] = useState([]);

    const [isLoggedIn, setIsLoggedIn] = useState('');

    const token = localStorage.getItem('token');
    const navigate = useNavigate();
   
    const isloged = localStorage.getItem('isloged');
    



    useEffect(() => {
        const fetchRecenzije = async () => {
        
            if (token) {
                setIsLoggedIn(true);
              }
            
             


            try {
                // Ažuriranje URL-a da uključuje oba parametra
                const response = await axios.get(`https://localhost:5001/api/Review/courseReviews/id:Guid?id=${id}`);
                setRecenzije(response.data);
            } catch (error) {
                console.error('Error fetching reviews:', error);
            }
        };

        fetchRecenzije();
    }, [id, reviewId, isloged, navigate]); // Dodaj oba parametra kao zavisnosti


    

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



    return (
        <div>
            {isLoggedIn ? <LoggedHeader /> : <Header />}

            <div className="recenzije">
                <h2>Recenzije</h2>

                {recenzije.map((recenzija, index) => (
                    <div key={index} className="box">
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
            <Footer />
        </div>
    );
};

export default Recenzije;