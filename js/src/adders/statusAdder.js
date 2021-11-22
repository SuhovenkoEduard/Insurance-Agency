const add = (collections, funcs) => {
  let contracts = funcs.findCollection(collections, 'Contract').data;
  let statuses = funcs.findCollection(collections, 'Status').data;

  contracts = contracts.map(contract => {
    let propName = 'StatusId';
    let add = statuses.filter(status => status[propName] === contract[propName])[0];
    delete contract.StatusId;
    return {
      ...contract,
      Status: add
    };
  });

  // updaters workers
  funcs.findCollection(collections, 'Contract').data = contracts;
  return collections;
};

export let statusAdder = {add: add};

/*
  Status -> Contract
*/