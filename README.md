<<<<<<< HEAD
## Description
This project is a web application built with ASP.NET Core. It includes features such as user authentication, category management, and bundle management.

## Technologies Used
- ASP.NET Core
- Entity Framework Core
- AutoMapper
- JWT Authentication
- Microsoft SQL Server
- Custom Exception Handling Middleware

## Architecture
This project follows the Clean Architecture principles with four layers:
1. **Presentation Layer**: Handles the user interface and API endpoints.
2. **Service Layer**: Contains business logic and application services.
3. **Domain Layer**: Includes the core business logic and domain entities.
4. **Repository Layer**: Manages data access, external services, and other infrastructure concerns.

Additionally, the project implements the Repository and Specification patterns to manage data access and querying.


## Getting Started

### Prerequisites
- .NET 6 SDK or later
- SQL Server

### Installation
1. Clone the repository:
   git clone https://github.com/Mahmoudkhaled225/Gemini-Media-Workshop
2. Navigate to the project directory:
    cd Gemini-Media-Workshop
3. Restore the dependencies:
    dotnet restore

### Configuration

Update the appsettings.json file in the PresentationLayer project with your database connection string and JWT settings:  
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YourConnectionStringHere"
  },
  "Jwt": {
    "Issuer": "YourIssuer",
    "Audience": "YourAudience",
    "Secret": "YourSecretKey"
  },
  "Cloudinary": {
    "CloudName": "**********",
    "ApiKey": "**********",
    "ApiSecret": "**********",
    "Folder": "Gemini-Media"
  }
}
```


### Database Migration
1. Apply the database migrations:
    Don't Forget to Add Store Procedure in the Database and the trigger for the product table in the Database 
    dotnet ef database update --project RepositoryLayer

### Running the Application
1. Run the application:  
    dotnet run --project PresentationLayer
    The application will be available at https://localhost:44370 

### Documentation
1. hitting https://localhost:44370/swagger/index.html will find details Documentation for all apis 


### Usage


#### User Management  
POST /api/user/register - Register a new user  
POST /api/user/login - Login a user  

#### Category Management  
POST /api/category/add - Add a new category  
GET /api/category/getall - Get all categories  
GET /api/category/get/{id} - Get a category by ID  
PUT /api/category/update/{id} - Update a category  
DELETE /api/category/delete/{id} - Delete a category  

#### Subcategory Management   
POST /api/subcategory/add - Add a new subcategory  
GET /api/subcategory/getall - Get all subcategories  
GET /api/subcategory/get/{id} - Get a subcategory by ID  
PUT /api/subcategory/update/{id} - Update a subcategory  
DELETE /api/subcategory/delete/{id} - Delete a subcategory

#### Bundle Management   
POST /api/bundle/add - Add a new bundle  
GET /api/bundle/getall - Get all bundles  
GET /api/bundle/get/{id} - Get a bundle by ID  
PUT /api/bundle/update/{id} - Update a bundle  
DELETE /api/bundle/delete/{id} - Delete a bundle

#### product Management  
POST /api/product/add - Add a new product  
GET /api/product/getall - Get all products  
GET /api/product/get/{id} - Get a product by ID  
PUT /api/product/update/{id} - Update a product  
DELETE /api/product/delete/{id} - Delete a product
=======
## Description
This project is a web application built with ASP.NET Core. It includes features such as user authentication, category management, and bundle management.

## Technologies Used
- ASP.NET Core
- Entity Framework Core
- AutoMapper
- JWT Authentication
- Microsoft SQL Server

## Architecture
This project follows the Clean Architecture principles with four layers:
1. **Presentation Layer**: Handles the user interface and API endpoints.
2. **Serive Layer**: Contains business logic and application services.
3. **Domain Layer**: Includes the core business logic and domain entities.
4. **Repository Layer**: Manages data access, external services, and other infrastructure concerns.  

Additionally, the project implements the Repository and Specification patterns to manage data access and querying.  


## Getting Started

### Prerequisites
- .NET 6 SDK or later
- SQL Server

### Installation
1. Clone the repository:
   git clone https://github.com/Mahmoudkhaled225/Gemini-Media-Workshop
2. Navigate to the project directory:
    cd Gemini-Media-Workshop
3. Restore the dependencies:
    dotnet restore

### Configuration

Update the appsettings.json file in the PresentationLayer project with your database connection string and JWT settings:  
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YourConnectionStringHere"
  },
  "Jwt": {
    "Issuer": "YourIssuer",
    "Audience": "YourAudience",
    "Secret": "YourSecretKey"
  },
  "Cloudinary": {
    "CloudName": "**********",
    "ApiKey": "**********",
    "ApiSecret": "**********",
    "Folder": "Gemini-Media"
  }
}
```


### Database Migration
1. Apply the database migrations:
    dotnet ef database update --project RepositoryLayer

### Running the Application
1. Run the application:  
    dotnet run --project PresentationLayer
    The application will be available at https://localhost:44370 

### Documentation
1. hitting https://localhost:44370/swagger/index.html will find details Documentation for all apis 


### Usage


#### User Management  
POST /api/user/register - Register a new user  
POST /api/user/login - Login a user  

#### Category Management  
POST /api/category/add - Add a new category  
GET /api/category/getall - Get all categories  
GET /api/category/get/{id} - Get a category by ID  
PUT /api/category/update/{id} - Update a category  
DELETE /api/category/delete/{id} - Delete a category  

#### Subcategory Management   
POST /api/subcategory/add - Add a new subcategory  
GET /api/subcategory/getall - Get all subcategories  
GET /api/subcategory/get/{id} - Get a subcategory by ID  
PUT /api/subcategory/update/{id} - Update a subcategory  
DELETE /api/subcategory/delete/{id} - Delete a subcategory

#### Bundle Management   
POST /api/bundle/add - Add a new bundle  
GET /api/bundle/getall - Get all bundles  
GET /api/bundle/get/{id} - Get a bundle by ID  
PUT /api/bundle/update/{id} - Update a bundle  
DELETE /api/bundle/delete/{id} - Delete a bundle

#### product Management  
POST /api/product/add - Add a new product  
GET /api/product/getall - Get all products  
GET /api/product/get/{id} - Get a product by ID  
PUT /api/product/update/{id} - Update a product  
DELETE /api/product/delete/{id} - Delete a product
>>>>>>> 31c7196e42f9cfe90d442ceb764e67ee453696a7
