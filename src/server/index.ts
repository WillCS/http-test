import * as express from 'express';

const app = express();
const port: number = 3080;

app.use(express.json());

let storedStrings: string[] = [];

app.post ('/add/', (request, response) => {
    let results: string[] = [];
    request.body.forEach(toAdd => {
        if(storedStrings.includes(toAdd)) {
            results.push(`We already know the word ${toAdd}.`);
        } else {
            results.push(`Learned the word ${toAdd}.`);
            storedStrings.push(toAdd);
        }
    });

    response.send(results);
});

app.get('/get/', (request, response) => {
    if(storedStrings.length == 0) {
        response.send(["We don't know any words yet."]);
    } else {
        response.send([`We know ${storedStrings.length} words.`].concat(storedStrings));
    }
});

app.listen(port, () => console.log(`We listening on port ${port}.`));
