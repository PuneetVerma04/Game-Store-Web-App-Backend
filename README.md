# Steam Clone Backend

A comprehensive ASP.NET Core Web API backend for a Steam-like game store application. This project provides a full-featured API for managing games, users, shopping carts, orders, reviews, coupons, and analytics.

## 🚀 Features

### Core Functionality
- **User Management**: Registration, authentication, and role-based authorization (Player, Publisher, Admin)
- **Game Catalog**: Complete CRUD operations for game management with filtering by genre and price
- **Shopping Cart**: Add, remove, and manage game purchases
- **Order Processing**: Complete order management system
- **Review System**: User reviews and ratings for games
- **Coupon System**: Discount codes and promotional offers
- **Analytics Dashboard**: Revenue tracking and game performance metrics

### Security & Authentication
- JWT-based authentication
- Role-based authorization (Player, Publisher, Admin)
- Password hashing using BCrypt
- Secure API endpoints

## 🛠 Technology Stack

- **.NET 9.0**: Latest .NET framework
- **ASP.NET Core Web API**: RESTful API framework
- **Entity Framework Core**: ORM for database operations
- **SQL Server**: Database management system
- **AutoMapper**: Object-to-object mapping
- **JWT Bearer Tokens**: Authentication mechanism
- **BCrypt**: Password hashing
- **Swagger/OpenAPI**: API documentation

## 📁 Project Structure

```
SteamClone.Backend/
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
│   ├── Cart.cs
│   ├── Order.cs
│   ├── Reviews.cs
│   └── Coupons.cs
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
├── Settings/             # Configuration settings
└── Properties/           # Launch settings
```

## 🔧 Installation & Setup

### Prerequisites
- .NET 9.0 SDK
- SQL Server (LocalDB or full version)
- Visual Studio 2022 or VS Code

### Steps

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd "Steam Clone Back"
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Configure database connection**
   Update the connection string in `appsettings.json` or `appsettings.Development.json`

4. **Configure JWT settings**
   Set up JWT configuration in `appsettings.json`:
   ```json
   {
     "JwtSettings": {
       "Key": "your-secret-key-here",
       "Issuer": "your-issuer",
       "Audience": "your-audience"
     }
   }
   ```

5. **Run the application**
   ```bash
   dotnet run --project SteamClone.Backend
   ```

6. **Access API documentation**
   - Development: `https://localhost:7044/swagger`
   - HTTP: `http://localhost:5062/swagger`

## 📚 API Endpoints

### Authentication
- `POST /store/auth/register` - User registration
- `POST /store/auth/login` - User login

### Games
- `GET /store/games` - Get all games (with optional filtering)
- `GET /store/games/{id}` - Get game by ID
- `POST /store/games` - Create new game (Publisher/Admin only)
- `PUT /store/games/{id}` - Update game (Publisher/Admin only)
- `DELETE /store/games/{id}` - Delete game (Admin only)

### Users
- `GET /store/users` - Get all users (Admin only)
- `GET /store/users/{id}` - Get user by ID
- `PUT /store/users/{id}` - Update user profile

### Cart
- `GET /store/cart/{userId}` - Get user's cart
- `POST /store/cart` - Add item to cart
- `DELETE /store/cart/{userId}/{gameId}` - Remove item from cart

### Orders
- `GET /store/orders` - Get user's orders
- `POST /store/orders` - Create new order
- `GET /store/orders/{id}` - Get order details

### Reviews
- `GET /store/reviews/game/{gameId}` - Get reviews for a game
- `POST /store/reviews` - Create review
- `PUT /store/reviews/{id}` - Update review
- `DELETE /store/reviews/{id}` - Delete review

### Coupons
- `GET /store/coupons` - Get available coupons
- `POST /store/coupons` - Create coupon (Admin only)
- `POST /store/coupons/validate` - Validate coupon

### Analytics
- `GET /store/analytics/summary` - Get analytics summary (Admin only)

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

- **AutoMapper** (12.0.1) - Object mapping
- **BCrypt.Net-Next** (4.0.3) - Password hashing
- **Microsoft.AspNetCore.Authentication.JwtBearer** (9.0.9) - JWT authentication
- **Microsoft.EntityFrameworkCore** (9.0.10) - ORM
- **Microsoft.EntityFrameworkCore.SqlServer** (9.0.10) - SQL Server provider
- **Swashbuckle.AspNetCore** (9.0.6) - Swagger documentation

## 🚀 Development

### Running in Development
```bash
dotnet run --environment Development
```

### Building for Production
```bash
dotnet build --configuration Release
```

### Running Tests
```bash
dotnet test
```

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🤝 Contributing

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📞 Support

For support and questions, please open an issue in the GitHub repository.