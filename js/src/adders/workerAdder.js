const NAMES = {
  contract: 'Contract',
  user: 'User',
};

const add = (collections, funcs) => {
  let contracts = funcs.findCollection(collections, NAMES.contract).data;
  let users = funcs.findCollection(collections, NAMES.user).data;
  let workers = users.filter(user => 'Worker' in user)
    .map(user => user.Worker);
  let agents = workers.filter(worker => 'Agent' in worker)
    .map(worker => worker.Agent);

  contracts = contracts.map(contract => {
    let propName = 'AgentId';
    let _agent = agents.filter(agent => agent[propName] === contract[propName])[0];
    let _worker = workers.filter(worker => worker.WorkerId === _agent.WorkerId)[0];
    delete contract[propName];
    return {
      ...contract,
      Worker: _worker
    };
  });

  // updaters workers
  funcs.findCollection(collections, 'Contract').data = contracts;
  return collections;
};

export let workerAdder = {add: add};

/*
  Worker -> Contract
*/