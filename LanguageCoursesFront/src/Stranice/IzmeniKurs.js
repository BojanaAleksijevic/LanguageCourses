import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
import '../Stil.css';
import LoggedHeader from '../LoggedHeader.js';
import Footer from '../Footer.js';

const IzmeniKurs = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    id: id,
    description: "",
    price: 0,
    duration: 0
  });

  const [isSubmitting, setIsSubmitting] = useState(false);
  const [submissionError, setSubmissionError] = useState(null);

  useEffect(() => {
    const fetchCourseDetails = async () => {
      try {
        const response = await axios.get(`https://localhost:5001/api/Course/${id}`);
        setFormData({
          id: id,
          description: response.data.description,
          price: response.data.price,
          duration: response.data.duration
        });
      } catch (error) {
        console.error('Error fetching course details:', error);
      }
    };

    fetchCourseDetails();
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
  
    try {
      setIsSubmitting(true);
      console.log("Slanje zahteva za izmenu kursa...");
  
      // Validacija pre slanja zahteva
      if (!formData.description || !formData.price || !formData.duration) {
        // Ako neki od obaveznih podataka nisu popunjeni, prikaži poruku i prekini proces izmene
        setSubmissionError("Svi podaci su obavezni.");
        return;
      }
  
      const response = await axios.put(`https://localhost:5001/api/Course/updateCourse`, {
  id: id,
  description: formData.description,
  price: parseInt(formData.price, 10),
  duration: parseInt(formData.duration, 10)
});

  
      console.log("Odgovor od servera:", response.data);
  
      // Dodajte redirekciju ili ažuriranje UI-a nakon uspešne izmene
      navigate(`/detaljiKursa/${id}`);
    } catch (error) {
      console.error('Error updating course:', error);
  
      if (error.response) {
        // Ako postoji odgovor od servera, prikažite ga u konzoli
        console.log("Odgovor servera:", error.response.data);
      }
  
      // Obrada greške i postavljanje odgovarajuće poruke za korisnika
      setSubmissionError("Došlo je do greške prilikom izmene kursa.");
    } finally {
      setIsSubmitting(false);
    }
  };
  
  return (
    <div className='glavnidivg'>
      <LoggedHeader />
      <div className="dodaj-kurs-container">
        <h2>Izmeni kurs</h2>
        <form className="izmeni-kurs-forma" onSubmit={handleSubmit}>
          <label className="form-label">
            Opis kursa:
            <textarea
              name="description"
              className="form-input"
              value={formData.description}
              onChange={(e) => setFormData((prevData) => ({ ...prevData, description: e.target.value }))}
              required
            />
          </label>
      
          <label className="form-label">
            Cena (u €):
            <input
              type="text"
              name="price"
              className="form-input"
              value={formData.price}
              onChange={(e) => setFormData((prevData) => ({ ...prevData, price: e.target.value }))}
              pattern="[0-9]+"
              required
            />
          </label>
          <label className="form-label">
            Trajanje (u časovima):
            <input
              type="text"
              name="duration"
              className="form-input"
              value={formData.duration}
              onChange={(e) => setFormData((prevData) => ({ ...prevData, duration: e.target.value }))}
              pattern="[0-9]+"
              required
            />
          </label>
          <center>
          <button  type="submit" className="submit-button" disabled={isSubmitting}>
            {isSubmitting ? "Izmena..." : "Sačuvaj izmene"}
            </button>
            </center>
          {submissionError && <p className="error-message">{submissionError}</p>}
        </form>
      </div>
      <Footer />
    </div>
  );
};

export default IzmeniKurs;
