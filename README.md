# Generic Repository for CRUD Operations

This repository provides a generic implementation of a data access layer for CRUD (Create, Read, Update, Delete) operations using Entity Framework. It offers a flexible and reusable solution for interacting with various entities in a database.

## Features

- **Generic Operations**: Perform common CRUD operations on entities without writing repetitive code.
- **Filtering and Customization**: Easily filter, include navigation properties, and order data based on your requirements.
- **Asynchronous Support**: Asynchronous methods are provided for efficient and non-blocking data access.
- **Obsolete Methods**: Outdated methods are marked as `[Obsolete]` to guide you towards using more modern alternatives.

## Getting Started

1. Clone this repository to your local machine.

2. Install any required dependencies, such as Entity Framework, if not already installed.

3. Configure your database connection in the `appsettings.json` file.

4. Use the provided generic methods to interact with your database entities.

## Usage

Here's an example of how to use the generic repository methods:

```csharp
// Make IUnitOfWork interface

    public interface IUnitOfWork : IDisposable
    {
        #region Repos
        IGenericRepo<Location> Locations { get; }
        #endregion

        #region
        Task<bool> Save();
        #endregion
    }

// Implement IUnitOfWork

    public class UnitOfWork : IUnitOfWork
    {
        #region Fields and Properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region Repos
        public IGenericRepo<Location> Locations { get; private set; }
        #endregion

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Locations = new GenericRepo<Location>(_context);
        }
        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }

// Register IUnitOfWork on your program.cs
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// you can query a table like
var locations = _unitOfWork.Locations.GetAll();

// Perform searching
var location = await _unitOfWork.Locations.FindAsync(l => l.Id == 3);

// Perform searching and include navigation property
var location = await _unitOfWork.Locations.FindAsync(l => l.Id == 3, n => n.Include(p => p.Image));

// Perform filtering and include navigation property
var filteredLocations = _unitOfWork.Locations.Filter(l => l.Name = "Cairo", n => n.Include(p => p.WorkingHours));

// You can chain navigation properties like
var filteredLocations = _unitOfWork.Locations.Filter(l => l.Name = "Cairo", n => n.Include(p => p.WorkingHours).Inclue(p => p.Image));
```

## Contributing

Contributions are welcome! If you find a bug or have an enhancement in mind, please open an issue or submit a pull request.
