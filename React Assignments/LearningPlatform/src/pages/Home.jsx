import { useNavigate } from 'react-router-dom';

export default function Home() {
  const navigate = useNavigate();
  return (
    <div className="page-container text-center">
      <h1>Welcome to Student Learning Portal</h1>
      <p>Learn React, Web API, and Full Stack Development from one place.</p>
      <div className="button-group">
        <button onClick={() => navigate('/courses')}>View Courses</button>
        <button onClick={() => navigate('/dashboard')} className="btn-secondary">Go to Dashboard</button>
      </div>
    </div>
  );
}