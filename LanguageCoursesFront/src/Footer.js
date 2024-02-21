import React from 'react';

const Footer = () => {
  return (
    <footer className='footer'>
<div className='mreze'>
        <h2>Mozete nas pronaci i na drustvenim mrezama</h2>
        
        <div className="mreze-slike">
          <a href='https://www.instagram.com/happykidscentertopola/'>
            <img src="../../instagram.png" alt="Instagram"  width="50" />
          </a>
          <a href='https://www.facebook.com/happykidscentertopola'>
            <img src="../../facebook.png" alt="Facebook" width="50" />
          </a>
        </div>
      </div>
            
      <p>Kontakt: 📞064/31-37-991</p>
      <p>Happy Kids Center Topola</p>
      
      <div class="tooltip">
        Javi nam se putem mejla
        <div class="tooltiptext">languagecourses.fin@gmail.com</div>
      </div>


    </footer>
  );
}

export default Footer;
