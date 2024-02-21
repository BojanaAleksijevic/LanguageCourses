import React, { useState, useEffect } from 'react';
import { Link } from "react-router-dom";
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { setToken } from "../autentifikacija/setToken";

function Login({ setIsLoggedIn }) {

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errors, setErrors] = useState({});
    const navigate = useNavigate();

    useEffect(() => {
        const isloged = localStorage.getItem('isloged');
        if (isloged === 'yes') {
            // If already logged in, navigate to a different page (e.g., home)
            navigate('/');
        }
    }, [navigate]);

    const handleEmailChange = (value) => {
        setEmail(value);
    }

    const handlePasswordChange = (value) => {
        setPassword(value);
    }

    const handleLogin = async () => {
        try {
            const response = await axios.post('https://localhost:5001/api/User/login', {
                email,
                password,
            });
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('email', response.data.email);
            localStorage.setItem('id', response.data.id);
            localStorage.setItem('firstName', response.data.firstName);
            localStorage.setItem('lastName', response.data.lastName);
            localStorage.setItem('role', response.data.role);
            localStorage.setItem('isloged', 'yes');
            setToken(response.data.token);
            navigate('/');
            window.location.reload(); // Reload the page

        } catch (error) {
            alert("Imamo problema sa konektovenjem sa bazom. Molimo pokusajte kasnije.Hvala!")
        }
    };

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
                <button onClick={() => handleLogin()}>Login</button>
                <hr></hr>
                <Link to="/registruj">
                    <button className=''>
                        Ako nemas nalog, registruj se!
                    </button>
                </Link>
                <hr></hr>
                <Link to="/zaboravio">
                    <button className=''>
                        Zaboravio si lozinku?
                    </button>
                </Link>
            </div>
        </div>
    );
}

export default Login;
