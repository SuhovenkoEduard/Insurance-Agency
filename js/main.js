import {MongoClient} from 'mongodb';
import * as fs from 'fs';

const collectionNames = [
  'Agent',
  'Client',
  'Contract',
  'DType',
  'Departament',
  'Filial',
  'Manager',
  'Service',
  'Status',
  'User',
  'Worker'
];

const mongoCredentials = {
  dbName: 'InsuranceAgency',
  login: 'avatar_dj@mail.ru',
  password: '17Faslge'
};
const connectUrl = `mongodb+srv://edsuhovenko:${mongoCredentials.password}@cluster0.whuyf.mongodb.net/myFirstDatabase?retryWrites=true&w=majority`;

const client = new MongoClient(connectUrl);

async function run() {
  try {
    await client.connect();
    let db = await client.db(mongoCredentials.dbName);

    let collections = await Promise.all(collectionNames.map(async (x) => {
      return {
        collectionName: x,
        data: await db.collection(x).find().toArray(),
      };
    }));

    fs.writeFileSync('./out/input.json',
      JSON.stringify(collections, null, '  '));

    console.log("Connected successfully to server");
  } catch (e) {
    console.log(e.message);
  } finally {
    await client.close();
  }
}

await run();