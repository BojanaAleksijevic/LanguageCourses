import React, { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import LoggedHeader from '../LoggedHeader.js';
import Footer from '../Footer.js';
import Header from "../Header.js";
import axios from "axios";

function DodajProf() {
    const navigate = useNavigate();
    const isLogged = localStorage.getItem('isloged') === 'yes';

    const [isLoggedIn, setIsLoggedIn] = useState('');
    const token = localStorage.getItem('token');

    const [newProfessor, setNewProfessor] = useState({
        firstName: "",
        lastName: "",
        password: "",
        confirmPassword: "",
        phone: "",
        email: ""
    });
    const [professorAddedMessage, setProfessorAddedMessage] = useState('');

    

    useEffect(() => {
        if (token) {
            setIsLoggedIn(true);
          }

        console.log('isLoggedIn changed:', isLoggedIn);
    }, [isLoggedIn]);

    
    
    

    if (!isLogged) {
        return null;
    }

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNewProfessor((prevProfessor) => ({
            ...prevProfessor,
            [name]: value
        }));
    };

    const handleAddProfessor = async () => {
        try {
            await axios.post("https://localhost:5001/api/User/addProfessor", newProfessor);
            setProfessorAddedMessage("Professor successfully added.");
            
            // Add the following line to return to the home page after adding the professor
            navigate('/');
        } catch (error) {
            console.error("Error adding professor:", error);
            setProfessorAddedMessage("Error adding professor. Please try again.");
        }
    };

    if (!isLogged) {
        return null;
    }

    return (
        <div className="glavnidivg">

        <div>
            {isLoggedIn ? <LoggedHeader /> : <Header />}
                

                {localStorage.getItem('role') === "2" &&  (
                
                <div className="dodaj-kurs-container"style={{ margin: '20px', padding: '10px', width: '50%' }}>
                    <h2>Dodaj profesora</h2>
                    <form>
                        <label>
                            First Name:
                            <input type="text" name="firstName" value={newProfessor.firstName} onChange={handleInputChange} />
                        </label>
                        <label>
                            Last Name:
                            <input type="text" name="lastName" value={newProfessor.lastName} onChange={handleInputChange} />
                        </label>
                        <label>
                            Password:
                            <input type="password" name="password" value={newProfessor.password} onChange={handleInputChange} />
                        </label>
                        <label>
                            Confirm Password:
                            <input type="password" name="confirmPassword" value={newProfessor.confirmPassword} onChange={handleInputChange} />
                        </label>
                        <label>
                            Phone:
                            <input type="text" name="phone" value={newProfessor.phone} onChange={handleInputChange} />
                        </label>
                        <label>
                            Email:
                            <input type="text" name="email" value={newProfessor.email} onChange={handleInputChange} />
                        </label>
                        <button type="button" onClick={handleAddProfessor} className='button-prijava'>
                            Dodaj
                        </button>
                        {professorAddedMessage && <p>{professorAddedMessage}</p>}
                    </form>
                </div>
                )}
            </div>
            
            <div className="main-container-dm"></div>
            <Footer />
        </div>
    );
}

export default DodajProf;