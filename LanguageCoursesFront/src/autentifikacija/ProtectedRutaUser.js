import React from 'react';
import { Navigate, useNavigate } from 'react-router-dom';

const ProtectedRoutesUser = () => {
  const isAuth = localStorage.getItem('token') && localStorage.getItem('role') === "1";
  const navigate = useNavigate();

  if (!isAuth) {
    // If user is not authenticated, manually navigate to "/uloguj"
    navigate('/uloguj');
    // Return null or an empty component, as Navigate will handle the redirection
    return null;
  }

  // If user is authenticated, return null to render the child components
  return null;
}

export default ProtectedRoutesUser;
