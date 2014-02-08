using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Petuda.Model.DDD
{
    [Serializable]
    [XmlRoot("Joke")]
    public class Joke : ICloneable
    {
        [XmlAttribute]
        public Guid ID { get; set; }
        public String Name { get; set; }
        public String Theme { get; set; }
        public String Text { get; set; }
        public List<String> Tags { get; set; }
        public DateTime Date { get; set; }
        
        public object Clone()
        {
            return new Joke
            {
                ID = this.ID,
                Name = this.Name,
                Theme = this.Theme,
                Text = this.Text,
                Tags = this.Tags,
                Date = this.Date
            };
        }
    }
}