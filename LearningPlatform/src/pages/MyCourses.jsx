import { courses } from '../data/courses';

export default function MyCourses() {
  const currentUser = localStorage.getItem('currentUser');
  const users = JSON.parse(localStorage.getItem('users')) || [];
  const userObj = users.find(u => u.username === currentUser);
  
  const enrolledIds = userObj ? userObj.enrolledCourses : [];
  const enrolledCourses = courses.filter(course => enrolledIds.includes(course.id));

  return (
    <div>
      <h2>My Enrolled Courses</h2>
      {enrolledCourses.length === 0 ? (
        <p>You have not enrolled in any courses yet.</p>
      ) : (
        <ul className="list-styled">
          {enrolledCourses.map(course => (
            <li key={course.id}>{course.title} - <em>{course.trainer}</em></li>
          ))}
        </ul>
      )}
    </div>
  );
}