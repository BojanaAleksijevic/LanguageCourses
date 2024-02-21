import React from 'react';
import { Navigate, Outlet, useNavigate } from 'react-router-dom';

const ProtectedRoutesUser = () => {
    const isAuth = localStorage.getItem('token') && localStorage.getItem('role')==="0";
    return isAuth ? <Outlet /> : <Navigate to="/login" />;
  };

export default ProtectedRoutesUser;