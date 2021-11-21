const update = (collections, funcs) => {
  let workers = funcs.findCollection(collections, 'Worker').data;
  let managers = funcs.findCollection(collections, 'Manager').data;
  let agents = funcs.findCollection(collections, 'Agent').data;

  workers = workers.map(worker => {
    let propName = 'WorkerId';
    let add;
    if (managers.some(manager => manager[propName] === worker[propName])) {
      add = managers.filter(manager => manager[propName] === worker[propName])[0];
    } else {
      add = agents.filter(agent => agent[propName] === worker[propName])[0];
    }
    return {
      ...worker,
      ...add
    };
  });

  // remove old collections
  collections = funcs.removeCollection(collections, 'Agent');
  collections = funcs.removeCollection(collections, 'Manager');

  // updaters workers
  funcs.findCollection(collections, 'Worker').data = workers;
  return collections;
};

export let workerUpdater = { update: update };

/*
 Agent -> Worker
 Manager -> Worker
*/