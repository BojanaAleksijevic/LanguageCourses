
import React, { useState } from "react";
import axios from "axios";
import LoggedHeader from "../LoggedHeader.js";
import Footer from "../Footer.js";
import '../Stil.css'; 

function DodajKurs() {
  const [formData, setFormData] = useState({
    name: "",
    description: "",
    language: "",
    level: "",
    type: 0,
    price: "",
    duration: "",
  });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      // Slanje podataka na server ili obavljanje željene logike
      const response = await axios.post("https://localhost:5001/api/Course/addCourse", formData);
      console.log("Odgovor od servera:", response.data);
    } catch (error) {
      console.error("Greška prilikom slanja kursa:", error);
      // Obrada greške
    }
  };

  return (
    <div className="glavnidivg">
      <LoggedHeader></LoggedHeader>
      <div className="dodaj-kurs-container">
        <h2>Dodaj kurs</h2>
        <form className="dodaj-kurs-forma" onSubmit={handleSubmit}>
          <label>
            Naziv kursa:
            <input
              type="text"
              name="name"
              value={formData.name}
              onChange={(e) => setFormData((prevData) => ({ ...prevData, name: e.target.value }))}
              required
            />
          </label>
          <label>
            Opis kursa:
            <textarea
              name="description"
              value={formData.description}
              onChange={(e) => setFormData((prevData) => ({ ...prevData, description: e.target.value }))}
              required
            />
          </label>
          <label>
            Jezik:
            <input
              type="text"
              name="language"
              value={formData.language}
              onChange={(e) => setFormData((prevData) => ({ ...prevData, language: e.target.value }))}
              required
            />
          </label>
          <label>
            Nivo:
            <input
              type="text"
              name="level"
              value={formData.level}
              onChange={(e) => setFormData((prevData) => ({ ...prevData, level: e.target.value }))}
              required
            />
          </label>
          <label>
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
            <label>
            Cena (u €):
            <input
                type="text"
                name="price"
                value={formData.price}
                onChange={(e) => setFormData((prevData) => ({ ...prevData, price: e.target.value }))}
                pattern="[0-9]+"
                required
            />
            </label>
            <label>
            Trajanje (u časovima):
            <input
                type="text"
                name="duration"
                value={formData.duration}
                onChange={(e) => setFormData((prevData) => ({ ...prevData, duration: e.target.value }))}
                pattern="[0-9]+"
                required
            />
            </label>

          <button type="submit">Dodaj kurs</button>
        </form>
      </div>
      <Footer></Footer>
    </div>
  );
}

export default DodajKurs;
