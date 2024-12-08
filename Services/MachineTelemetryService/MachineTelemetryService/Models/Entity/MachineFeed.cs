namespace Augury.RepairTelemetryService.Models.Entity
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    public class MachineFeed
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("machineId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MachineId { get; set; }

        [BsonElement("components")]
        public List<Component> Components { get; set; }

        [BsonElement("created_at")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updated_at")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("continuous")]
        public bool Continuous { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("status_updated_at")]
        public DateTime StatusUpdatedAt { get; set; }

        [BsonElement("enableInterim")]
        public bool EnableInterim { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; }
    }

    public class Component
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("bearings")]
        public List<Bearing> Bearings { get; set; }
    }

    public class Bearing
    {
        [BsonElement("samples")]
        public List<string> Samples { get; set; }

        [BsonElement("endpointInfo")]
        public EndpointInfo EndpointInfo { get; set; }
    }

    public class EndpointInfo
    {
        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("installationPlane")]
        public int InstallationPlane { get; set; }

        [BsonElement("versions")]
        public VersionInfo Versions { get; set; }
    }

    public class VersionInfo
    {
        [BsonElement("hwVersion")]
        public string HwVersion { get; set; }

        [BsonElement("swVersion")]
        public string SwVersion { get; set; }
    }

}
