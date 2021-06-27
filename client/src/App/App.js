import './App.css';
import BattleshipsPlayerSetup from '../BattleshipsPlayerSetup/BattleshipsPlayerSetup';
import React, { useEffect, useRef } from 'react';

const App = () => {
    const [eventSource, setEventSource] = React.useState();
    const [players, setPlayers] = React.useState([]);
    
    const playersReference = useRef(players);

    useEffect(() => {
        playersReference.current = players;
    });

    useEffect(() => {
        setEventSource(new EventSource('http://localhost:5000/events'));
    }, []);

    useEffect(() => {
        if (eventSource) {
            eventSource.onmessage = (event) => {
                updatePlayers(JSON.parse(event.data));
            }
            return () => {
                eventSource.close();
            }
        }
    }, [eventSource])

    const updatePlayers = (player) => {
        let newData = [];

        if (playersReference.current.some(p => p.name === player.name)) {
            newData = playersReference.current.map(item => {
                if (item.name === player.name) {
                    item.ships = player.ships;
                }
                return item;
            });
        }
        else {
            newData = [...playersReference.current, player];
        }

        setPlayers(newData);
    }

    return (
        <div className="App">
            <header className="App-header">
                Battleships
            </header>
            <div className="App-content">
                <h2>Players</h2>
                {players.map((player, index) =>
                    <div key={`data-${index}`}>
                        <BattleshipsPlayerSetup player={player}></BattleshipsPlayerSetup>
                    </div>
                )}
            </div>
        </div>
    );
}

export default App;
