import React from 'react'

const Main = () =>{
    return(
        <div className='glavnidiv'>
            <div className='main-left'>
            <h1 className='main'>Dobrodosli u prvu školu stranih jezika u Topoli!</h1>
            <ul className='main-opis'>
                <h3>Zasto je vazno da se uci strani jezik?
    
                    <li>Ucenjem stranog jezika poboljsava se pamcenje.</li>
                    <li>Postizu se bolji skolski rezultati.</li>
                    <li>Pomazu vasem detetu u ucenju drugih jezika.</li>
                </h3>
                <h2>Odaberite omiljeni jezik i pridružite se vašim drugarima!</h2>
                <h1>Zasto bas mi?</h1>
                <h3>Zato sto predstavljamo inicijativu mladih i obrazovanih predavača koji se trude da probude svest o važnosti poznavanja jezika u modernom društvu. 
                    Prioritet nam je da prenesemo svoje znanje na kvalitetan način. 
                    Naš cilj je da sa svakog časa odlazite sa osmehom na licu! </h3>
                

                
            </ul>
            </div>

            <div className='main-right'>
                <h2>Kursevi</h2>
                
                <ul className='bold'>
                    <li>Engleski - prof Ivana Djurdjevic i prof Jelisaveta Miladinovic</li>
                    <img src="./britanija3.png" width="80px" />
                    <li>Spanski - prof Milica Cirovic</li>
                    <img src="./spanija3.png" width="80px" />
                    <li>Italijanski - prof Milena Radojkovic</li>
                    <img src="./italija3.png" width="80px" />
                    <li>Turski - prof Stefan Jovanovic</li>
                    <img src="./turska3.png" width="80px" />
                </ul>
            </div>

           
        </div>

    )
}

export default Main;
