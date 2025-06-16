using System;

namespace SchematicHQ.Client.Datastream
{
    /// <summary>
    /// Specifies the JSON serialization name for an enum field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class JsonStringEnumMemberNameAttribute : Attribute
    {
        /// <summary>
        /// Gets the JSON serialization name for the enum field.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonStringEnumMemberNameAttribute"/> class
        /// with the specified name.
        /// </summary>
        /// <param name="name">The JSON serialization name for the enum field.</param>
        public JsonStringEnumMemberNameAttribute(string name)
        {
            Name = name;
        }
    }
}
