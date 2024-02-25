import React, { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import LoggedHeader from '../LoggedHeader.js';
import Footer from '../Footer.js';
import Header from "../Header.js";
import axios from "axios";

function Profil() {
    const navigate = useNavigate();
    const isLogged = localStorage.getItem('isloged') === 'yes';
    const [enrolledCourses, setEnrolledCourses] = useState([]);
    const [isLoggedIn, setIsLoggedIn] = useState('');
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
        const fetchEnrolledCourses = async () => {
            try {
                const response = await axios.get('https://localhost:5001/api/Course/userEnrolled');
                setEnrolledCourses(response.data);
            } catch (error) {
                console.error('Error fetching courses:', error);
            }
        };

        fetchEnrolledCourses();
    }, []);

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
                <div className="profil-box" style={{ margin:'20px', padding: '10px' }}>
                <h2>Licni podaci</h2>
                    <div className="profil-info">
                        <div className="profil-slika">
                        <img src='' className='profil-slika' alt={`Slika za ${localStorage.getItem('firstName')}`} />
                        </div>

                        <div className="profil-podaci">
                            <p>Ime: {localStorage.getItem('firstName')}</p>
                            <p>Prezime: {localStorage.getItem('lastName')}</p>
                            <p>Broj telefona: {localStorage.getItem('Phone')}</p>
                        </div>
                    </div>

                    
                </div>

                <div className="add-professor-form">
                    <h2>Add Professor</h2>
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
                            Add Professor
                        </button>
                        {professorAddedMessage && <p>{professorAddedMessage}</p>}
                    </form>
                </div>
            </div>
            <div className="main-container-dm"></div>
            <Footer />
        </div>
    );
}

export default Profil;