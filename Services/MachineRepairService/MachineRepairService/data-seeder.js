const { MongoClient, ObjectId } = require('mongodb');

// MongoDB connection URL
const url = 'mongodb://localhost:27017';  // Change this if MongoDB is running elsewhere
const dbName = 'repairs';  // Name of the MongoDB database
const collectionName = 'repairlogs';  // Collection to insert the document

// Sample repair document
const repairDocument = {
    "_id": new ObjectId("6731d639ab9cf66009b6a405"),
    "userId": new ObjectId("5f746ffb60cb73000157377f"),
    "componentId": new ObjectId("5f54dd217546020001758b7b"),
    "repair": "other",
    "text": "General",
    "summary": "We will relubricate bearing 4 today.",
    "created_at": new Date("2024-11-11T10:02:33.065Z"),
    "updated_at": new Date("2024-11-13T14:02:51.749Z"),
    "repair_date": new Date("2024-11-11T10:02:15.000Z"),
    "initial_repair_date": new Date("2024-11-11T10:02:15.000Z"),
    "repairWorkflowStatus": {
        "initialRepairStatus": "futureRepair",
        "repairStatus": "repairPendingAnalystReview"
    },
    "machine": {
        "_id": new ObjectId("5f54dd217546020001758b7b"),
        "type": "other",
        "name": "101/14 Guide roll felt",
        "imageUrl": "/images/machine_machineImage_10514_guide_roll_felt_778_1_58952066_1601112126732_8578584564b97c8dfa303fd50e8b0eb5.png"
    },
    "reason": {
        "type": "augury_recommendation"
    },
    "user": {
        "_id": new ObjectId("5f746ffb60cb73000157377f"),
        "name": "Demo - Anonymized"
    },
    "reviewStatus": "pending",
    "workorderId": "",
    "fixAreaDetails": {
        "areaType": "specific_component_part",
        "components": [
            {
                "_id": new ObjectId("5f54dd217546020001758b7d"),
                "type": "driven_equipment"
            }
        ]
    }
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
        const result = await collection.insertOne(repairDocument);
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
