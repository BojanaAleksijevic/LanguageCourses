import React from 'react';
import { Link } from "react-router-dom";
import { useState } from 'react';
import axios from 'axios';

function Register (){
    
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [phone, setPhone] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');


    const handleFirstNameChange = (value) =>{
        setFirstName(value);
    }

    const handleLastNameChange = (value) =>{
        setLastName(value);
    }

    const handlePhoneChange = (value) =>{
        setPhone(value);
    }
    
    const handleEmailChange = (value) =>{
        setEmail(value);
    }
    
    const handlePasswordChange = (value) =>{
        setPassword(value);
    }

    const handleConfirmPasswordChange = (value) =>{
        setConfirmPassword(value);
    }

    const handleRegister = () =>{
        const data = {
            FirstName : firstName,
            LastName : lastName,
            Phone : phone,
            Email : email,
            Password : password,
            ConfirmPassword : confirmPassword
        }

        const url ='https://localhost:5001/api/User/register';

        axios.post(url,data).then((result) =>{
            alert("Uspesno ste se registrovali");
        }).catch((error)=>{
            alert(error);
        })
    }

    return (
        <div>
            <p>Register forma </p>

            <label>First Name</label>
            <input type="text" id="txtFirstName" placeholder="Enter first name" onChange={(e)=>handleFirstNameChange(e.target.value) }  /> <br></br>

            <label>Last Name</label>
            <input type="text" id="txtLastName" placeholder="Enter last name" onChange={(e)=>handleLastNameChange(e.target.value) }/> <br></br>

            <label>Phone</label>
            <input type="text" id="txtPhone" placeholder="Enter phone" onChange={(e)=>handlePhoneChange(e.target.value) }/> <br></br>

            <label>Email</label>
            <input type="text" id="txtEmail" placeholder="Enter email" onChange={(e)=>handleEmailChange(e.target.value) } /> <br></br>

            <label>Password</label>
            <input type="text" id="txtPassword" placeholder="Enter password" onChange={(e)=>handlePasswordChange(e.target.value) } /> <br></br>

            <label>Confirm Password</label>
            <input type="text" id="txtConfirmPassword" placeholder="Confirm password" onChange={(e)=>handleConfirmPasswordChange(e.target.value) } /> <br></br>

            <button onClick={() => handleRegister()}>Register</button>

            <Link to="/uloguj">
             <button className='button button1'>
                Ako vec imas nalog, login se 
            </button>
        </Link>


        </div>
        
    )

}
export default Register;
