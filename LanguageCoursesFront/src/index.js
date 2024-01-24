
import React from 'react';
import ReactDOM from 'react-dom';

import './Stil.css';  // Uključi CSS stilove
import Header from './Header.js';
import Footer from './Footer.js';
import Main from './Main.js';

import {
    createBrowserRouter,
    RouterProvider,
  } from "react-router-dom";

import Uloguj from './Stranice/Uloguj.js';
import Kursevi from './Stranice/Kursevi.js';
import Lokacija from './Stranice/Lokacija.js';
import Akcije from './Stranice/Akcije.js';

import Registracija from './Stranice/Registracija.js';
import Verifikacija from './Stranice/Verifikacija.js';
import Zaboravljena from './Stranice/Zaboravljena.js';

const App = () => {
  return (
    
      <div className='glavnidivg'>
        <Header />
        <Main/>
        <Footer />
      </div>
  
  );
};

const router = createBrowserRouter([
    {
      path: "/",
      element: <App/>,
    },
    {
        path: "uloguj",
        element: <Uloguj/>,
      },
      {
        path: "kursevi",
        element: <Kursevi/>,
      },
      
      {
        path: "lokacija",
        element: <Lokacija/>,
      },
      {
        path: "akcije",
        element: <Akcije/>,
      },
      {
        path: "registruj",
        element: <Registracija/>,
      },
      {
        path: "verifikacija",
        element: <Verifikacija/>,
      },
      {
        path: "zaboravljena",
        element: <Zaboravljena/>,
      }
  ]);


ReactDOM.render(<RouterProvider router={router} />, document.getElementById('root'));



