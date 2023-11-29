import React from "react";
import {useState} from "react";
import Header from '../Header.js';
import { Link } from "react-router-dom";



function Uloguj(){
  const [email, setEmail] = useState('');
  const [pass, setPass] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(email);
  }


  return(
    <div className="glavnidivg">
      <Header></Header>
      <div className="main">
            <h1>Uloguj se</h1>

            <form>
              <label for="email">Email</label>
              <input value={email} onChange={(e) => setEmail(e.target.value)} type="email" placeholder="vasemail@gmail.com" id="email" name="email" />

              <label for="password">Password</label>
              <input value={pass} onChange={(e) => setPass(e.target.value)} type="password" placeholder="********" id="password" name="password" />

              <button className="button button1">Login</button>
            </form>
            <Link to="/registruj">
                        <button className='button button1'>
                            Registracija
                        </button>
            </Link>
      </div>

    </div>
  )
}
export default Uloguj;