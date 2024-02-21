import React, { useEffect } from "react";
import { useNavigate } from 'react-router-dom';
import Header from '../Header.js';
import Footer from '../Footer.js';
import Register from "../register i login/register.js";

function Registracija() {
    const navigate = useNavigate();
    const isloged = localStorage.getItem('isloged');

    useEffect(() => {
        if (isloged === 'yes') {
            // If already logged in, navigate to a different page (e.g., home)
            navigate('/');
        }
    }, [isloged, navigate]);

    if (isloged === 'yes') {
        // If already logged in, the component won't be rendered
        return null;
    }

    return (
        <div className="glavnidivg">
            <Header />
            <Register />
            <Footer />
        </div>
    );
}

export default Registracija;
