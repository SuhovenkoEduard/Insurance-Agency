import fs from "fs";

const init = (inputPath, outputPath) => {
  let collections = JSON.parse(fs.readFileSync(inputPath));
  collections.map(collection => {
    collection.data.forEach(elem => {
      delete elem._id;
    });
  });
  return collections;
};

export default init;