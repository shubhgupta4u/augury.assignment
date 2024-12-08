// Import the MongoDB client
const { MongoClient, ObjectId } = require("mongodb");

// MongoDB connection details
const url = "mongodb://localhost:27017"; // Default MongoDB URL
const dbName = "machines"; // Database name
const collectionName = "machines"; // Collection name

// Document to insert
const seedData = {
    "_id": new ObjectId("5f54dd217546020001758b7b"),
    name: "101/14 Guide roll felt",
    created_at: new Date("2020-09-06T12:59:13.055Z"),
    technicalSpecs: {
        gearbox_cfg: "None",
        installation: "Rigid",
        specific_type: "Roll",
        type: "Other",
        vfd: true,
        an: "101/14 Guide roll felt",
        drive_cfg: "Coupling Driven",
        motor_type: "Electrical",
        cfg: "Horizontal",
        design: "Centerhung",
    },
    components: [
        {
            "_id": new ObjectId("5f54dd217546020001758b7c"),
            type: "motor",
            technicalSpecs: {
                hp: 50,
                hz: "50",
                rpm: 1500,
            },
            numBearings: 2,
            bearings: [
                { name: "", description: "NDE", extended_desc: "non-driving end" },
                { name: "", description: "DE", extended_desc: "driving end" },
            ],
            powerDrives: {
                driving: [
                    {
                        "componentId": new ObjectId("5f54dd217546020001758b7d"),
                        driveType: "Coupling",
                    },
                ],
            },
        },
        {
            "_id": new ObjectId("5f54dd217546020001758b7d"),
            type: "driven_equipment",
            technicalSpecs: {},
            numBearings: 2,
            bearings: [
                { name: "", description: "DE", extended_desc: "driving end" },
                { name: "", description: "NDE", extended_desc: "non-driving end" },
            ],
            powerDrives: {
                drivenBy: [
                    {
                        "componentId": new ObjectId("5f54dd217546020001758b7c"),
                        driveType: "Coupling",
                    },
                ],
            },
        },
    ],
    updated_at: new Date("2024-03-17T12:07:09.090Z"),
    "updatedBy": new ObjectId("63da798fac2b0afe0aba2f8c")
};

// Function to seed the database
async function seedDatabase() {
    const client = new MongoClient(url);

    try {
        // Connect to MongoDB
        await client.connect();
        console.log("Connected to MongoDB");

        // Get database and collection
        const db = client.db(dbName);
        const collection = db.collection(collectionName);

        // Insert seed data
        const result = await collection.insertOne(seedData);
        console.log(`Inserted document with _id: ${result.insertedId}`);
    } catch (error) {
        console.error("Error seeding database:", error);
    } finally {
        // Close the connection
        await client.close();
    }
}

// Run the seed function
seedDatabase();
