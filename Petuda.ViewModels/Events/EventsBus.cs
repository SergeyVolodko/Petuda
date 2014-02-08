using System;

namespace Petuda.ViewModels.Events
{
    public class EventsBus
    {
        #region Singleton

        private static EventsBus _instance;

        public static EventsBus Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventsBus();
                }

                return _instance;
            }
        }

        private EventsBus()
        {
        }

        #endregion

        public delegate void JokeCreatedHandler(object sender, JokeEventArgs e);
        public event JokeCreatedHandler JokeCreated;

        public delegate void JokeUpdatedHandler(object sender, JokeEventArgs e);
        public event JokeUpdatedHandler JokeUpdated;

        public delegate void JokeDeletedHandler(object sender, JokeEventArgs e);
        public event JokeDeletedHandler JokeDeleted;

        public delegate void AddJokeToSelectedScriptHandler(object sender, JokeEventArgs e);
        public event AddJokeToSelectedScriptHandler AddJokeToSelectedScript;

        public delegate void ScriptCreatedHandler(object sender, ScriptEventArgs e);
        public event ScriptCreatedHandler ScriptCreated;

        public delegate void ScriptUpdatedHandler(object sender, ScriptEventArgs e);
        public event ScriptUpdatedHandler ScriptUpdated;
        
        public void RaiseJokeCreated(Guid jokeID)
        {
            if (JokeCreated != null)
            {
                JokeCreated(this, new JokeEventArgs(jokeID));
            }
        }

        public void RaiseJokeUpdated(Guid jokeID)
        {
            if (JokeUpdated != null)
            {
                JokeUpdated(this, new JokeEventArgs(jokeID));
            }
        }

        public void RaiseJokeDeleted(Guid jokeID)
        {
            if (JokeDeleted != null)
            {
                JokeDeleted(this, new JokeEventArgs(jokeID));
            }
        }

        public void RaiseAddJokeToSelectedScript(Guid jokeID)
        {
            if (AddJokeToSelectedScript != null)
            {
                AddJokeToSelectedScript(this, new JokeEventArgs(jokeID));
            }
        }

        public void RaiseScriptCreated(Guid scriptID)
        {
            if (ScriptCreated != null)
            {
                ScriptCreated(this, new ScriptEventArgs(scriptID));
            }
        }
        public void RaiseScriptUpdated(Guid scriptID)
        {
            if (ScriptUpdated != null)
            {
                ScriptUpdated(this, new ScriptEventArgs(scriptID));
            }
        }

    }//class
}//namespace