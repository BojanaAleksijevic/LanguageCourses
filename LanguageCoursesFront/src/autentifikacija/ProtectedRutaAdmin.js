import React from 'react';
import { Navigate, Outlet, useNavigate } from 'react-router-dom';

const ProtectedRutaAdmin = () => {
    const isAuth = localStorage.getItem('token')!=="" && localStorage.getItem('role')==="2";
    return isAuth ? <Outlet /> : <Navigate to="/login" />;
  };

export default ProtectedRutaAdmin;