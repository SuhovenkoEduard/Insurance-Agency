import * as fs from 'fs';

import { workerUpdater } from './src/updaters/workerUpdater.js';
import { statusUpdater } from "./src/updaters/statusUpdater.js";
import { filialUpdater } from './src/updaters/filialUpdater.js';

import init from './src/init.js';
import funcs from "./src/funcs.js";

const inputPath = './out/input.json';
const outputPath = './out/output.json';
let collections = init(inputPath, outputPath);

collections = workerUpdater.update(collections, funcs);
collections = statusUpdater.update(collections, funcs);
collections = filialUpdater.update(collections, funcs);

fs.writeFileSync(outputPath, JSON.stringify(collections, null, '  '));
console.log("Updated successful!");