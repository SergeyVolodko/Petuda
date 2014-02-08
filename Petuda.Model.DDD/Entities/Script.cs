using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Petuda.Model.DDD.Exceptions;

namespace Petuda.Model.DDD
{
    [Serializable]
    [XmlRoot("Script")]
    public class Script : ICloneable
    {
        [XmlAttribute]
        public Guid ID { get; set; }

        public String Name { get; set; }
        public String League { get; set; }
        public DateTime? GameDate { get; set; }
        public List<Guid> JokesIDs { get; set; }

        public bool IsEditable
        {
            get { return this.GameDate.HasValue ? this.GameDate.Value.Date >= DateTime.Now.Date : true; }
        }

        public bool ContainsJokeId(Guid jokeID)
        {
            return this.JokesIDs.Contains(jokeID);
        }

        public void AddJoke(Guid jokeID)
        {
            if (this.ContainsJokeId(jokeID))
            {
                throw new ScriptAlreadyContainsJokeException(this.JokesIDs.IndexOf(jokeID) + 1);
            }

            JokesIDs.Add(jokeID);
        }

        public void AddJoke(Guid jokeID, int index)
        {
            if (this.ContainsJokeId(jokeID))
            {
                throw new ScriptAlreadyContainsJokeException(this.JokesIDs.IndexOf(jokeID) + 1);
            }

            if (index < 0)
            {
                index = 0;
            }

            if (index > JokesIDs.Count - 1)
            {
                AddJoke(jokeID);
            }
            else
            {
                JokesIDs.Insert(index, jokeID);
            }
        }

        public void RemoveJoke(Guid jokeID)
        {
            JokesIDs.Remove(jokeID);
        }

        public void MoveJoke(int prevIndex, int? newIndex)
        {
            if (!newIndex.HasValue)
            {
                newIndex = this.JokesIDs.Count - 1;
            }

            if (newIndex < 0 || newIndex > this.JokesIDs.Count - 1 ||
                prevIndex < 0 || prevIndex > this.JokesIDs.Count - 1)
            {
                return;
            }

            if (prevIndex == newIndex)
                return;

            var di = 0;
            if (prevIndex < newIndex.Value)
            {
                di = 1;
            }

            MoveJokeID(prevIndex, newIndex.Value + di);
        }

        private void MoveJokeID(int prevIndex, int newIndex)
        {
            var jokeID = this.JokesIDs[prevIndex];

            this.JokesIDs.Insert(newIndex, jokeID);

            if (prevIndex > newIndex)
            {
                this.JokesIDs.RemoveAt(prevIndex + 1);
            }
            else
            {
                this.JokesIDs.RemoveAt(prevIndex);
            }
        }
        
        public object Clone()
        {
            return new Script()
            {
                ID = this.ID,
                Name = this.Name,
                League = this.League,
                GameDate = this.GameDate,
                JokesIDs = this.JokesIDs
            };
        }
    }
}