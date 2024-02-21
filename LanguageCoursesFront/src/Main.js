import React from 'react';
import  { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import Kursevi from './Stranice/Kursevi';
import { Routes, Route, Link, BrowserRouter as Router } from 'react-router-dom';
import DetaljiKursa from './Stranice/detaljiKursa';



const Main = () => {
    const navigate = useNavigate();
    const [pretraga, setPretraga] = useState('');
    const [reviews, setReviews] = useState([]);
    const [kursevi, setKursevi] = useState([]);

    useEffect(() => {
        const fetchReviews = async () => {
            try {
                const response = await axios.get('https://localhost:5001/api/Review/first');
                setReviews(response.data);
            } catch (error) {
                console.error('Error fetching reviews:', error);
            }
        };

        const fetchKursevi = async () => {
            try {
                const response = await axios.get('https://localhost:5001/api/Course/availablefirst');
                setKursevi(response.data);
            } catch (error) {
                console.error('Error fetching courses:', error);
            }
        };

        fetchReviews();
        fetchKursevi();
    }, []);



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


    return(
        <div className='naslov'>
            <h1 className='text-naslov'>Dobrodosli u prvu školu stranih jezika u Topoli</h1>
            <h3 className='text-podnaslov'>Nasi posveceni predavaci ce vam pomoci da naucite strani jezik</h3>
            <img src="./naslovnaSlika.jpg" width="100%" />

            <div className='glavnidiv'>
                <div className='main-left'>
                <ul className='main-opis'>
                    <h1>Zasto bas mi?</h1>
                    <h3>Zato sto predstavljamo inicijativu mladih i obrazovanih predavača koji se trude da probude svest o važnosti poznavanja jezika u modernom društvu. 
                        Prioritet nam je da prenesemo svoje znanje na kvalitetan način. 
                        Naš cilj je da sa svakog časa odlazite sa osmehom na licu! </h3>
                    <br></br>

                    <h2>Zasto je vazno uciti strani jezik?</h2>
                    <ul class="prva-lista">
                        <li>Poboljsavate vasu sposobnost izrazavanja, cime se povecava samopouzdanje </li>
                        <li>Postizu se bolji skolski rezultati</li>
                        <li>Povecava vase profesionalne mogućnosti</li>
                        <li>Pruza vam se veca prolaznost prilikom trazenja posla</li>
                        <li>Osecate se sigurnije prilikom odlaska u drugu drzavu</li>
                        <li>Omogućava vam komunikaciju sa ljudima sirom sveta, otvarajuci vrata internacionalnim prilikama.</li>
                    </ul>

                    <h2>Sta cu nauciti na ovom kursu?</h2>
                    <ul class="druga-lista">
                        <li>Izbeci ces pocetnicke greske</li>
                        <li>Razvijaćete citanje, pisanje, slusanje i govor na jezicima.</li>
                        <li>Sticaćete dublje razumevanje gramatickih pravila i prosirivati recnik</li>
                        <li>Usavršavaćete sposobnost efikasne komunikacije u svakodnevnim i poslovnim situacijama</li>
                        <li>Razvijacete sposobnost jasnog izrazavanja misli i stavova</li>
                        <li>Steci cete uvid u kulturu jezika, običaje i drustvo</li>
                        <li>Kroz vezbe i prakticne aktivnosti, povecavacete samopouzdanje u koriscenju engleskog jezika u razlicitim situacijama.</li>
                        <li>I jos mnogo toga</li>
                    </ul>
                    <br></br>
                    
                    
                </ul>
                </div>

                <div className='main-right'>
                <h2>Nasi najtrazeniji kursevi: </h2>
                <ul className='linkovi'>
                    {kursevi.map((kurs) => (
                        <Link to={`/detaljiKursa/${kurs.id}`} className="kurs" key={kurs.id}>
                            <div className="box-sa-strane">
                                <div className="slova">
                                    <p>{kurs.name}</p>
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
                </ul>
            </div>


            
            </div>
            <br></br>
            
            <h1 className='naslov-utisci'>Sta drugi misle o nasim kursevima?</h1>



            <div className="div-utisaka">
                {reviews.map(review => (
                    <div key={review.id} className="box">
                        <div className="slika-osobe">
                            <img src={`data:image/jpg;base64,${review.picture}`} alt={`Slika ${review.id}`} /> 
                        </div>

                        <div class="comment">
                            <p style={{ color: "#00b93b" }}>{review.firstName} {review.lastName}</p>
                            <p>Postavljeno: {formatirajDatum(review.postDate)}</p>
                            <p><i>{review.content}</i></p>
                            <p>Ocena: {zvezdica(review.rating)}</p>
                        </div>
                        
                        
                    </div>
                ))}
            </div>

            <h1 className='naslov-kraj'>Odaberite jezik i pridružite nam se!</h1>
        </div>
    )
}

export default Main;


