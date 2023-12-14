// Registracija.js

import React, { useState } from "react";
import Header from '../Header.js';
import Footer from '../Footer.js';
import Register from "../register i login/register.js";

function Registracija(){
    return (
        <div className="glavnidivg">
            <Header />

            <Register />

            <Footer />
        </div>
    );
}

export default Registracija;
