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

        let timer = 0;

        while (timer < players.length * 3000) {
            await new Promise(resolve => setTimeout(resolve, 1000));

            timer += 1000;
            console.log(`Time is ${timer / 1000} ticks`);

            if (timer % 3000 === 0) {
                response.write('data: ' + JSON.stringify(players[(timer / 3000) - 1]));
                response.write("\n\n");
            }
        }
    });

    var server = app.listen(process.env.API_PORT, () =>
        console.log(`Listening on port ${server.address().port}`)
    );

    server.on('connection', socket => {
        console.log(`A new connection was made by a client`);
    })
}
