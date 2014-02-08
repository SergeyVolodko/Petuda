using System;

namespace Petuda.Model.DDD.Exceptions
{
    public class MissingRequiredField: Exception
    {
        public String EntityName { get; set; }
        public String FieldName { get; set; }

        public MissingRequiredField(String entityName, String fieldName)
        {
            EntityName = entityName;
            FieldName = fieldName;
        }
    }
}