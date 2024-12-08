const machine_data_seeder_File = './Services/MachineInfoService/MachineInfoService/data-seeder.js';
const repair_data_seeder_File = './Services/MachineRepairService/MachineRepairService/data-seeder.js';
const feed_data_seeder_File = './Services/MachineTelemetryService/MachineTelemetryService/data-seeder.js';

// Import the relative file
const machine_data_seeder = require(machine_data_seeder_File);
const repair_data_seeder = require(repair_data_seeder_File);
const feed_data_seeder = require(feed_data_seeder_File);

// Command 1: MachineRepairService Data Seed
if (typeof machine_data_seeder === 'function') {
    console.log("Command 1: Executing Function");
    machine_data_seeder();
}

// Command 2: MachineInfoService Data Seed
if (typeof repair_data_seeder === 'function') {
    console.log("Command 2: Executing Function");
    repair_data_seeder();
}

// Command 3: MachineTelemetryService Data Seed
if (typeof feed_data_seeder === 'function') {
    console.log("Command 3: Executing Function");
    feed_data_seeder();
}

