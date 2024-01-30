import React from 'react';
import  { useState, useEffect } from 'react';
// dodato 
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import Kursevi from './Stranice/Kursevi';


const Main = () => {
    const navigate = useNavigate();
    const [pretraga, setPretraga] = useState('');
    const [reviews, setReviews] = useState([]);


    useEffect(() => {
        const fetchReviews = async () => {
            try {
                const response = await axios.get('https://localhost:5001/api/Review/first');
                setReviews(response.data);
            } catch (error) {
                console.error('Error fetching reviews:', error);
            }
        };

        fetchReviews();
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


    const pretraziKurs = (jezik) => {
        setPretraga(jezik);
        // Navigacija do stranice kurseva sa pretragom za odabrani jezik
        navigate(`/kursevi?pretraga=${encodeURIComponent(jezik)}`);
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
                    <div class="box-sa-strane">
                        <div className="tekst">
                            <li><a onClick={() => pretraziKurs('engleski')}>Engleski jezik - prof. Ivana Djurdjevic i prof. Jelisaveta Miladinovic</a></li>
                        </div>
                        <div className="slika">
                            <img src="./britanija3.png" className='okrugla-slika' />
                        </div>
                    </div>                        
                    <div class="box-sa-strane">
                        <div className="tekst">
                            <li><a onClick={() => pretraziKurs('spanski')}>Spanski jezik - prof. Milica Cirovic</a></li>
                        </div>
                        <div className="slika">
                            <img src="./spanija3.png" className='okrugla-slika'/>
                        </div>
                    </div>
                    
                    <div class="box-sa-strane">
                        <div className="tekst">
                            <li><a onClick={() => pretraziKurs('italijanski')}>Italijanski jezik - prof. Milena Radojkovic</a></li>
                        </div>
                        <div className="slika">
                            <img src="./italija3.png" className='okrugla-slika'/>
                        </div>
                    </div>
                    <div class="box-sa-strane">
                        <div className="tekst">
                            <li><a onClick={() => pretraziKurs('turski')}>Turski jezik - prof. Stefan Jovanovic</a></li>
                        </div>
                        <div className="slika">
                            <img src="./turska3.png" className='okrugla-slika'/>
                        </div>
                    </div>
                    </ul>
                    
                </div>


            
            </div>
            <br></br>
            
            <h1 className='naslov-utisci'>Sta drugi misle o nasim kursevima?</h1>



            <div className="div-utisaka">
                {reviews.map(review => (
                    <div key={review.id} className="box">
                        <div class="slika-osobe">
                            <img src={review.picture} alt={`Slika ${review.id}`} /> 
                        </div>
                        <div class="comment">
                            <p><i>{review.content}</i></p>
                            <p style={{ color: "#00b93b" }}>{review.firstName} {review.lastName}</p>
                            <p>Postavljeno: {formatirajDatum(review.postDate)}</p>
                            
                            <p>Ocena: {zvezdica(review.rating)}</p>
                        </div>
                        
                        
                    </div>
                ))}
            </div>

{/*

            <div class="div-utisaka">
                <div class="box">
                    <div class="slika-osobe">
                        <img src="./novak.jpg" alt="Slika 1"></img>
                    </div>
                    <div class="comment">
                        <p>Ovaj kurs mi je pomogao da savladam kineski jezik! Zbog svog posla volim da ispostujem svaku zemlju, pa tako i novinarsko pitanje na bilo kom jeziku, pa sam  tako resio da naucim poneku rec na kineskom jeziku. Postepeno samm krenuo, ali uz pomoc divnih nastavnika na ovom kursu sam uspeo i moj kineski je cuo ceo svet!</p>
                        <p style={{ color: "#00b93b" }}>Novak Djokovic, sportista</p>
                    </div>
                </div>
                <div class="box">
                    <div class="slika-osobe">
                        <img src="./dete.jpg" alt="Slika 1"></img>
                    </div>
                    <div class="comment">
                        <p>Ja vise nemam problem sa stranim jezicima u skoli. Pored toga sto dobijam petice na svakom test, znam i mnogo vise od toga, toliko da me i drugari stalno zovu da im pomognem. Zahvalna sam divnim uciteljima na ovom kursu koji su ucenje ucinili zabavnim. Kurs je prilagodjen svim uzrastima. Ja sam svoje neznanje savladala kroz igru, pesmu, interaktivne aktivnosti.</p>
                        <p style={{ color: "#00b93b" }}>Jana Jovanovic, ucenik</p>
                    </div>
                </div>
                
                <div class="box">
                    <div class="slika-osobe">
                        <img src="./lea.jpg" alt="Slika 1"></img>
                    </div>
                    <div class="comment">
                        <p>Ja ovu skolu stranih jezika posecujem godinama! S obzirom da sam isla u srednju skolu u kojoj je nastava bila na engleskom, morala sam perfektno da ga znam. Na ovom kursu sam to i uspela. Pored engleskog, posecivala sam i casove italijanskog i francuskog jezika, koji su mi takodje bili potrebni. Sada, s obzirom da dosta putujem i radim, neophodno mi je znanje jezika. Volim da ucim, a divni ljudi sa ovog kursa su to da mi to olaksaju.</p>
                        <p style={{ color: "#00b93b" }}>Lea Stankovic, influeser</p>
                    </div>
                </div>

        
            </div>
    */}
            <h1 className='naslov-kraj'>Odaberite jezik i pridružite nam se!</h1>
        </div>
    )
}

export default Main;


