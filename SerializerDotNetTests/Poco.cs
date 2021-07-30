﻿using System.Runtime.Serialization;

namespace SerializerDotNetTests
{
    [DataContract]
    public class Poco
    {
        public Poco()
        {
            Id = int.MaxValue;
            Name = "I'm in love with the POCO";
        }

        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }
    }
}
