import * as fs from 'fs';

// updaters
import { workerUpdater } from './src/updaters/workerUpdater.js';
import { filialUpdater } from './src/updaters/filialUpdater.js';
import { userUpdater } from "./src/updaters/userUpdater.js";

// adders
import { statusAdder } from "./src/adders/statusAdder.js";
import { clientAdder } from "./src/adders/clientAdder.js";
import { workerAdder } from "./src/adders/workerAdder.js";
import { serviceAdder } from "./src/adders/serviceAdder.js";

import init from './src/init.js';
import funcs from "./src/funcs.js";

const inputPath = './out/input.json';
const outputPath = './out/output.json';
let collections = init(inputPath, outputPath);

// updates
collections = workerUpdater.update(collections, funcs);
collections = filialUpdater.update(collections, funcs);
collections = userUpdater.update(collections, funcs);

// adds
collections = statusAdder.add(collections, funcs);
collections = clientAdder.add(collections, funcs);
collections = workerAdder.add(collections, funcs);
collections = serviceAdder.add(collections, funcs);

// fs
collections.forEach(collection => {
  fs.writeFileSync(`./out/collections/${collection.collectionName}.json`,
    JSON.stringify(collection.data, null, '  '));
});

fs.writeFileSync(outputPath, JSON.stringify(collections, null, '  '));
console.log("Updated successful!");