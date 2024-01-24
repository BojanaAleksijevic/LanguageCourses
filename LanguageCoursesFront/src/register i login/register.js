import React from 'react';
import { Link } from "react-router-dom";
import { useState } from 'react';
import axios from 'axios';

import './Register.css'; // Adjust the path based on your file structure


function Register() {

  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [phone, setPhone] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [errors, setErrors] = useState({});

  const handleFirstNameChange = (value) => {
    setFirstName(value);
  }

  const handleLastNameChange = (value) => {
    setLastName(value);
  }

  const handlePhoneChange = (value) => {
    setPhone(value);
  }

  const handleEmailChange = (value) => {
    setEmail(value);
  }

  const handlePasswordChange = (value) => {
    setPassword(value);
  }

  const handleConfirmPasswordChange = (value) => {
    setConfirmPassword(value);
  }

  const handleRegister = () => {
    if (validateForm()) {
      const data = {
        FirstName: firstName,
        LastName: lastName,
        Phone: phone,
        Email: email,
        Password: password,
        ConfirmPassword: confirmPassword
      }

      const url = 'https://localhost:5001/api/User/register';

      axios.post(url, data).then((result) => {
        alert("Uspesno ste se registrovali");
      }).catch((error) => {
        alert(error);
      })
    }
  }

  const validateForm = () => {
    let formIsValid = true;
    const newErrors = {};

    if (!lastName) {
      formIsValid = false;
      newErrors.lastName = 'Morate uneti prezime';
    }

    if (!firstName) {
      formIsValid = false;
      newErrors.firstName = 'Morate uneti ime';
    }

    if (!email) {
      formIsValid = false;
      newErrors.email = 'Morate uneti email';
    }

    if (!password) {
      formIsValid = false;
      newErrors.password = 'Morate uneti lozinku';
    }

    if (!confirmPassword) {
      formIsValid = false;
      newErrors.confirmPassword = 'Morate potvrditi lozinku';
    }

    if (password !== confirmPassword) {
      formIsValid = false;
      newErrors.confirmPassword = 'Lozinke se ne poklapaju';
    }

    if (!phone) {
      formIsValid = false;
      newErrors.phone = 'Morate uneti nesto o sebi';
    }

    setErrors(newErrors);
    return formIsValid;
  };

  return (
    <div className='body'>
    <div className='card'>
      <h1>Register forma </h1>

      <div>
        <label>First Name</label>
        <input type="text" id="txtFirstName" placeholder="Enter first name" onChange={(e) => handleFirstNameChange(e.target.value)} /> <br></br>
        <div className={errors.firstName && 'error'}>
        {errors.firstName && errors.firstName}
      </div>
      </div>

      <div>
        <label>Last Name</label>
        <input type="text" id="txtLastName" placeholder="Enter last name" onChange={(e) => handleLastNameChange(e.target.value)} /> <br></br>
        <div className={errors.lastName && 'error'}>
        {errors.lastName && errors.lastName}
      </div>
      </div>

      <div>
        <label>Phone</label>
        <input type="text" id="txtPhone" placeholder="Enter phone" onChange={(e) => handlePhoneChange(e.target.value)} /> <br></br>
        <div className={errors.phone && 'error'}>
        {errors.phone && errors.phone}
        </div>
      </div>

      <div>
        <label>Email</label>
        <input type="text" id="txtEmail" placeholder="Enter email" onChange={(e) => handleEmailChange(e.target.value)} /> <br></br>
        <div className={errors.email && 'error'}>
        {errors.email && errors.email}
        </div>
      </div>

      <div>
        <label>Password</label>
        <input type="password" id="txtPassword" placeholder="Enter password" onChange={(e) => handlePasswordChange(e.target.value)} /> <br></br>
        <div className={errors.password && 'error'}>
        {errors.password && errors.password}
        </div>
      </div>

      <div>
        <label>Confirm Password</label>
        <input type="password" id="txtConfirmPassword" placeholder="Confirm password" onChange={(e) => handleConfirmPasswordChange(e.target.value)} /> <br></br>
        <div className={errors.confirmPassword && 'error'}>
        {errors.confirmPassword && errors.confirmPassword}
        </div>
      </div>

      <button onClick={() => handleRegister()}>Register</button>
      <hr></hr>

      <Link to="/uloguj">
        <button className=''>
          Ako već imate nalog, ulogujte se!
        </button>
      </Link>
    </div>
    </div>
  )
}

export default Register;
