namespace Augury.RepairTelemetryService.Models.Entity
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    public class RepairLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("componentId")]
        public string ComponentId { get; set; }
        [BsonElement("repair")]
        public string Repair { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("summary")]
        public string Summary { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement("repair_date")]
        public DateTime RepairDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement("initial_repair_date")]
        public DateTime InitialRepairDate { get; set; }
        [BsonElement("repairWorkflowStatus")]
        public RepairWorkflowStatus RepairWorkflowStatus { get; set; }
        [BsonElement("machine")]
        public MachineInfo Machine { get; set; }
        [BsonElement("user")]
        public User User { get; set; }
        [BsonElement("reason")]
        public Reason Reason { get; set; }
        [BsonElement("reviewStatus")]
        public string ReviewStatus { get; set; }
        [BsonElement("workorderId")]
        public string WorkOrderId { get; set; }
        [BsonElement("fixAreaDetails")]
        public FixAreaDetails FixAreaDetails { get; set; }
    }

    public class RepairWorkflowStatus
    {
        [BsonElement("initialRepairStatus")]
        public string InitialRepairStatus { get; set; }
        [BsonElement("repairStatus")]
        public string RepairStatus { get; set; }
    }

    public class MachineInfo
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }
    }

    public class Reason
    {
        [BsonElement("type")]
        public string Type { get; set; }
    }

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
    }

    public class FixAreaDetails
    {
        [BsonElement("areaType")]
        public string AreaType { get; set; }
        [BsonElement("components")]
        public List<Component> Components { get; set; }
    }

    public class Component
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
    }


}
