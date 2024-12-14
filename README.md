 

# **Movie Backend API**

This project is a backend system for managing movie data and user interactions. It includes features such as CRUD operations, user authentication, movie search and filtering, and integration with external APIs (e.g., TMDB). The backend is built using **ASP.NET Core** and follows modern development practices.

---

## **Features**

### **1. Endpoints**
- **Movies**:
  - CRUD operations (Create, Read, Update, Delete) on movie data.
- **Movie Details**:
  - Fetch detailed information about individual movies.
- **Search**:
  - Search for movies based on various criteria like title, genre, or release date.
- **User Authentication**:
  - Secure user login and registration functionality.

### **2. Functionality**
- **Data Integration**:
  - Fetch and manage movie data from the TMDB (The Movie Database) API.
- **Search and Filtering**:
  - Implement dynamic search and filtering capabilities for movies.
- **User Management**:
  - Handle user authentication and session management with JWT.

### **3. Security**
- **Authentication**:
  - All sensitive endpoints are secured with JWT-based authentication.
  - Custom unauthorized and forbidden error messages are implemented for user feedback.
- **Input Validation**:
  - DTOs include data annotations for validating user inputs (e.g., `Required`, `StringLength`, `Range`).

### **4. Integration**
- Compatible with frontend systems for dynamic movie data display and user interactions.
- External API integration with TMDB to fetch popular movies, movie details, and support search functionality.

---

## **Technologies Used**
- **ASP.NET Core 6**
- **Entity Framework Core**
- **JWT Authentication**
- **SQL Server** (Database)
- **TMDB API** (Data integration)
- **Swashbuckle/Swagger** (API documentation)
- **Dependency Injection** for services
- **HttpClient** for API calls
- **Data Validation** with data annotations

---

## **Endpoints**

### **Movies**
- **GET** `/api/films`  
  Fetch all movies.

- **GET** `/api/films/{id}`  
  Fetch details of a specific movie by its ID.

- **POST** `/api/films`  
  Create a new movie (requires authentication).

- **PUT** `/api/films/{id}`  
  Update an existing movie by ID (requires authentication).

- **DELETE** `/api/films/{id}`  
  Delete a movie by ID (requires authentication).

### **Movie Details**
- **GET** `/api/films/details/{id}`  
  Fetch detailed information about a specific movie from the TMDB API.

### **Search**
- **GET** `/api/films/search?query={query}`  
  Search for movies by title or criteria.

### **Popular Films**
- **GET** `/api/films/popular`  
  Fetch a list of popular films from the TMDB API.

### **Authentication**
- **POST** `/api/auth/register`  
  Register a new user.

- **POST** `/api/auth/login`  
  Log in and retrieve a JWT token.

---

## **How It Works**

### **1. Authentication**
- Users must log in to access sensitive endpoints such as creating, updating, or deleting movies.
- A JWT token is issued upon login and must be included in the `Authorization` header for authenticated requests:
  ```
  Authorization: Bearer <token>
  ```

### **2. Data Integration**
- Integrated with the TMDB API to fetch:
  - Popular movies.
  - Movie details by ID.
  - Search results based on query strings.
- External data is fetched using `HttpClient`.

### **3. Data Validation**
- DTOs are used for input validation to ensure secure and clean data handling.
- Example DTO:
  ```csharp
  public class FilmCreateDto
  {
      [Required]
      [StringLength(100)]
      public string Title { get; set; }

      [Required]
      public string Genre { get; set; }

      [Required]
      public DateTime ReleaseDate { get; set; }

      [Range(0, 10)]
      public double Rating { get; set; }

      [StringLength(500)]
      public string Overview { get; set; }
  }
  ```

### **4. Error Handling**
- Custom error messages are implemented for unauthorized (`401`) and forbidden (`403`) responses.
- Example unauthorized response:
  ```json
  {
      "Message": "You must log in to access this resource."
  }
  ```

---

## **Setup Instructions**

### **1. Clone the Repository**
```bash
git clone https://github.com/yourusername/movie-backend-api.git
cd movie-backend-api
```

### **2. Configure the Environment**
- Add your TMDB API key and other configurations in `appsettings.json`:
  ```json
  {
      "ConnectionStrings": {
          "Default": "Server=YOUR_SERVER;Database=Movies;Trusted_Connection=True;"
      },
      "TokenKey": "your_jwt_secret_key_here",
      "Tmdb": {
          "ApiKey": "your_tmdb_api_key_here",
          "BaseUrl": "https://api.themoviedb.org/3/"
      }
  }
  ```

### **3. Run Migrations**
Run the following commands to set up the database:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### **4. Run the Application**
Start the application:
```bash
dotnet run
```

Navigate to:
- API: `https://localhost:5001/swagger`
- Swagger Documentation: `https://localhost:5001/swagger/index.html`

---

## **Future Improvements**
- Implement role-based access control (e.g., admin privileges).
- Add caching for frequently accessed TMDB data.
- Improve search functionality to support filtering by genre, release date, etc.

---

## **Contributions**
Contributions are welcome! Please fork this repository and create a pull request for any features or fixes.

---

## **License**
This project is licensed under the MIT License. See the `LICENSE` file for details.
 
