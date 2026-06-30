import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { courses } from '../data/courses';

export default function CourseDetails() {
  const { courseId } = useParams();
  const navigate = useNavigate();
  const [isEnrolled, setIsEnrolled] = useState(false);
  
  const course = courses.find((c) => c.id === parseInt(courseId));
  const currentUser = localStorage.getItem('currentUser');

  useEffect(() => {
    if (currentUser && course) {
      const users = JSON.parse(localStorage.getItem('users')) || [];
      const userObj = users.find(u => u.username === currentUser);
      if (userObj && userObj.enrolledCourses.includes(course.id)) {
        setIsEnrolled(true);
      }
    }
  }, [currentUser, course]);

  const handleEnroll = () => {
    if (!currentUser) {
      navigate('/login');
      return;
    }

    const users = JSON.parse(localStorage.getItem('users')) || [];
    const userIndex = users.findIndex(u => u.username === currentUser);
    
    if (userIndex !== -1 && !isEnrolled) {
      users[userIndex].enrolledCourses.push(course.id);
      localStorage.setItem('users', JSON.stringify(users));
      setIsEnrolled(true);
      alert('Successfully enrolled!');
    }
  };

  if (!course) {
    return (
      <div className="page-container text-center">
        <h2>Course not found</h2>
        <button onClick={() => navigate('/courses')}>Back to Courses</button>
      </div>
    );
  }

  return (
    <div className="page-container">
      <h1>Course Details</h1>
      <div className="card course-detail-card">
        <p><strong>Course ID:</strong> {course.id}</p>
        <p><strong>Title:</strong> {course.title}</p>
        <p><strong>Category:</strong> {course.category}</p>
        <p><strong>Duration:</strong> {course.duration}</p>
        <p><strong>Trainer:</strong> {course.trainer}</p>
        <p><strong>Description:</strong> {course.description}</p>
      </div>
      <div className="button-group" style={{ justifyContent: 'flex-start' }}>
        <button onClick={() => navigate('/courses')} className="btn-secondary">Back to Courses</button>
        {currentUser && (
          <button onClick={handleEnroll} disabled={isEnrolled} style={{ backgroundColor: isEnrolled ? '#95a5a6' : '#3498db' }}>
            {isEnrolled ? 'Already Enrolled' : 'Enroll Now'}
          </button>
        )}
      </div>
    </div>
  );
}