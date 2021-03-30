using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRWeb
{
    public sealed class EventMessage
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public EventMessage(string id, string title, DateTime createdDateTime) {
            Id = id;
            Title = title;
            CreatedDateTime = createdDateTime;
        }
    }
}
