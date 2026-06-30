import { useNavigate } from 'react-router-dom';

export default function Contact() {
  const navigate = useNavigate();
  return (
    <div className="page-container">
      <h1>Contact Support</h1>
      <div className="card">
        <p><strong>Email:</strong> support@studentportal.com</p>
        <p><strong>Phone:</strong> 9876543210</p>
        <p><strong>Location:</strong> Chennai, India</p>
      </div>
      <button onClick={() => navigate(-1)} style={{ marginTop: '20px' }}>Go Back</button>
    </div>
  );
}