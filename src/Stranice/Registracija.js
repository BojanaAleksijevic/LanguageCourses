// Registracija.js

import React, { useState } from "react";
import Header from '../Header.js';

function Registracija() {
    // State za praćenje unesenih podataka
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    // Funkcija za obradu registracije
    const handleRegistracija = async () => {
        try {
            // Pozivamo backend endpoint za registraciju
            const response = await fetch('http://localhost:3001/registracija', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ email, password }),
            });

            // Ako je registracija uspješna, možete obraditi odgovor ili redirektovati korisnika
            if (response.ok) {
                console.log('Korisnik uspješno registrovan!');
                // Dodajte redirekciju ili druge radnje prema potrebi
            } else {
                console.error('Greška prilikom registracije:', response.statusText);
            }
        } catch (error) {
            console.error('Greška prilikom registracije:', error.message);
        }
    };

    return (
        <div className="glavnidivg">
            <Header />
            <h1>Ukoliko nemaš nalog, registruj se</h1>
            <div>
                <label>Email:</label>
                <input type="text" value={email} onChange={(e) => setEmail(e.target.value)} />
            </div>
            <div>
                <label>Šifra:</label>
                <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
            </div>
            <button onClick={handleRegistracija}>Registruj se</button>
        </div>
    );
}

export default Registracija;
