import React, { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from 'react-router-dom';
import LoggedHeader from "../LoggedHeader.js";
import Footer from "../Footer.js";
import '../Stil.css';

function DodajKurs() {
  const navigate = useNavigate();
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [submissionError, setSubmissionError] = useState(null);

  const [formData, setFormData] = useState({
    name: "",
    description: "",
    language: "",
    level: "",
    
    price: "",
    duration: "",
    picture: null,
  });

  const handleFileChange = async (e) => {
    const file = e.target.files[0];

    if (file && file.type.startsWith('image/')) {
      try {
        const base64String = await convertFileToBase64(file);
        setFormData((prevData) => ({ ...prevData, picture: base64String }));
      } catch (error) {
        console.error("Greška prilikom konvertovanja slike u base64:", error);
      }
    }
  };
  
  const convertFileToBase64 = (file) => {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
  
      reader.onloadend = () => {
        resolve(reader.result.split(',')[1]);
      };
  
      reader.onerror = (error) => {
        reject(error);
      };
  
      reader.readAsDataURL(file);
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
  
    if (!formData.name || !formData.level || !formData.language || !formData.description) {
      setSubmissionError("Molimo vas da popunite sva obavezna polja.");
      return;
    }
  
    const data = new FormData();

    data.append("name", formData.name);
    data.append("description", formData.description);
    data.append("language", formData.language);
    data.append("level", formData.level);
  
    data.append("price", formData.price);
    data.append("duration", formData.duration);
  
    // Dodajemo sliku samo ako je odabrana
    if (formData.picture) {
      data.append("picture", formData.picture);
    }
  
    try {
      setIsSubmitting(true);
      console.log("FormData before request:", data);
  
      const response = await axios.post(
        "https://localhost:5001/api/Course/addCourse",
        data,
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
  
      console.log("Odgovor od servera:", response.data);
  
      // Navigacija nazad na prethodnu stranicu
      navigate(-1);
    } catch (error) {
      console.error("Greška prilikom podnošenja:", error);
      if (error.response) {
        setSubmissionError(error.response.data.message || "Došlo je do greške prilikom podnošenja.");
        console.log("Detalji greške:", error.response.data.errors);
      } else if (error.request) {
        setSubmissionError("Nije primljen odgovor od servera.");
      } else {
        setSubmissionError("Došlo je do greške prilikom podnošenja.");
      }
    } finally {
      setIsSubmitting(false);
    }
  };




  return (
    <div className="glavnidivg">
      <LoggedHeader></LoggedHeader>
      <div className="dodaj-kurs-container">
        <h2>Dodaj kurs</h2>
        <form className="dodaj-kurs-forma" onSubmit={handleSubmit}>
          <label className="form-label">
            Naziv kursa:
            <input
              type="text"
              className="form-input"
              name="name"
              value={formData.name}
              onChange={(e) => setFormData((prevData) => ({ ...prevData, name: e.target.value }))}
              disabled={isSubmitting}
              required
            />
          </label>
          <label className="form-label">
            Opis kursa:
            <textarea
              name="description"
              className="form-input"
              value={formData.description}
              onChange={(e) => setFormData((prevData) => ({ ...prevData, description: e.target.value }))}
              disabled={isSubmitting}
              required
            />
          </label>
          <label className="form-label">
            Jezik:
            <input
              type="text"
              name="language"
              className="form-input"
              value={formData.language}
              onChange={(e) => setFormData((prevData) => ({ ...prevData, language: e.target.value }))}
              disabled={isSubmitting}
              required
            />
          </label>
          <label className="form-label">
            Nivo:
            <input
              type="text"
              name="level"
              className="form-input"
              value={formData.level}
              onChange={(e) => setFormData((prevData) => ({ ...prevData, level: e.target.value }))}
              disabled={isSubmitting}
              required
            />
          </label>
          
          <label className="form-label">
            Cena (u €):
            <input
                type="number"
                name="price"
                className="form-input"
                value={formData.price}
                onChange={(e) => setFormData((prevData) => ({ ...prevData, price: e.target.value }))}
                pattern="[0-9]+"
                disabled={isSubmitting}
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
                disabled={isSubmitting}
                required
            />
          </label>
          <label className="form-label">
            Slika kursa:
            <input
              type="file"
              name="picture"
              accept="image/*"
              onChange={handleFileChange}
              disabled={isSubmitting}
              className="form-input"
            />
          </label>

          <button type="submit" disabled={isSubmitting} className="submit-button">
            {isSubmitting ? "Podnošenje..." : "Dodaj kurs"}
          </button>
        </form>
      </div>
      <Footer></Footer>
    </div>
  );
}

export default DodajKurs;