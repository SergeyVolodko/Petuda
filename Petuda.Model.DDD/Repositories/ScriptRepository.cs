using System;
using System.Linq;
using System.Collections.Generic;
using Petuda.Model.DDD.DALContracts;

namespace Petuda.Model.DDD.Repositories
{
    public class ScriptRepository : IScriptRepository
    {
        private IScriptDao _scriptDao;

        public ScriptRepository(IScriptDao _scriptDao)
        {
            this._scriptDao = _scriptDao;
        }

        public void Save(Script item)
        {
            _scriptDao.Save(item);
        }

        public ICollection<Script> LoadAll()
        {
            return _scriptDao.GetAll();
        }

        public Script Load(Guid id)
        {
            return _scriptDao.GetByID(id);
        }

        public void Remove(Guid id)
        {
            _scriptDao.Remove(id);
        }

        public IEnumerable<Script> GetScriptsThatContainsJokeID(Guid id)
        {
            var scripts = _scriptDao.GetAll();
            var result = scripts.Where(s => s.JokesIDs.Contains(id));

            return result;
        }

        public void Save(IEnumerable<Script> scripts)
        {
            _scriptDao.Save(scripts);
        }
    }//class
}//namespace