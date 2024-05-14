let baseUrl = "http://localhost:5169"; 
let playerList = [];
let matchList = [];

function getAllMatches() {
    fetch(`${baseUrl}/api/Leaderboard/matches/all`)
        .then(response => response.json())
        .then(matches => {
            const matchesList = document.getElementById('matchesList');
            let tableHTML = `<table>
                                <tr>
                                    <th>Player 1</th>
                                    <th>Player 2</th>
                                    <th>Score</th>
                                    <th>Winner</th>
                                </tr>`;
            matches.forEach(match => {
                let resolvedMatch = {
                    winner: playerList.find(player => player.id === match.winnerId).name,
                    loser: playerList.find(player => player.id === match.loserId).name,
                    score: match.score
                }
                tableHTML += `<tr>
                                <td>${resolvedMatch.winner}</td>
                                <td>${resolvedMatch.loser}</td>
                                <td>${resolvedMatch.score}</td>
                                <td><b>${resolvedMatch.winner}</b></td>
                              </tr>`;
            });
            tableHTML += '</table>';
            matchesList.innerHTML = tableHTML;
        });
}

function getPlayerAndMatchesTables() {
    fetch(`${baseUrl}/api/Player/all`)
        .then(response => response.json())
        .then(players => {
            const playersList = document.getElementById('playersList');
            let tableHTML = `<table>
                                <tr>
                                    <th>Name</th>
                                    <th>Wins</th>
                                    <th>Losses</th>
                                </tr>`;
            playerList = players; // Update playerList for global access
            players.forEach(player => {
                tableHTML += `<tr>
                                <td>${player.name}</td>
                                <td>${player.matchesWon}</td>
                                <td>${player.matchesLost}</td>
                              </tr>`;
            });
            tableHTML += '</table>';
            playersList.innerHTML = tableHTML;

            const winnerDropdown = document.getElementById('winnerId');
            const loserDropdown = document.getElementById('loserId');
            let options = '<option value="">Select a Player</option>'; // Default option
            players.forEach(player => {
                options += `<option value="${player.id}">${player.name}</option>`;
            });
            winnerDropdown.innerHTML = options;
            loserDropdown.innerHTML = options;
        }).then(() => getAllMatches());
}

document.getElementById('createMatchForm').addEventListener('submit', function(e) {
    e.preventDefault();
    const winnerId = document.getElementById('winnerId').value;
    const loserId = document.getElementById('loserId').value;
    const score = document.getElementById('score').value;
    fetch(`${baseUrl}/api/Leaderboard/matches/create`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ winnerId, loserId, score })
    }).then(() => getPlayerAndMatchesTables())    
});

document.getElementById('registerPlayerForm').addEventListener('submit', function(e) {
    e.preventDefault();
    const name = document.getElementById('playerName').value;
    fetch(`${baseUrl}/api/Player/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name })
    }).then(() => getPlayerAndMatchesTables())
});

getPlayerAndMatchesTables();