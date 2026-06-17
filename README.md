# Revit → Speckle Data Pipeline

> 🚧 Work in progress — building this in public as a learning/portfolio project.

A lightweight pipeline that extracts element data from Revit models and 
publishes it to Speckle, with a simple web dashboard to visualize it.

## Why

BIM models hold a lot of valuable data, but it's often locked inside the 
desktop application. This project explores a simple way to get that data 
out of Revit and into a format that's easier to visualize, share, and 
connect to other tools — using Speckle as the data layer.

## Planned architecture

1. **Revit add-in** (C#) — extracts data from selected elements (category, 
   key parameters, quantities)
2. **Speckle** — acts as the data layer between Revit and the web
3. **Web dashboard** — reads data from Speckle and displays it

## Status

- [ ] Revit add-in: basic element data extraction
- [ ] Send extracted data to Speckle
- [ ] Web dashboard: read data from Speckle
- [ ] Web dashboard: basic visualization
- [ ] Demo GIF

## Tech stack

- C# / .NET — Revit API
- Speckle (GraphQL API) — data transport
- [pendiente de decidir: HTML/JS, Chart.js, etc.]

## Why Speckle

Speckle is an open-source data hub for AEC, designed to break down the 
data silos between BIM tools. Using it here as a way to get hands-on with 
modern interoperability approaches in the industry, beyond the usual 
desktop-only plugins.

---

📌 Following along? This repo will be updated as the project progresses.
