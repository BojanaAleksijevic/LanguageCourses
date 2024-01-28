import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Header from '../Header';
import Footer from '../Footer';
import { useNavigate, useLocation } from 'react-router-dom';

function Zaboravljena() {
  const [password, setPassword] = useState('');
  const [Cpassword, setCPassword] = useState('');
  const [errors, setErrors] = useState({});
  const [successMessage, setSuccessMessage] = useState('');
  const location = useLocation();
  const navigate = useNavigate();

  const handlePasswordChange = (value) => {
    setPassword(value);
  }

  const handleCPasswordChange = (value) => {
    setCPassword(value);
  }

  const queryParams = new URLSearchParams(location.search);
  const token = queryParams.get('token');

  const handleReset = () => {
    if (validateForm()) {
     
      console.log('Token:', token);
      console.log('Password:', password);
      console.log('Confirm Password:', Cpassword);
      
      const data = {
        Token: token,
        Password: password,
        ConfirmPassword: Cpassword,
      }
  
      const url = 'https://localhost:5001/api/User/reset-password';
  
      axios.post(url, data)
        .then((result) => {
          alert("Uspesno ste promenili lozinku");
          navigate('/uloguj');
        })
        .catch((error) => {
          alert(error);
        });
    }
  }
  const validateForm = () => {
    let formIsValid = true;
    const newErrors = {};

    if (!password) {
      formIsValid = false;
      newErrors.password = 'Morate uneti lozinku';
    }

    if (!Cpassword) {
      formIsValid = false;
      newErrors.Cpassword = 'Morate uneti ponovljenu lozinku';
    } else if (Cpassword !== password) {
      formIsValid = false;
      newErrors.Cpassword = 'Lozinke se ne podudaraju';
    }

    setErrors(newErrors);
    return formIsValid;
  };

  return (
    <div className='glavnidivg'>
      <Header />
      <div className='body'>
        <div className='card2'>
          <h1>Resetovanje lozinke</h1>
          <div>
            <label>Password</label>
            <input type="password" id="txtPassword" placeholder="Enter password" onChange={(e) => handlePasswordChange(e.target.value)} />
            <br />
            <div className={errors.password && 'error'}>
              {errors.password && errors.password}
            </div>
          </div>
          <div>
            <label>Confirm Password</label>
            <input type="password" id="txtCPassword" placeholder="Confirm password" onChange={(e) => handleCPasswordChange(e.target.value)} />
            <br />
            <div className={errors.Cpassword && 'error'}>
              {errors.Cpassword && errors.Cpassword}
            </div>
          </div>
          <button onClick={handleReset}>Resetuj lozinku</button>
          {successMessage && <div className="success-message">{successMessage}</div>}
          {errors.general && <div className="error">{errors.general}</div>}
          <hr />
        </div>
      </div>
      <Footer />
    </div>
  )
}

export default Zaboravljena;
