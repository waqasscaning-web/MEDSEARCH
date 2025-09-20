# MEDSEARCH

A complete web application for searching and managing medicines (Online Med Search).  
Includes features, design docs, and source code in ASP.NET MVC.

---

## Table of Contents

1. [Overview](#overview)  
2. [Features](#features)  
3. [Tech Stack](#tech-stack)  
4. [Getting Started](#getting-started)  
5. [Project Structure](#project-structure)  
6. [Controllers / Key Components](#controllers--key-components)  
7. [Documentation](#documentation)  
8. [Database Setup](#database-setup)  
9. [Contributing](#contributing)  
10. [License](#license)

---

## Overview

MEDSEARCH is an ASP.NET MVC 5 web application for searching medicines online.  
It allows user authentication, medicine management (CRUD operations), image uploads, role-based access, and more.  

---

## Features

- Medicine search (by name/keyword)  
- Add, update, delete medicines  
- Manage medicine images  
- User registration/login + role-based access  
- Clean MVC architecture  
- Responsive UI using Bootstrap / jQuery  

---

## Tech Stack

- ASP.NET MVC 5, .NET Framework (version used in the project)  
- Entity Framework for ORM / database access  
- ASP.NET Identity + OWIN for authentication & authorization  
- Bootstrap, jQuery, Modernizr for front-end UI  
- SQL Server / LocalDB for database  

---

## Getting Started

### Prerequisites

- Visual Studio 2019/2022 or newer with ASP.NET and Web Development workload  
- SQL Server (LocalDB or full instance)  
- .NET Framework installed (ensure matches project target)  

### Setup

1. Clone this repository:  
   ```bash
   git clone https://github.com/waqasscaning-web/MEDSEARCH.git
MEDSEARCH/
├── OMS/                 ← Main MVC project
│   ├── Controllers/     ← Controller classes
│   ├── Models/          ← Data models
│   ├── Views/           ← Razor view files (UI)
│   ├── Content/         ← CSS, images
│   ├── Scripts/         ← JavaScript files
│   ├── Startup.cs       ← OWIN startup config
│   ├── Web.config       ← Main configuration file
│   └── …                ← Other supporting files
├── OMS.sln              ← Visual Studio solution
├── README.md            ← This documentation
├── .gitignore           ← Files/folders to ignore
└── Documentation/       ← SRS, SDD, Final Report
