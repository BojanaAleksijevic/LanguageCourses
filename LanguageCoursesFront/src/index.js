/*import React from 'react';
import ReactDom from 'react-dom'
import App from './App';
*/

//ReactDom.render(<App/>, document.getElementById('root'));

/*
const MainContent = () => {
    return(
        <h1>Cao ja ucim sugavi react</h1>
       
    )
}
*/
/*
ReactDom.render(
    
    <div>
        <p>oprem dobar</p>
    </div>,
    document.getElementById("root")
)
*/

/*
const navbar = (

    <nav>
        <h1>asdasdasd</h1>
        <ul>
            <li>asdasd</li>
            <li>asdasd</li>
            <li>asdasd</li>
        </ul>
    </nav>
)

ReactDom.render(navbar, document.getElementById("root"))
*/



/*
const page = (

    <div>
        <img src="./react.png"  />
        <h1>Fun facts about REACT</h1>
        <ui>
            <li>asdasdasdasd</li>
            <li>asdasdasd</li>
            <li>asdasdasdasd</li>
            <li>asdasdasdasdasd</li>
            <li>asdasdasdasdasdasdasd</li>


        </ui>

    </div>
)
ReactDom.render(page,document.getElementById("root"));
*/



/*
const Header = () =>{
    return (
        <header>
            <nav className="nav">
                <img src="./react.png" width="40px"/>
                <ul className="nav-items">
                 <li>asdasdasd</li>
                 <li>asdasd</li>
                <li>asdasdasd</li>
                </ul>
            </nav>
        </header>
    )
}
*/

/*
const Footer = () =>{
    return(
        <footer>
            <small>THIS IS HUGE TEKST BRE </small>
        </footer>
    )
}
*/
/*
const Main = () =>{
    return(
        <div>
        <h1>Pisem satima istu stvar smorio sam se kao kerina</h1>
        
        </div>
    )
}
*/
/*
import React from 'react'
import ReactDom from 'react-dom'
import Stil from './Stil.css'
import Header from './Header.js'
import Footer from './Footer.js'
import Main from './Main.js'
import Login from './login.js'


const Page = () => {
    return (
        <div className='glavnidivg'>
        <Header/>
        <Main/>
        <Footer/>
        </div> 
    )
}
ReactDom.render(<Page/>, document.getElementById("root"))
*/

/*
// index.js
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import './Stil.css';  // Uključi CSS stilove
import Header from './Header.js';
import Footer from './Footer.js';
import Main from './Main.js';


const Page = () => {
  return (
    <div className='glavnidivg'>
      <Header />
      <Main />
      <Footer />
    </div>
  );
};

ReactDOM.render(<Page />, document.getElementById('root'));
*/

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
import Cenovnik from './Stranice/Cenovnik.js';
import Kontakt from './Stranice/Kontakt.js';
import Lokacija from './Stranice/Lokacija.js';
import Registracija from './Stranice/Registracija.js';
import Verifikacija from './Stranice/Verifikacija.js';

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
        path: "cenovnik",
        element: <Cenovnik/>,
      },
      {
        path: "kontakt",
        element: <Kontakt/>,
      },
      {
        path: "lokacija",
        element: <Lokacija/>,
      },
      {
        path: "registruj",
        element: <Registracija/>,
      },
      {
        path: "verifikacija",
        element: <Verifikacija/>,
      }
  ]);


ReactDOM.render(<RouterProvider router={router} />, document.getElementById('root'));



