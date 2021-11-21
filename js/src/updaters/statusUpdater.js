const update = (collections, funcs) => {
  let contracts = funcs.findCollection(collections, 'Contract').data;
  let statuses = funcs.findCollection(collections, 'Status').data;

  contracts = contracts.map(contract => {
    let propName = 'StatusId';
    let add = statuses.filter(status => status[propName] === contract[propName])[0];
    return {
      ...contract,
      ...add
    };
  });

  // remove old collections
  collections = funcs.removeCollection(collections, 'Status');

  // updaters workers
  funcs.findCollection(collections, 'Contract').data = contracts;
  return collections;
};

export let statusUpdater = {update: update};

/*
  Status -> Order
*/