require('dotenv').config();
const bodyParser = require('body-parser');
const express = require('express');
const cors = require('cors');

const corsOptions = {
    origin: 'http://localhost:3000'
};

const players = [
    {
        name: 'Drew',
        ships: [
            { type: 'Carrier', location: 'A1', orientation: 'horizontal' },
            { type: 'Battleship', location: 'B1', orientation: 'horizontal' },
            { type: 'Cruiser', location: 'C1', orientation: 'horizontal' },
            { type: 'Submarine', location: 'D1', orientation: 'horizontal' },
            { type: 'Destroyer', location: 'E1', orientation: 'horizontal' }
        ]
    },
    {
        name: 'Andy',
        ships: [
            { type: 'Carrier', location: 'A1', orientation: 'vertical' },
            { type: 'Battleship', location: 'A2', orientation: 'vertical' },
            { type: 'Cruiser', location: 'A3', orientation: 'vertical' },
            { type: 'Submarine', location: 'A4', orientation: 'vertical' },
            { type: 'Destroyer', location: 'A5', orientation: 'vertical' }
        ]
    },
    {
        name: 'Drew',
        ships: [
            { type: 'Carrier', location: 'E1', orientation: 'horizontal' },
            { type: 'Battleship', location: 'F1', orientation: 'horizontal' },
            { type: 'Cruiser', location: 'G1', orientation: 'horizontal' },
            { type: 'Submarine', location: 'H1', orientation: 'horizontal' },
            { type: 'Destroyer', location: 'I1', orientation: 'horizontal' }
        ]
    }
];

run().catch(error => console.log(error));

openResponse = undefined;

const sendPlayers = async () => {
    let timer = 0;
    let delay = 2;

    while (timer++ < players.length * delay) {
        await new Promise(resolve => setTimeout(resolve, 1000));

        console.log(`Time is ${timer} ticks`);

        if (timer % delay === 0) {
            addPlayer(players[(timer / delay) - 1]);
        }
    }
}

const addPlayer = (player) => {
    openResponse.write('data: ' + JSON.stringify(player));
    openResponse.write("\n\n");
}

async function run() {
    const app = express();

    app.use(bodyParser.json());
    app.use(express.urlencoded());
    app.use(cors(corsOptions));

    app.get('/events', async function (request, response) {
        response.set({
            'Cache-Control': 'no-cache',
            'Content-Type': 'text/event-stream',
            'Connection': 'keep-alive'
        });
        response.flushHeaders();
        response.write('retry: 10000\n\n');

        openResponse = response;
        await sendPlayers();
    });

    app.post('/join', async function (request, response) {
        const player = request.data;
        addPlayer(player);
    });

    var server = app.listen(process.env.API_PORT, () =>
        console.log(`Listening on port ${server.address().port}`)
    );

    server.on('connection', socket => {
        console.log(`A new connection was made by a client`);
    })
}
