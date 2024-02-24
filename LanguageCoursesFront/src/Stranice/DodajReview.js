import React, { useEffect, useState } from "react";
import { useNavigate, useLocation } from 'react-router-dom';
import axios from 'axios';
import LoggedHeader from '../LoggedHeader.js';
import Footer from '../Footer.js';

function DodajReview() {
  const [rating, setRating] = useState(0);
  const [content, setContent] = useState("");
  const { search } = useLocation();
  const queryParams = new URLSearchParams(search);
  const courseId = queryParams.get('id');

  const [isSubmitting, setIsSubmitting] = useState(false);
  const [submissionError, setSubmissionError] = useState(null);

  const navigate = useNavigate();
  const isloged = localStorage.getItem('isloged');


  

  useEffect(() => {
    if (isloged !== 'yes') {
      // If not logged in, navigate to the login page
      navigate('/uloguj');
    }
  }, [isloged, navigate]);

  const handleSubmit = () => {
    // Provera da li su polja ocene i komentara popunjena
    if (!rating || !content) {
      setSubmissionError("Ocena i komentar su obavezni.");
      return;
    }

    setIsSubmitting(true);

    console.log("Podaci za slanje:", { courseId, rating, content });

    axios.post("https://localhost:5001/api/Review/addReview", {
        courseId,
        rating,
        content,
    })
      .then((response) => {
        console.log("Odgovor od servera:", response.data);

        // Navigacija nazad na prethodnu stranicu
        navigate(-1);
      })
      .catch((error) => {
        console.error("Greška prilikom podnošenja:", error);
        if (error.response) {
          // Ako je odgovor od servera sa statusom izvan opsega 2xx, koristite poruku od servera
          setSubmissionError(error.response.data.message || "Došlo je do greške prilikom podnošenja.");
        } else if (error.request) {
          // Ako nije primljen odgovor od servera
          setSubmissionError("Nije primljen odgovor od servera.");
        } else {
          // Ako se neka druga greška dogodi tokom postavljanja zahteva
          setSubmissionError("Došlo je do greške prilikom podnošenja.");
        }
      })
      .finally(() => {
        setIsSubmitting(false);
      });
  };

  return (
    <div className="glavnidivg">
      <LoggedHeader></LoggedHeader>
      <div className="posalji-utiske-container">
        <h1>Ostavi svoje utiske o kursu</h1>
        <div className="posalji-utiske-box">

          <div className="posalji-utiske-ocena">
            <label>Ocena:</label>
            <select
              value={rating}
              onChange={(e) => setRating(e.target.value)}
              disabled={isSubmitting}
            >
              <option value="0">Izaberi ocenu</option>
              <option value="1">⭐</option>
              <option value="2">⭐⭐</option>
              <option value="3">⭐⭐⭐</option>
              <option value="4">⭐⭐⭐⭐</option>
              <option value="5">⭐⭐⭐⭐⭐</option>
            </select>
          </div>

          <div className="posalji-utiske-komentar">
            <label>Komentar:</label>
            <textarea
              value={content}
              onChange={(e) => setContent(e.target.value)}
              disabled={isSubmitting}
            ></textarea>
          </div>

          <button onClick={handleSubmit} disabled={isSubmitting} className="posalji-utiske">
            {isSubmitting ? "Podnošenje..." : "Pošalji utiske"}
          </button>

          {submissionError && <p className="error-message">{submissionError}</p>}
        </div>
      </div>
      <Footer></Footer>
    </div>
  );
}

export default DodajReview;