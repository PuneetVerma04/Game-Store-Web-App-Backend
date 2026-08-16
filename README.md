# Aphelion Backend

Frontend: [Aphelion-Frontend](https://github.com/PuneetVerma04/Aphelion-Frontend)

A comprehensive ASP.NET Core Web API backend for a Steam-like game store application. This project provides a full-featured API for managing games, users, shopping carts, orders, reviews, coupons, and analytics.

## 🚀 Features

### Core Functionality

- **User Management**: Registration, authentication, and role-based authorization (Player, Publisher, Admin)
- **Game Catalog**: Complete CRUD operations for game management with pagination, filtering by genre and price
- **Shopping Cart**: Add and manage game purchases with real-time price calculation
- **Order Processing**: Complete order management system with order history and tracking
- **Review System**: User reviews and ratings for games with validation
- **Coupon System**: Discount codes and promotional offers
- **Analytics Dashboard**: Revenue tracking, game performance metrics, and sales analytics

### Security & Authentication

- JWT-based authentication with secure token management
- Role-based authorization (Player, Publisher, Admin)
- Password hashing using BCrypt
- Secure API endpoints with proper middleware
- Input validation using FluentValidation
- Exception handling middleware for consistent error responses

## 🛠 Technology Stack

- **.NET 9.0**: Latest .NET framework
- **ASP.NET Core Web API**: RESTful API framework
- **Entity Framework Core 9.0**: ORM for database operations with migrations
- **SQL Server**: Database management system
- **AutoMapper 12.0**: Object-to-object mapping
- **JWT Bearer Tokens**: Authentication mechanism
- **BCrypt.Net-Next**: Secure password hashing
- **FluentValidation**: Input validation

## 📁 Project Structure

```
AphelionBackend/
├── Controllers/          # API controllers
│   ├── AuthController.cs
│   ├── GamesController.cs
│   ├── UsersController.cs
│   ├── CartController.cs
│   ├── OrderController.cs
│   ├── ReviewController.cs
│   ├── CouponController.cs
│   └── AnalyticsController.cs
├── DTOs/                 # Data Transfer Objects
│   ├── Auth/
│   ├── Game/
│   ├── User/
│   ├── Cart/
│   ├── Order/
│   ├── Review/
│   ├── Coupon/
│   └── Analytics/
├── Entities/             # Domain models
│   ├── User.cs
│   ├── Game.cs
│   ├── CartItem.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   ├── Review.cs
│   ├── Coupon.cs
│   └── BackendDbContext.cs
├── Services/             # Business logic layer
│   ├── Interfaces/
│   ├── UserService.cs
│   ├── GameService.cs
│   ├── CartService.cs
│   ├── OrderService.cs
│   ├── ReviewService.cs
│   ├── CouponService.cs
│   ├── AnalyticsService.cs
│   └── JwtService.cs
├── Profiles/             # AutoMapper profiles
│   ├── CartProfile.cs
│   ├── CouponsProfile.cs
│   ├── GameProfile.cs
│   ├── OrderProfile.cs
│   └── ReviewProfile.cs
├── Validators/           # FluentValidation validators
├── Middleware/           # Custom middleware
│   └── ExceptionHandlingMiddleware.cs
├── Extensions/           # Extension methods
│   └── DbSeeder.cs
├── Settings/             # Configuration settings
├── Migrations/           # EF Core migrations
└── Properties/           # Launch settings
```

## 🔧 Installation & Setup

### Prerequisites

- .NET 9.0 SDK or later
- SQL Server (LocalDB or full version)
- Visual Studio 2022 or VS Code with C# extension
- Git (for cloning the repository)

### Steps

1. **Clone the repository**

   ```bash
   git clone https://github.com/PuneetVerma04/Aphelion-Backend.git
   cd Aphelion-Backend
   ```

2. **Restore packages**

   ```bash
   dotnet restore
   ```

3. **Configure database connection**
   Update the connection string in `appsettings.json` or `appsettings.Development.json`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AphelionDB;Trusted_Connection=true;"
     }
   }
   ```

4. **Configure JWT settings**
   Set up JWT configuration in `appsettings.json`:

   ```json
   {
     "JwtSettings": {
       "Key": "your-secret-key-minimum-32-characters",
       "Issuer": "AphelionAPI",
       "Audience": "AphelionClient",
       "ExpirationInMinutes": 60
     }
   }
   ```

5. **Apply database migrations**

   ```bash
   dotnet ef database update --project AphelionBackend
   ```

6. **Run the application**

   ```bash
   dotnet run --project AphelionBackend
   ```

7. **Access API documentation**
   - Development: `https://localhost:7044/swagger`
   - HTTP: `http://localhost:5062/swagger`

## 📚 API Endpoints

### Authentication (`/store/auth`)

- `POST /store/auth/register` - User registration (defaults to `Player` role, returns a JWT token)
- `POST /store/auth/login` - User login (returns JWT token, username, and role)

### Games (`/store/games`)

