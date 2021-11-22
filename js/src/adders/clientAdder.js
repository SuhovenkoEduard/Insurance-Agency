const NAMES = {
  contract: 'Contract',
  user: 'User',
};

const add = (collections, funcs) => {
  let contracts = funcs.findCollection(collections, NAMES.contract).data;
  let users = funcs.findCollection(collections, NAMES.user).data;
  let clients = users.filter(user => 'Client' in user)
    .map(user => user.Client);

  contracts = contracts.map(contract => {
    let propName = 'ClientId';
    let add = clients.filter(client => client[propName] === contract[propName])[0];
    delete contract[propName];
    return {
      ...contract,
      Client: add
    };
  });

  // updaters workers
  funcs.findCollection(collections, 'Contract').data = contracts;
  return collections;
};

export let clientAdder = {add: add};

/*
  Client -> Contract
*/