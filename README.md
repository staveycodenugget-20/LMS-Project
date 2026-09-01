# MockCanvas LMS

A full-stack learning management system inspired by Canvas, built to practice C#/.NET application development, API design, database persistence, and object-oriented programming.

Project Background: This project began from instructor-provided starter files. I was tasked with completing 60+ development issues and independently planned and implemented the application's core functionality, including course management, student enrollment, assignments, submissions, modules, and role-specific menus.

## Technologies

* **C#**
* **.NET 10**
* **.NET MAUI** — cross-platform user interface
* **ASP.NET Core Web API** — server-side application layer
* **Entity Framework Core** — ORM and database access
* **Microsoft SQL Server** — persistent data storage
* **Swagger / OpenAPI** — API testing and documentation

## Features

### Student

* View enrolled courses and course content
* View modules and assignments
* Submit assignments and quizzes
* Upload files with submissions
* Participate in assignment conversations with instructors
* View course announcements
* Search course rosters

### Teacher

* Create and manage courses
* Add, edit, and remove students from course rosters
* Import/export student rosters
* Create and manage assignments
* Copy assignments between courses without copying submissions
* Import/export assignments
* Create quizzes with free-form student responses
* Manage course announcements
* Configure course letter-grade ranges
* Export course gradebooks as CSV
* Search students within course rosters
* Configure semester start and end dates

### Backend & Persistence

* RESTful Web API controllers for student and course data
* SQL Server persistence for student and course information
* Entity Framework Core migrations for database schema management
* Swagger-based API testing and exploration

## Architecture

The project separates the client UI, server-side API logic, shared models, and persistent database layer to mirror a real-world application architecture.

## Project Structure

```text
MockCanvas
  MockCanvasMauiApp1   # .NET MAUI client
  MockCanvasAPI        # ASP.NET Core Web API
  CLI.LMS              # LMS services and application logic
  UserInformation      # Shared models
```

## Current Status

The core LMS functionality and server-side persistence features are implemented. Additional features may be added as development continues.
