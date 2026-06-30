const joinForm = document.querySelector('.join-form');
joinForm.addEventListener('submit', function(event){
    event.preventDefault();
    const name = document.getElementById('name').value;
    const email = document.getElementById("email").value;
    const experience = document.getElementById("experience").value;
    if(name=='' || email==''){
        alert("Please fill out name and email.");
        return;
    }
    joinForm.innerHTML='';
    const success = document.createElement('p');
    success.classList.add('success-message');
    success.textContent = "Thank you for joining the club!";
    joinForm.appendChild(success);
});
