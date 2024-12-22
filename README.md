# Event System API 🎟️  

Welcome to the **Event System API**, a robust platform built with .NET Core and Clean Architecture to manage events and tickets efficiently. This API empowers businesses to create and publish events, while enabling users to browse, purchase, and manage their tickets seamlessly.

## Features ✨  
- **Role-Based Access Control**:  
  - 🛠️ **Admin**: Manage roles, users, and oversee events.  
  - 🎤 **Event Organizer**: Create, update, and manage events.  
  - 🛒 **Customer**: Browse and purchase tickets, view past events.  
- **Event & Ticket Management**: Automatically generate tickets with the "Available" status upon event creation.  
- **Scalable Data Handling**: Efficiently handles data interactions with **Entity Framework**.  

---

## Tech Stack 🛠️  
- **Framework**: .NET Core  
- **Architecture**: Clean Architecture  
- **Database**: SQL Server (via Entity Framework)  
- **Authentication**: Token-based (JWT)  
- **Tools**: Visual Studio, Swagger for API documentation  

---

## Installation 🚀  

1. **Clone the repository**:  
   ```bash
   git clone https://github.com/FrancoBerlochi/EventManagerApi.git
   
2. **Set up the database**
   dotnet ef database update

3. **Start the API**
   dotnet run

4. Access the API documentation:
Navigate to http://localhost:5000/swagger.
