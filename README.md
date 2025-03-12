# Course Enrollment System (CES)
***
### A web-based application built with ASP.NET Core MVC that manages student enrollments in courses.
***
# Update: now you can use the container instead of running the application locally
## How to Run the container
1. Pull the image from docker hub
```bash
docker pull mohamedwaelelteir/ces
```
2. Run the container
```bash
docker run -d -p 8080:80 mohamedwaelelteir/ces
```
3. Open your browser and navigate to `http://localhost:8080`

***
### Features:
- **Student Management**
    - Create, view, edit, and delete student records
    - Validate unique email addresses and national IDs
    - Optional phone number field
    - Track student details including full name, date of birth, and contact information

- **Course Management**
    - Create, view, edit, and delete courses
    - Set maximum capacity for each course
    - Optional course descriptions
    - Track available slots dynamically
  

- **Enrollment Management**
    - Enroll students in courses
    - Prevent duplicate enrollments
    - Real-time available slots checking
    - View all enrollments with student and course details
  

***
### Implemented pagination defaults to 5 rows per page
***
## Technology Stack

- ASP.NET Core MVC 9.0
- Entity Framework Core
- JavaScript/jQuery

## Database
### Database is created using Entity Framework Core in-memory database for demonstration purposes.

## How to Run

1. Clone the repository
```bash
git clone https://github.com/MohamedWElteir/CES.git
```
2. Open the solution in your preferred IDE (Visual Studio, Rider, etc.)
3. run the application
```bash
dotnet run
```

### Note: The application is configured to use an in-memory database by default.

By default, you will see two students and two courses in the database, with two initial enrollments. That's thanks to the database seeding done in the `DbContext`. You can add more students and courses as needed.
