using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace Scenario
{
    /// <summary>
    /// Describes a "TODO" for a user
    /// </summary>
    public class Todo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Todo"/>
        /// </summary>
        public Todo()
        {
            Id = Guid.NewGuid();
            Description = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Todo"/>
        /// </summary>
        public Todo(string description)
            : this()
        {
            Description = description;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the todo
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the todo
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [StringLength(250, MinimumLength = 0)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the state of the todo
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TodoState State { get; set; }
    }
}
