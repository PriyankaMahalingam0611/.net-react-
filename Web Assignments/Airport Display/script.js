const initialFlights = [];

let flightsData = [];

function getStatusClass(status) {
    return status.toLowerCase().replace(' ', '-');
}

function getRandomTime() {
    const hours = String(Math.floor(Math.random() * 24)).padStart(2, '0');
    const mins = String(Math.floor(Math.random() * 60)).padStart(2, '0');
    return `${hours}:${mins}`;
}

const clockDisplay = document.getElementById('clock');

function updateClock() {
    const now = new Date();
    const hours = String(now.getHours()).padStart(2, '0');
    const minutes = String(now.getMinutes()).padStart(2, '0');
    const seconds = String(now.getSeconds()).padStart(2, '0');
    clockDisplay.textContent = `${hours}:${minutes}:${seconds}`;
}

updateClock();
setInterval(updateClock, 1000);

const reportDisplay = document.getElementById('report');

function updateReport() {
    let departed = 0, boarding = 0, delayed = 0;
    
    flightsData.forEach(f => {
        if (f.status === 'DEPARTED') departed++;
        if (f.status === 'BOARDING') boarding++;
        if (f.status === 'DELAYED') delayed++;
    });

    reportDisplay.textContent = `${flightsData.length} departures • ${boarding} boarding • ${delayed} delayed`;
}

const board = document.getElementById('board');

function createRow(flight) {
    const rowTr = document.createElement('tr');
    rowTr.className = 'flight-row';

    const timeTd = document.createElement('td');
    timeTd.textContent = flight.time;

    const flightTd = document.createElement('td');
    flightTd.textContent = flight.flight;

    const destTd = document.createElement('td');
    destTd.textContent = flight.dest;

    const gateTd = document.createElement('td');
    gateTd.textContent = flight.gate;

    const statusTd = document.createElement('td');
    statusTd.textContent = flight.status;
    statusTd.className = `status-cell ${getStatusClass(flight.status)}`;
    statusTd.id = `status-${flight.id}`; 

    rowTr.appendChild(timeTd);
    rowTr.appendChild(flightTd);
    rowTr.appendChild(destTd);
    rowTr.appendChild(gateTd);
    rowTr.appendChild(statusTd);

    return rowTr;
}

function renderBoard() {
    board.innerHTML = ''; 
    flightsData.forEach(flight => {
        const row = createRow(flight);
        board.appendChild(row);
    });
    updateReport(); 
}

renderBoard();

const statuses = ['ON TIME', 'DELAYED', 'BOARDING', 'GATE CLOSED', 'DEPARTED'];

function liveStatusUpdate() {
    if (flightsData.length === 0) return;

    const randomIndex = Math.floor(Math.random() * flightsData.length);
    const flight = flightsData[randomIndex];
    const currentIndex = statuses.indexOf(flight.status);
    
    if (currentIndex !== -1 && currentIndex < statuses.length - 1) {
        flight.status = statuses[currentIndex + 1];

        const statusCell = document.getElementById(`status-${flight.id}`);
        if (statusCell) {
            statusCell.textContent = flight.status;
            statusCell.className = `status-cell ${getStatusClass(flight.status)}`;
        }
        updateReport(); 
    }
}

setInterval(liveStatusUpdate, 4000);

const btnAdd = document.getElementById('btn-add');

btnAdd.addEventListener('click', () => {
    const newFlight = {
        id: 'f' + Date.now(), 
        time: getRandomTime(),
        flight: 'XX ' + Math.floor(Math.random() * 9999),
        dest: ['PARIS', 'ROME', 'MIAMI', 'BERLIN'][Math.floor(Math.random() * 4)],
        gate: ['E01', 'F12', 'G02'][Math.floor(Math.random() * 3)],
        status: 'ON TIME'
    };

    flightsData.push(newFlight);
    
    const newRow = createRow(newFlight);
    board.appendChild(newRow);
    
    updateReport();
});

const btnReset = document.getElementById('btn-reset');

btnReset.addEventListener('click', () => {
    flightsData = JSON.parse(JSON.stringify(initialFlights));
    renderBoard();
});