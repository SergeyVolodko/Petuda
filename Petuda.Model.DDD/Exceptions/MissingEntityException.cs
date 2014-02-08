using System;

namespace Petuda.Model.DDD.Exceptions
{
    public class MissingEntityException: Exception
    {
        public string EntityName { get; set; }
        public Guid? EntityID { get; set; }

        public MissingEntityException(string entityName, Guid? entityID )
        {
            EntityName = entityName;
            EntityID = entityID;
        }
    }
}
