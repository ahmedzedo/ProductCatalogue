using System.Collections.Generic;

namespace ProductCatalogue.Application.Common.Models
{
    public class Errors
    {
        public Dictionary<string, string[]> Items { get; private set; }

        public bool IsValid => Items.Count <= 0;

        public void AddError(string Name, string[] Message)
        {
            Items.Add(Name, Message);
        }
        public void AddErrors(Dictionary<string, string[]> errors)
        {
            Items = errors;
        }
        public Errors(Dictionary<string, string[]> errors = default)
        {
            Items = errors ?? new Dictionary<string, string[]>();
        }
    }
}
