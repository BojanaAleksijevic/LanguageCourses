import React from 'react';
import { Link } from "react-router-dom";
import { useState } from 'react';

function Login (){
    
    return (
        <div>
          <p>Login forma</p>

          <label>Email</label>
            <input type="text" id="txtEmail" placeholder="Enter email" />  <br></br>

            <label>Last Name</label>
            <input type="text" id="txtPassword" placeholder="Enter password" /> <br></br>





        <Link to="/registruj">
             <button className='button button1'>
                Ako nisi logovan, registruj se 
            </button>
        </Link>

        
        

        
        </div>
        
        
    )

}
export default Login;
