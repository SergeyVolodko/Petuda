using System;
using System.Collections.Generic;
using Petuda.Model.DDD.Exceptions;
using Petuda.Model.DDD.Helpers;

namespace Petuda.Model.DDD.Factories
{
    public class JokeFactory : IJokeFactory
    {
        public Joke Create(String name, String theme, String text, List<String> tags)
        {
            name = StringHelper.Trim(name);

            if (String.IsNullOrEmpty(name))
            {
                throw new MissingRequiredField("Joke", "Name");
            }

            theme = StringHelper.Trim(theme);
            text = StringHelper.Trim(text);

            return new Joke()
                {
                    ID = Guid.NewGuid(),
                    Name = name,
                    Theme = theme,
                    Text = text,
                    Tags = tags,
                    Date = DateTime.Now
                };
        }
    }
}