const findCollection = (arrays, name) => {
  return arrays.filter(x => x.collectionName === name)[0];
};

const removeCollection = (arrays, name) => {
  return arrays.filter(array => array.collectionName !== name);
};

let funcs = {
  findCollection,
  removeCollection
};

export default funcs;