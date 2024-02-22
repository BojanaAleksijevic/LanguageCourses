import React, { useEffect, useState  } from "react";
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import LoggedHeader from '../LoggedHeader.js';
import Footer from '../Footer.js';

function UtisciOKursu() {
    const [rating, setRating] = useState(0);
    const [content, setContent] = useState("");
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

    if (isloged !== 'yes') {
        // If not logged in, the component won't be rendered
        return null;
    }


    const handleSubmit = async () => {
        try {
          setIsSubmitting(true);
    
          // Zamijeni 'YOUR_BACKEND_API_URL' sa stvarnim URL-om backend-a
          const response = await axios.post("https://localhost:5001/api/Review/addReview", {
           
            rating,
            content,
          });
    
          console.log("Odgovor od servera:", response.data);
          // Ovde možeš dodati dodatne korake nakon uspešnog podnošenja
        } catch (error) {
          console.error("Greška prilikom podnošenja:", error);
          setSubmissionError("Došlo je do greške prilikom podnošenja.");
        } finally {
          setIsSubmitting(false);
        }
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
    </div>            <Footer></Footer>
        </div>
    );
}

export default UtisciOKursu;