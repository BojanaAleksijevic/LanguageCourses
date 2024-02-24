import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from 'react-router-dom';  // Dodat import
import LoggedHeader from "../LoggedHeader.js";
import Footer from "../Footer.js";
import '../Stil.css'; 

function DodajKurs() {
  const navigate = useNavigate();  
  const [formData, setFormData] = useState({
    name: "",
    description: "",
    language: "",
    level: "",
    type: 0,
    price: "",
    duration: "",
    picture: null,
  });

  const handleSubmit = async (e) => {
    e.preventDefault();
    const data = new FormData();
    data.append("name", formData.name);
    data.append("description", formData.description);
    data.append("language", formData.language);
    data.append("level", formData.level);
    data.append("type", formData.type);
    data.append("price", formData.price);
    data.append("duration", formData.duration);
    data.append("picture", formData.picture);
  
    try {
      const response = await axios.post("https://localhost:5001/api/Course/addCourse", data, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });
      console.log("Odgovor od servera:", response.data);
      // Preusmeravanje na stranicu "Kursevi" nakon uspešnog dodavanja
      navigate('/kursevi');
    } catch (error) {
      console.error("Greška prilikom slanja kursa:", error);
    }
  };
  

  const handleFileChange = (e) => {
    setFormData((prevData) => ({ ...prevData, picture: e.target.files[0] }));
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
              required
            />
          </label>
          <label className="form-label">
            Tip:
            <br></br>
            <select
              name="type"
              value={formData.type}
              onChange={(e) => setFormData((prevData) => ({ ...prevData, type: e.target.value }))}
              required
            >
              <option value="0">Individualna</option>
              <option value="1">Grupna</option>
            </select>
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
            <label className="form-label">
            Slika kursa:
            <input
              type="file"
              name="picture"
              accept="image/*"
              onChange={handleFileChange}
              className="form-input"
            />
          </label>

            <button type="submit" className="submit-button">
              Dodaj kurs
            </button>
          </form>
      </div>
      <Footer></Footer>
    </div>
  );
}

export default DodajKurs;
