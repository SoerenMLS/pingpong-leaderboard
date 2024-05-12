let baseUrl = "http://localhost:5169"; 
let playerList = [];
let matchList = [];

document.getElementById('createMatchForm').addEventListener('submit', function(e) {
    e.preventDefault();
    const winnerId = document.getElementById('winnerId').value;
    const loserId = document.getElementById('loserId').value;
    const score = document.getElementById('score').value;
    fetch(`${baseUrl}/api/Leaderboard/matches/create`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ winnerId, loserId, score })
    }).then(response => response.json())
      .then(data => console.log(data));
});

document.getElementById('registerPlayerForm').addEventListener('submit', function(e) {
    e.preventDefault();
    const name = document.getElementById('playerName').value;
    fetch(`${baseUrl}/api/Player/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name })
    }).then(response => response.json())
      .then(data => console.log(data));
});

document.getElementById('getAllMatches').addEventListener('click', function() {
    fetch(`${baseUrl}/api/Leaderboard/matches/all`)
        .then(response => response.json())
        .then(matches => {
            const matchesList = document.getElementById('matchesList');
            matchesList.innerHTML = matches.map(match => `<div>Match ID: ${match.id}, Score: ${match.score}</div>`).join('');
        });
});

document.getElementById('getAllPlayers').addEventListener('click', function() {
    fetch(`${baseUrl}/api/Player/all`)
        .then(response => response.json())
        .then(players => {
            const playersList = document.getElementById('playersList');
            playersList.innerHTML = players.map(player => `<div>Player ID: ${player.id}, Name: ${player.name}</div>`).join('');
        });
});

function getAllMatches() {
    fetch(`${baseUrl}/api/Leaderboard/matches/all`)
        .then(response => response.json())
        .then(matches => {
            const matchesList = document.getElementById('matchesList');
            matches.forEach(match => {
                let resolvedMatch = {winner: playerList.find(player => player.id === match.winnerId).name, 
                    loser: playerList.find(player => player.id === match.loserId).name, 
                    score: match.score}
                matchesList.innerHTML += `<div>${resolvedMatch.winner} VS ${resolvedMatch.loser} Score: ${resolvedMatch.score} <b>WINNER: ${resolvedMatch.winner}</b> </div>`
            })
        });
}

function getAllPlayers() {
    fetch(`${baseUrl}/api/Player/all`)
        .then(response => response.json())
        .then(players => {
            const winnerDropdown = document.getElementById('winnerId');
            const loserDropdown = document.getElementById('loserId');
            let options = '<option value="">Select a Player</option>';  // Default option
            playerList = players;
            players.forEach(player => {
                options += `<option value="${player.id}">${player.name}</option>`;
            });
            const playersList = document.getElementById('playersList');
            playersList.innerHTML = players.map(player => `<div>Name: ${player.name} - Wins: ${player.matchesWon} Losses: ${player.matchesLost}</div>`).join('');
            winnerDropdown.innerHTML = options;
            loserDropdown.innerHTML = options;
            console.log(players)
        });
}

getAllPlayers();
getAllMatches();
