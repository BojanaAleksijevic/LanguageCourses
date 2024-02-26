import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Header from './Header.js';
import Footer from './Footer.js';
import Main from './Main.js';
import Uloguj from './Stranice/Uloguj.js';
import Kursevi from './Stranice/Kursevi.js';
import Lokacija from './Stranice/Lokacija.js';
import Profil from './Stranice/Profil.js';
import Registracija from './Stranice/Registracija.js';
import Verifikacija from './Stranice/Verifikacija.js';
import Zaboravljena from './Stranice/Zaboravljena.js';
import Zaboravio from './Stranice/Zaboravio.js';
import Kursevi2 from './Stranice/Kursevi2.js';
import DetaljiKursa from './Stranice/detaljiKursa.js';
import LoggedHeader from './LoggedHeader.js';
import Recenzije from './Stranice/recenzije.js';
import axios from 'axios';
import DodajReview from './Stranice/DodajReview.js';
import DodajKurs from './Stranice/DodajKurs.js';
import IzmeniKurs from './Stranice/IzmeniKurs.js';
import ProfilAdmin from './Stranice/ProfilAdmin.js';
import KurseviNedostupni from './Stranice/KurseviNedostupni.js';
import KurseviDostupni from './Stranice/KurseviDostupni.js';
import DodajProf from './Stranice/DodajProf.js';




import ProtectedRutaAdmin from './autentifikacija/ProtectedRutaAdmin.js';
import ProtectedRutaUser from './autentifikacija/ProtectedRutaUser.js';

function App() {
 
  const isloged = localStorage.getItem('isloged');
  console.log(isloged);
  const token = localStorage.getItem('token');

  useEffect(()=>{
    if (token) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    }
  },[]) // This empty dependency array ensures that the effect runs only once after the initial render

  function Alllayout() {
    return (
      <>
        {isloged=='yes' ? <LoggedHeader /> : <Header />}
        <Main />
        <Footer />
      </>
    );
  }
  

  return (
    <Router>
      <Routes>
        <Route path="/" element={<Alllayout />} />

        <Route path="uloguj" element={<Uloguj />} />
        <Route path="registruj" element={<Registracija />} />
        <Route path="lokacija" element={<Lokacija />} />
        <Route path="kursevi" element={<Kursevi />} />
        <Route path="profil" element={<Profil />} />
        <Route path="verifikacija" element={<Verifikacija />} />
        <Route path="zaboravljena" element={<Zaboravljena />} />
        <Route path="zaboravio" element={<Zaboravio />} />
        <Route path="detaljiKursa/:id" element={<DetaljiKursa />} />
        <Route path="recenzije/:id" element={<Recenzije />} />
        <Route path="dodajreview" element={<DodajReview />} />
        <Route path="dodajKurs" element={<DodajKurs />} />
        <Route path="izmeniKurs/:id" element={<IzmeniKurs />} />
        <Route path="profilAdmin" element={<ProfilAdmin />} />
        <Route path="kurseviNedostupni" element={<KurseviNedostupni />} />
        <Route path="kurseviDostupni" element={<KurseviDostupni />} />
        <Route path="dodajProf" element={<DodajProf />} />
       
       
      </Routes>
    </Router>
  );
}

export default App;