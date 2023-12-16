import React from 'react';
import { Link } from "react-router-dom";
import { useState } from 'react';

function Login (){
    
    return (
        <div className='body'>
        <div className='card2'>
          <h1>Login forma</h1>

          <label>Email</label>
            <input type="text" id="txtEmail" placeholder="Enter email" />  <br></br>

            <label>Last Name</label>
            <input type="text" id="txtPassword" placeholder="Enter password" /> <br></br>



            <button>Login</button>

        <Link to="/registruj">
             <button className=''>
                Ako nemas nalog, registruj se! 
            </button>
        </Link>

        
        

        
        </div>
        
        </div>
    )

}
export default Login;
