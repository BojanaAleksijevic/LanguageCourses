import React, { useState, useEffect } from "react";
import axios from "axios";
import Header from '../Header.js';
import Footer from "../Footer.js";

function Zaboravio() {
  const [email, setEmail] = useState('');
  const [errors, setErrors] = useState({});

  const handleEmailChange = (value) => {
    setEmail(value);
  }

  const handleSubmit = () => {
    if (validateForm()) {
      const url = `https://localhost:5001/api/User/forgot-password?email=${encodeURIComponent(email)}`;

      axios.post(url)
        .then((response) => {
          alert(response.data); 
        })
        .catch((error) => {
          alert(error);
        });
    }
  };

  const validateForm = () => {
    let formIsValid = true;
    const newErrors = {};

    if (!email) {
      formIsValid = false;
      newErrors.email = 'Morate uneti email';
    }

    setErrors(newErrors);
    return formIsValid;
  };

  return (
    <div className="glavnidivg">
      <Header />
      <div className='body'>
        <div className="donjaMargina">
          <div className='card'>
            <h1>Unesite email za vraćanje lozinke</h1>
            <div>
              <label>Email</label>
              <input type="text" id="txtEmail" placeholder="Enter email" onChange={(e) => handleEmailChange(e.target.value)} /> <br></br>
              <div className={errors.email && 'error'}>
                {errors.email && errors.email}
              </div>
            </div>
            
        
            <button onClick={handleSubmit}>Resetuj lozinku</button>
          </div>
        </div>
      </div>
      <Footer />
    </div>
  );
}

export default Zaboravio;
