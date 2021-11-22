const NAMES = {
  contract: 'Contract',
  service: 'Service',
  dtype: 'DType',
};

const add = (collections, funcs) => {
  let contracts = funcs.findCollection(collections, NAMES.contract).data;
  let services = funcs.findCollection(collections, NAMES.service).data;
  let dtypes = funcs.findCollection(collections, NAMES.dtype).data;

  contracts = contracts.map(contract => {
    let propNames = {
      service: 'ServiceId',
      dtype: 'DTypeId',
    };
    let _service = services
      .filter(service => service[propNames.service] === contract[propNames.service])[0];
    _service = { ..._service };
    let _dtype = dtypes
      .filter(dtype => dtype[propNames.dtype] === _service[propNames.dtype])[0];
    delete _service[propNames.dtype];
    _service[NAMES.dtype] = _dtype;

    delete contract[propNames.service];
    return {
      ...contract,
      Service: _service
    };
  });

  // updaters workers
  funcs.findCollection(collections, 'Contract').data = contracts;
  return collections;
};

export let serviceAdder = {add: add};

/*
  Service -> Contract
*/