const NAMES = {
  filial: "Filial",
  department: "Departament"
};

const update = (collections, funcs) => {
  let filials = funcs.findCollection(collections, NAMES.filial).data;
  let departaments = funcs.findCollection(collections, NAMES.department).data;

  filials = filials.map(filial => {
    let propName = 'FilialId';
    let add = departaments.filter(department => filial[propName] === department[propName]);
    return {
      ...filial,
      Departments: add
    };
  });

  // remove old collections
  collections = funcs.removeCollection(collections, NAMES.department);

  // updaters workers
  funcs.findCollection(collections, NAMES.filial).data = filials;
  return collections;
};

export let filialUpdater = {update: update};

/*
  Department -> Filial
*/