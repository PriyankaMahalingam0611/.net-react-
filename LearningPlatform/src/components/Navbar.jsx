import { NavLink, useNavigate } from 'react-router-dom';

export default function Navbar({ isAuthenticated, logout }) {
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <nav className="navbar">
      <div className="nav-brand">StudentPortal</div>
      <div className="nav-links">
        <NavLink to="/">Home</NavLink>
        <NavLink to="/about">About</NavLink>
        <NavLink to="/courses">Courses</NavLink>
        <NavLink to="/contact">Contact</NavLink>        
        {!isAuthenticated ? (
          <NavLink to="/login">Login</NavLink>
        ) : (
          <>
            <NavLink to="/dashboard">Dashboard</NavLink>
            <button onClick={handleLogout} className="logout-btn">Logout</button>
          </>
        )}
      </div>
    </nav>
  );
}