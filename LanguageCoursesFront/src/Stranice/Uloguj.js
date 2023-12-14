import React from "react";
import {useState} from "react";
import Header from '../Header.js';
import { Link } from "react-router-dom";
import Footer from "../Footer.js";
import Login from "../register i login/login.js";


function Uloguj(){

  return(
    <div className="glavnidivg">
      <Header></Header>
      <Login></Login>

      <Footer></Footer>

    </div>
  )
}
export default Uloguj;