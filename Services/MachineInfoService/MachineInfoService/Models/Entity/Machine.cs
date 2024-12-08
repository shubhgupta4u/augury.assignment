namespace Augury.MachineInfoService.Models.Entity
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;
    using System.Collections.Generic;

    public class Machine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("technicalSpecs")]
        public MachineTechnicalSpecs TechnicalSpecs { get; set; }

        [BsonElement("components")]
        public List<Component> Components { get; set; }

        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("updatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UpdatedBy { get; set; }
    }

    public class TechnicalSpecs
    {
    }

    public class MachineTechnicalSpecs: TechnicalSpecs
    {
        [BsonElement("gearbox_cfg")]
        public string GearboxConfig { get; set; }

        [BsonElement("installation")]
        public string Installation { get; set; }

        [BsonElement("specific_type")]
        public string SpecificType { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("vfd")]
        public bool VFD { get; set; }

        [BsonElement("an")]
        public string An { get; set; }

        [BsonElement("drive_cfg")]
        public string DriveConfig { get; set; }

        [BsonElement("motor_type")]
        public string MotorType { get; set; }

        [BsonElement("cfg")]
        public string Config { get; set; }

        [BsonElement("design")]
        public string Design { get; set; }
    }

    public class ComponentTechnicalSpecs: TechnicalSpecs
    {

        [BsonElement("hp")]
        public int Hp { get; set; }

        [BsonElement("hz")]
        public int Hz { get; set; }

        [BsonElement("rpm")]
        public int Rpm { get; set; }
    }

    public class Component
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("technicalSpecs")]
        public ComponentTechnicalSpecs ComponentTechnicalSpecs { get; set; }

        [BsonElement("numBearings")]
        public int NumBearings { get; set; }

        [BsonElement("bearings")]
        public List<Bearing> Bearings { get; set; }

        [BsonElement("powerDrives")]
        public PowerDrives PowerDrives { get; set; }
    }

    public class Bearing
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("extended_desc")]
        public string ExtendedDescription { get; set; }
    }

    public class PowerDrives
    {
        [BsonElement("driving")]
        public List<DriveDetail> Driving { get; set; }

        [BsonElement("drivenBy")]
        public List<DriveDetail> DrivenBy { get; set; }
    }

    public class DriveDetail
    {
        [BsonElement("componentId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ComponentId { get; set; }

        [BsonElement("driveType")]
        public string DriveType { get; set; }
    }

}
