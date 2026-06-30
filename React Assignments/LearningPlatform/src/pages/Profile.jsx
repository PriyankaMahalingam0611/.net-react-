export default function Profile() {
  const currentUser = localStorage.getItem('currentUser');
  const users = JSON.parse(localStorage.getItem('users')) || [];
  const userObj = users.find(u => u.username === currentUser);

  if (!userObj) {
    return <div>Loading profile...</div>;
  }

  return (
    <div>
      <h2>Student Profile</h2>
      <div className="card">
        <p><strong>Name:</strong> {userObj.username}</p>
        <p><strong>Email:</strong> {userObj.username}@studentportal.com</p>
        <p><strong>Enrolled Courses:</strong> {userObj.enrolledCourses ? userObj.enrolledCourses.length : 0}</p>
        <p><strong>Status:</strong> Active</p>
      </div>
    </div>
  );
}