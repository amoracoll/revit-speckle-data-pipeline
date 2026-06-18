# Revit → Speckle Data Pipeline

> 🚧 Work in progress — building this in public as a learning/portfolio project.

A lightweight pipeline that extracts element data from Revit models and
publishes it to Speckle, with a simple web dashboard to visualize it.

## Why

BIM models hold a lot of valuable data, but it's often locked inside the
desktop application. This project explores a simple way to get that data
out of Revit and into a format that's easier to visualize, share, and
connect to other tools — using Speckle as the data layer.

## Architecture

```
Revit Add-in
  → Extracts model elements (walls, levels, types)
  → Converts to Speckle objects
  → Sends to a Speckle server via REST/GraphQL
  → Visible in the Speckle web viewer
```

## Local Development Setup

### Prerequisites

- Revit 2020–2025
- Docker Desktop
- Visual Studio 2022

### 1. Start the local Speckle server

A ready-to-use Docker Compose file is included in this repo:

```bash
cd local-server
docker compose up -d
```

Once running, open `http://localhost:8080`, create an account and:

1. Create a new project (stream)
2. Copy the Stream ID from the URL
3. Go to **Profile → Developer settings → New Token** and generate an API token

### 2. Build the add-in

Open `revit-addin/RevitSpeckleExporter/RevitSpeckleExporter.sln` in Visual Studio, select the configuration matching your Revit version (e.g. `R2023`) and build.

### 3. Export from Revit

Open a Revit model, go to the **RevitSpeckleExporter** ribbon tab → **Export to Speckle**, fill in:

- **Server URL**: `http://localhost:8080`
- **Stream ID**: from step 1
- **API Token**: from step 1

## Project Structure

```
├── local-server/
│   └── docker-compose.yml              # Local Speckle server (pre-built images)
└── revit-addin/
    └── RevitSpeckleExporter/
        ├── Commands/                   # IExternalCommand implementations
        ├── Models/                     # Data models (WallData, ...)
        ├── Services/                   # ElementDataExtractor, SpeckleConverter, SpeckleService
        ├── Views/                      # WPF dialogs
        ├── Resources/                  # Icons and assets
        └── App.cs                      # Ribbon registration
```

## Status

- [x] Revit add-in: basic element data extraction
- [x] Ribbon panel with Export to Speckle button
- [x] Send extracted data to Speckle
- [ ] Web dashboard: read data from Speckle
- [ ] Web dashboard: basic visualization
- [ ] Demo GIF

## Tech stack

- C# / .NET — Revit API
- Speckle SDK (`Speckle.Core`) — data transport
- [pendiente de decidir: HTML/JS, Chart.js, etc.]

## Why Speckle

Speckle is an open-source data hub for AEC, designed to break down the
data silos between BIM tools. Using it here as a way to get hands-on with
modern interoperability approaches in the industry, beyond the usual
desktop-only plugins.

---

📌 Following along? This repo will be updated as the project progresses.
