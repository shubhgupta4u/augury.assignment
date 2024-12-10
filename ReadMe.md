# Distributed Microservices Demo

This project demonstrates a scalable and loosely coupled distributed microservices architecture built with .NET Core 7, using Docker, MongoDB, and several supporting technologies to create a fully functional and resilient system.

## Key Features

- **Programming Framework**: .NET Core 7
- **Containerization**: Docker (Linux Containers)
- **Database**: MongoDB
- **API Gateway**: Ocelot
- **Load Balancing**: Round Robin
- **Rate Limiting**: Configured to manage request traffic
- **Resilience**: Implements Exponential Retry Policy
- **Response Caching**: Enhances performance by caching frequently accessed data
- **Service Discovery**: Consul
- **Microservices Architecture**: Loosely Coupled, Scalable, and Distributed
- **Design Principle**: Follows the KISS (Keep It Simple, Stupid) principle
- **Repository Design Pattern**: Applied for clean separation of concerns

## Installation & Deployment

### Prerequisites

### 1. **Docker**

Ensure Docker Desktop is installed and running on your machine. Docker is required to build and run the microservices in containers.

- **Installation**: [Docker Installation Guide](https://docs.docker.com/get-docker/)

### 2. **Node.js & npm (for data seeding)**

To seed sample data into MongoDB, you will need Node.js and npm installed.

- **Verify npm installation**:
  ```bash
  npm -v


### Database Setup & Test Data Seeding
### Step 1: Build and Deploy Services Locally Using Docker
- Remove existing containers(optional):
    ```bash
	docker-compose down
    ```

- Build and deploy services:
    ```bash
	docker-compose up --build
	```

### Step 2: Install npm Dependencies (One-Time Setup)
- Run the following bat file to install require npm packages for data_seeder.js file:
    ```bash
    install_npm_packages.bat
    ```
	
### Step 3: Seed Sample Test Data (One-Time Setup)
- After deploying the services, run the following command to seed sample JSON data into MongoDB:
    ```bash
	node data-seeder.js
    ```

## Access Points and URLs
### 1. Consul Management UI
- View service discovery details and registered microservices:
    ```bash
    http://localhost:8500
    ```
### 2. Unified API Gateway (Gateway\Augury.Gateway\Augury.Gateway\Augury.Gateway.csproj via Ocelot)

**Endpoints:**
- **Retrieve Machine Feeds:**
    ```bash
    https://localhost:5000/api/v1/machine-feeds/5f54dd217546020001758b7b
    ```
    ```bash
    https://localhost:5000/api/v1/tabular/machine-feeds/5f54dd217546020001758b7b
    ```
    
- **API Documentation:** Unified Gateway Swagger UI
    - Select **"Aggregates"** API defintion from the header right side drop down to test aggregate machine feed API endpoints
    ```bash
    https://localhost:5000/swagger
	```

### 3. Individual Microservices
Each microservice has dedicated health checks and API documentation:

#### 3.1 MachineInfoService
- **Health Check:**
    ```bash
    http://localhost:5001/health
- **API Documentation:** 
    ```bash
    http://localhost:5001/swagger

#### 3.2 MachineRepairService
- **Health Check:**
    ```bash
    http://localhost:5002/health
- **API Documentation:** 
    ```bash
    http://localhost:5002/swagger

#### 3.3 MachineTelemetryService
- **Health Check:**
    ```bash
    http://localhost:5003/health
- **API Documentation:** 
    ```bash
    http://localhost:5003/swagger

## Running microservices in local development environment
### Prerequisites

### IDE/Tool/Framework for Running microservices in local development environment

- Visual Studio 2022/Visual Studio Code/Dotnet CLI
- .Net framework 7 SDK
- Mongodb or perform "Step 1: Build and Deploy Services Locally Using Docker" mentioned above

### Open, build and Run the below .net projects by either options - Visual Studio 2022/Visual Studio Code/Dotnet CLI
- Services\MachineInfoService\MachineInfoService\MachineInfoService.csproj
- Services\MachineRepairService\MachineRepairService\MachineRepairService.csproj
- Services\MachineTelemetryService\MachineTelemetryService\MachineTelemetryService.csproj
- Gateway\Augury.Gateway\Augury.Gateway\Augury.Gateway.csproj

- **Note**
    - "Env" parmater value should be **"Staging"** in "appsettings.Development.json" file, if like to point to the backend microservices running in docker containers.
    - "Env" parmater value should be **"Development"** in "appsettings.Development.json" file, if like to point to the backend microservices when they are running on localhost.
    - Use launch profile of IISSetting or else change and protocol to match the launch profile setting used to run the Gateway Api project

## Notes
- The data-seeder.js script is used only once to populate MongoDB with sample data for testing.
- Ensure Docker is running before deploying the services.
- All microservices are exposed through the unified Ocelot API Gateway, simplifying client access.
