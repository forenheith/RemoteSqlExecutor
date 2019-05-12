using System.Collections.Generic;

namespace xlsh
{
    public class QueryResult
    {
        public List<string> HeaderRow { get; set; }
        public List<List<string>> Payload { get; set; }
        public List<List<string>> Lines
        {
            get
            {
                var l = new List<List<string>>();
                l.AddRange(Payload);
                return l;
            }
        }
    }
}