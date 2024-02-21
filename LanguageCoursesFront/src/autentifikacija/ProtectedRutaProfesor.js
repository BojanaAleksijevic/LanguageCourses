import React from 'react';
import { Navigate, Outlet, useNavigate } from 'react-router-dom';

const ProtectedRoutesUser = () => {
    const isAuth = localStorage.getItem('token') && localStorage.getItem('role')==="1";
    if (isAuth){
      <Navigate to="/uloguj" />;
  };
 }
  

export default ProtectedRoutesUser;