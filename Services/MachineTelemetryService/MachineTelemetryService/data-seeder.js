const { MongoClient, ObjectId } = require('mongodb');

// MongoDB connection URL
const url = 'mongodb://localhost:27017';  // Change this if MongoDB is running elsewhere
const dbName = 'sessions';  // Name of the MongoDB database
const collectionName = 'feeds';  // Collection to insert the document


const feedDocument = {
    _id: new ObjectId("6728144fb3b5c98d48b2d5da"),
    userId: "",
    machineId: new ObjectId("5f54dd217546020001758b7b"),
    components: [
        {
            _id: new ObjectId("5f54dd217546020001758b7c"),
            type: "motor",
            bearings: [
                {
                    samples: ["sample_url"],
                    endpointInfo: {
                        type: "",
                        installationPlane: 0,
                        versions: {
                            hwVersion: "",
                            swVersion: ""
                        }
                    }
                },
                {
                    samples: ["sample_url"],
                    endpointInfo: {
                        type: "",
                        installationPlane: 0,
                        versions: {
                            hwVersion: "",
                            swVersion: ""
                        }
                    }
                }
            ]
        }
    ],
    created_at: new Date(1730678400000),
    updated_at: new Date(1730702268000),
    continuous: true,
    status: "approved",
    status_updated_at: new Date(1730702268000),
    enableInterim: false,
    tags: ["topShelf"]
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
        const result = await collection.insertOne(feedDocument);
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
