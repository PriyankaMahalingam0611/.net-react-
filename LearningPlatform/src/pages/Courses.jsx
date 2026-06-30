import { useNavigate } from 'react-router-dom';
import { courses } from '../data/courses';

export default function Courses() {
  const navigate = useNavigate();
  return (
    <div className="page-container">
      <h1>Available Courses</h1>
      <div className="courses-grid">
        {courses.map((course) => (
          <div key={course.id} className="card">
            <h3>{course.title}</h3>
            <p><strong>Category:</strong> {course.category}</p>
            <p><strong>Duration:</strong> {course.duration}</p>
            <p><strong>Trainer:</strong> {course.trainer}</p>
            <button onClick={() => navigate(`/courses/${course.id}`)}>View Details</button>
          </div>
        ))}
      </div>
    </div>
  );
}