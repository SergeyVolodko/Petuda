using System;
using System.Collections.Generic;
using System.Linq;
using Petuda.Model.DDD.Exceptions;
using Petuda.Model.DDD.Factories;
using Petuda.Model.DDD.Repositories;

namespace Petuda.Model.DDD.Services
{
    public class ScriptService : IScriptService
    {
        private readonly IScriptFactory scriptFactory;
        private readonly IScriptRepository scriptRepository;
        //private IJokeRepository jokeRepository;

        public ScriptService(IScriptRepository scriptRepository, IScriptFactory scriptFactory/*, IJokeRepository jokeRepository*/)
        {
            this.scriptRepository = scriptRepository;
            this.scriptFactory = scriptFactory;
            //this.jokeRepository = jokeRepository;
        }
        
        public ICollection<Script> LoadAllScripts()
        {
            try
            {
                return this.scriptRepository.LoadAll();
            }
            catch
            {
                throw;
            }
        }

        public Script CreateScript(string name,/* string league,*/ DateTime? gameDate)
        {
            var script = scriptFactory.Create(name,/* league,*/ gameDate);

            this.scriptRepository.Save(script);

            return script;
        }

        public void UpdateScript(Script script)
        {
            try
            {
                this.scriptRepository.Save(script);
            }
            catch
            {
                throw;
            }
        }

        public void RemoveScript(Guid scriptID)
        {
            try
            {
                this.scriptRepository.Remove(scriptID);
            }
            catch
            {
                throw;
            }
        }

        public void AddJokeToScript(Guid scriptId, Guid jokeId, int? index = null)
        {
            var script = this.scriptRepository.Load(scriptId);
            if (script == null)
                throw new MissingEntityException("script", scriptId);

            if (index.HasValue)
            {
                script.AddJoke(jokeId, index.Value);
            }
            else
            {
                script.AddJoke(jokeId);
            }
            

            this.scriptRepository.Save(script);
        }
        
        public void RemoveJokeFromScripts(Guid jokeId)
        {
            var scripts = this.scriptRepository.GetScriptsThatContainsJokeID(jokeId);

            if (scripts == null)
            {
                throw new MissingEntityException("scripts", null);
            }

            if (scripts.Any(s => !s.IsEditable))
            {
                throw new JokeCantBeDeletedException();
            }

            foreach (var script in scripts)
            {
                script.RemoveJoke(jokeId);
            }

            this.scriptRepository.Save(scripts);
        }

        public void RemoveJokeFromScript(Guid scriptId, Guid jokeId)
        {
            var script = this.scriptRepository.Load(scriptId);
            if (script == null)
                throw new MissingEntityException("script", null);
            
            script.RemoveJoke(jokeId);

            this.scriptRepository.Save(script);
        }

        public void MoveJokeInScriptIndex(Guid scriptId, int prevIndex, int? newIndex)
        {
            var script = this.scriptRepository.Load(scriptId);
            if (script == null)
                throw new MissingEntityException("script", null);

            script.MoveJoke(prevIndex, newIndex);

            this.scriptRepository.Save(script);
        }
    }//class
}//namespace