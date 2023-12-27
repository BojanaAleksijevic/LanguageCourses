import React from 'react';
import { Link } from "react-router-dom";
import { useState } from 'react';
import axios from 'axios';

function Login (){
    
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errors, setErrors] = useState({});

    const handleEmailChange = (value) => {
        setEmail(value);
      }
    
    const handlePasswordChange = (value) => {
        setPassword(value);
      }

      const handleLogin = () => {
        if (validateForm()) {
          const data = {
            
            Email: email,
            Password: password,
            
          }
    
          const url = 'https://localhost:5001/api/User/login';
    
          axios.post(url, data).then((result) => {
            alert("Uspesno ste se ulogovali");
          }).catch((error) => {
            alert(error);
          })
        }
      }

      const validateForm = () => {
        let formIsValid = true;
        const newErrors = {};
    
        
        if (!email) {
          formIsValid = false;
          newErrors.email = 'Morate uneti email';
        }
    
        if (!password) {
          formIsValid = false;
          newErrors.password = 'Morate uneti lozinku';
        }
    
        setErrors(newErrors);
        return formIsValid;
      };


    return (
        <div className='body'>
        <div className='card2'>
          <h1>Login forma</h1>

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


      <button onClick={() => handleLogin()} class="log-btn">Prijavi se</button>

        <Link to="/registruj">
             <button className='log-btn'>
                Ako nemas nalog, registruj se! 
            </button>
        </Link>

        
        

        
        </div>
        
        </div>
    )

}
export default Login;
