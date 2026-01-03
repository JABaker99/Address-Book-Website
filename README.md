# Baker Address Book

The Baker Address Book is an ASP.NET Core web application that allows users to manage personal contacts with full create, read, update, and delete (CRUD) functionality. Contacts can be categorized, viewed in a sortable list, and stored persistently using Entity Framework Core with a relational database.

## Features
- Add, edit, and delete contacts
- View all saved contacts in a clean table layout
- Sort contacts by name or category
- Organize contacts into predefined categories (Family, Friend, Work)
- Persistent storage using Entity Framework Core
- Database seeding with sample data for immediate use

## Technical Overview
- **Framework:** ASP.NET Core
- **Language:** C#
- **ORM:** Entity Framework Core
- **Database:** SQL Server (Visual Studio Enterprise)
- **Architecture:** MVC with DbContext-driven data access

## Data Model
### Contacts
Each contact stores:
- First Name
- Last Name
- Optional Nickname
- Phone Number
- Category association
- Date Created

### Categories
Pre-seeded categories include:
- Family
- Friend
- Work

## Sample Seed Data
The database initializes with example contacts and categories to demonstrate application functionality immediately upon launch.
