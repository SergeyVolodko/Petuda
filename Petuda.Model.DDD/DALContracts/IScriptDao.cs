using System.Collections.Generic;

namespace Petuda.Model.DDD.DALContracts
{
    public interface IScriptDao: IPetudaDAO<Script>
    {
        void Save(IEnumerable<Script> scripts);
    }
}