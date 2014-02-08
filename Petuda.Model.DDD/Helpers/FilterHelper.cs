using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Petuda.Model.DDD.Helpers
{
    public static class FilterHelper
    {
        public static bool TextFilterPassed(string text, Joke joke)
        {
            //Remove redundant spaces from text filter
            var textFilter = Regex.Replace(StringHelper.Trim(text), @"\s+", " ").ToLower();

            if (String.IsNullOrEmpty(textFilter) || textFilter.Length < 3)
                return true;

            var words = textFilter.Split(StringHelper.DelimiterChars);

            return words.All(word => NameAndJokeTextFilterPassed(joke, word.ToLower()) ||
                                     TagsFilterPassed(joke.Tags, word.ToLower()));
        }

        private static bool NameAndJokeTextFilterPassed(Joke joke, string word)
        {
            return (joke.Text != null && joke.Text.ToLower().Contains(word)) ||
                    joke.Name.ToLower().Contains(word);
        }

        // tag parameter is pessed in the lower case.
        private static bool TagsFilterPassed(List<string> tags, string tag)
        {
            if (tags == null || tags.Count == 0)
            {
                return false;
            }

            var foundTag = tags.FirstOrDefault(t => t.ToLower().Contains(tag));

            return foundTag != null;
        }

        public static bool DateFromFilterPassed(DateTime? filter, DateTime jokeDate)
        {
            if (!filter.HasValue)
                return true;

            return jokeDate >= filter.Value.Date;
        }

        public static bool DateToFilterPassed(DateTime? filter, DateTime jokeDate)
        {
            if (!filter.HasValue)
                return true;

            return jokeDate <= filter.Value.Date;
        }

        public static bool ThemeFilterPassed(String filter, String jokeTheme)
        {
            return String.IsNullOrEmpty(filter) || jokeTheme == filter;
        }

        //private bool LeagueFilterPassed(Joke joke)
        //{
        //    if (String.IsNullOrEmpty(LeagueFilter) && !IsNeverUsed)
        //        return true;

        //    var leagues = Cache.GetJokeLeagues(joke.ID);

        //    if (IsNeverUsed)
        //    {
        //        return leagues.Count == 0;
        //    }

        //    return leagues.Contains(LeagueFilter);
        //}

    }
}