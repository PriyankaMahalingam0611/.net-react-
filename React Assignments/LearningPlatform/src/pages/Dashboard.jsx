import { NavLink, Outlet } from 'react-router-dom';

export default function Dashboard() {
  return (
    <div className="dashboard-container">
      <aside className="sidebar">
        <h3>Welcome to Student Dashboard</h3>
        <nav className="dashboard-nav">
          <NavLink to="/dashboard/profile">Profile</NavLink>
          <NavLink to="/dashboard/my-courses">My Courses</NavLink>
          <NavLink to="/dashboard/settings">Settings</NavLink>
        </nav>
      </aside>
      <main className="dashboard-content">
        <Outlet />
      </main>
    </div>
  );
}