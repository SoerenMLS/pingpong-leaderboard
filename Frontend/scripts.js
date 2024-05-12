let baseUrl = "http://localhost:5169"; 

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
