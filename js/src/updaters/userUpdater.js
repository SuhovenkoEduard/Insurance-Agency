const NAMES = {
  user: 'User',
  worker: 'Worker',
  client: 'Client'
};

const update = (collections, funcs) => {
  let users = funcs.findCollection(collections, NAMES.user).data;
  let workers = funcs.findCollection(collections, NAMES.worker).data;
  let clients = funcs.findCollection(collections, NAMES.client).data;

  users = users.map(user => {
    let propName = 'UserId';
    let add;
    if (workers.some(worker => worker[propName] === user[propName])) {
      add = {
        Worker: workers.filter(worker => worker[propName] === user[propName])[0]
      };
    } else {
      add = {
        Client: clients.filter(client => client[propName] === user[propName])[0]
      };
    }
    return {
      ...user,
      ...add
    };
  });

  // remove old collections
  collections = funcs.removeCollection(collections, NAMES.worker);
  collections = funcs.removeCollection(collections, NAMES.client);

  // updaters workers
  funcs.findCollection(collections, NAMES.user).data = users;
  return collections;
};

export let userUpdater = { update: update };

/*
 Worker -> User
 Client -> User
*/