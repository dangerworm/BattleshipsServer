import React from 'react';

const BattleshipsPlayerSetup = ({ player }) => {
    return (
        <>
            {player &&
                <p>{player.name}</p>
            }

            {/* Shows players' ships' locations. Use for debugging only */}
            {/* {player &&
                <table>
                    <thead>
                        <tr>
                            <th style={{ textAlign: "left", width: "7em" }}><strong>{player.name}</strong></th>
                            <th>&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        {player.ships.map((ship, index) =>
                            <tr key={`ship-${index}`}>
                                <td>{ship.type}</td>
                                <td>{ship.orientation} from {ship.location}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            } */}
        </>
    )
}

export default BattleshipsPlayerSetup;