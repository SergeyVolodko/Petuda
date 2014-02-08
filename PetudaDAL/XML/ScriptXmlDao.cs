using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Petuda.Model.DDD;
using Petuda.Model.DDD.DALContracts;
using PetudaDAL.XML.Exceptions;

namespace PetudaDAL.XML
{
    public class ScriptXmlDao: IScriptDao
    {
        private readonly XMLSerializer<Script> scriptSerializer;
        private ObservableCollection<Script> scripts;
        private const String _fileName = "Scripts.xml";
        
        public ScriptXmlDao()
        {
            scriptSerializer = new XMLSerializer<Script>(_fileName);
        }

        public Script GetByID(Guid id)
        {
            if (scripts == null)
                GetAll();

            var script = scripts.FirstOrDefault(j => j.ID == id);

            return script;
        }

        public ICollection<Script> GetAll()
        {
            try
            {
                scriptSerializer.Read(out scripts);
            }
            catch
            {
                throw new ReadFileException(_fileName);
            }

            return scripts ?? (scripts = new ObservableCollection<Script>());
        }

        public void Remove(Guid id)
        {
            if (scripts == null)
                GetAll();

            var script = scripts.FirstOrDefault(j => j.ID == id);

            scripts.Remove(script);

            SaveCollection();
        }

        public void Save(IEnumerable<Script> scripts)
        {
            foreach (var script in scripts)
            {
                SaveScriptToCollection(script);
            }

            SaveCollection();
        }

        public void Save(Script item)
        {
            SaveScriptToCollection(item);

            SaveCollection();
        }

        private void SaveScriptToCollection(Script script)
        {
            if (scripts == null)
                GetAll();

            var oldScript = scripts.FirstOrDefault(j => j.ID == script.ID);

            // update script if it already exists
            if (oldScript != null)
            {
                var oldIndex = scripts.IndexOf(oldScript);
                scripts[oldIndex] = script;
            }
            else
            {
                scripts.Add(script);
            }
        }

        private void SaveCollection()
        {
            try
            {
                 scriptSerializer.Save(scripts);
            }
            catch
            {
                throw new SaveFileException(_fileName);
            }
        }

    }//class
}//namespace