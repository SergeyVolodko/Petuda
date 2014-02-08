using System;

namespace Petuda.ViewModels.Events
{
    public class ScriptEventArgs : EventArgs
    {
        private readonly Guid _scriptID;

        public Guid ScriptID
        {
            get { return _scriptID; }
        }

        public ScriptEventArgs(Guid scriptID)
        {
            _scriptID = scriptID;
        }
    }
}