- `GET /store/games` - Get all games with pagination, filtering, and sorting (Anonymous)
  - Query params: `pageNumber`, `pageSize`, `genre`, `minPrice`, `maxPrice`, `searchTerm`, `publisherId`, `releaseDateFrom`, `releaseDateTo`, `sortBy`, `sortOrder`
- `GET /store/games/all` - Legacy, unpaginated listing (Anonymous, deprecated — query params: `genre`, `maxPrice`)
- `GET /store/games/{id}` - Get game by ID (Anonymous)
- `POST /store/games` - Create new game (Publisher/Admin only)
- `PATCH /store/games/{id}` - Partially update a game (Publisher/Admin only; publishers may only edit their own games)
- `DELETE /store/games/{id}` - Delete game (Admin only)

### Users (`/store/users`)

- `GET /store/users` - Get all users, optional `?username=` filter (Admin only)
- `GET /store/users/{id}` - Get user by ID (self or Admin only)
- `PUT /store/users/{id}` - Update user profile (self or Admin only)
- `DELETE /store/users/{id}` - Delete user (Admin only)

### Cart (`/store/cart`)

All cart routes act on the authenticated user (from the JWT) and require the `Player` role — there is no `{userId}` in the URL.

- `GET /store/cart` - Get the current user's cart items
- `POST /store/cart/add` - Add a game to the cart (or increase quantity if already present)
- `PATCH /store/cart/update` - Update an item's quantity

There is currently no endpoint to remove a single item or clear the whole cart directly — the cart is only cleared automatically as part of checkout.

### Orders (`/store/orders`)

- `GET /store/orders` - Get the current user's orders, or all orders if Admin
- `POST /store/orders/checkout` - Create a new order from the current cart contents and clear the cart
- `GET /store/orders/{id}` - Get order details with items (owner or Admin only)
- `PATCH /store/orders/{id}/status` - Update an order's status (Admin only)

### Reviews (`/store/reviews`)

- `GET /store/reviews/game/{gameId}` - Get all reviews for a game (Anonymous)
- `GET /store/reviews/{id}` - Get a specific review
- `POST /store/reviews/game/{gameId}/add` - Create a review for a game (Player only)
- `PATCH /store/reviews/{id}/update` - Update a review (own reviews only, Player)
- `DELETE /store/reviews/{id}` - Delete a review (own reviews or Admin)

### Coupons (`/store/coupons`)

- `GET /store/coupons` - Get all coupons, active and inactive (Player/Admin)
- `GET /store/coupons/{id}` - Get coupon by numeric ID (Admin only)
- `POST /store/coupons` - Create coupon (Admin only)
- `PATCH /store/coupons/{id}/deactivate` - Deactivate a coupon (Admin only)

There is currently no lookup-by-code, validate, or hard-delete endpoint — deactivation is the only way to retire a coupon.

### Analytics (`/store/analytics`, Admin only)

- `GET /store/analytics` - Get comprehensive analytics summary
- `GET /store/analytics/topGames` - Get top performing games, optional `?count=` (default 5)
- `GET /store/analytics/revenue` - Get total revenue for the last 30 days
- `GET /store/analytics/revenue/daily` - Get daily revenue breakdown, optional `?days=` (default 30)

## 👥 User Roles

- **Player**: Can browse games, make purchases, write reviews
- **Publisher**: Can manage their own games, view analytics for their games
- **Admin**: Full system access, user management, analytics

## 🔒 Authentication & Authorization

The API uses JWT tokens for authentication. Include the token in the Authorization header:

```
Authorization: Bearer <your-jwt-token>
```

## 📦 Dependencies

### Core Packages

- **AutoMapper** (12.0.1) - Object-to-object mapping
- **AutoMapper.Extensions.Microsoft.DependencyInjection** (12.0.1) - DI integration
- **BCrypt.Net-Next** (4.0.3) - Password hashing
- **FluentValidation** (12.1.0) - Input validation framework
- **FluentValidation.AspNetCore** (11.3.1) - ASP.NET Core integration

### Authentication & Authorization

- **Microsoft.AspNetCore.Authentication.JwtBearer** (9.0.9) - JWT authentication

### Database

- **Microsoft.EntityFrameworkCore** (9.0.10) - ORM framework
- **Microsoft.EntityFrameworkCore.SqlServer** (9.0.10) - SQL Server provider
- **Microsoft.EntityFrameworkCore.Design** (9.0.10) - Design-time tools

### API Documentation

- **Swashbuckle.AspNetCore** (9.0.6) - Swagger documentation

## 📊 Database Schema

The application uses the following main entities:

- **User**: User accounts with roles
- **Game**: Game catalog with pricing and metadata
- **CartItem**: Shopping cart items
- **Order**: Purchase orders
- **OrderItem**: Individual items in orders
- **Review**: User reviews and ratings
- **Coupon**: Discount codes

## 👨‍💻 Author

**Puneet Verma**

- GitHub: [@PuneetVerma04](https://github.com/PuneetVerma04)
