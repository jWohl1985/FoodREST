# FoodREST - A RESTful API for food nutrition information.

This is a C# ASP.NET Core REST API that allows you to create, update, delete, and query food nutrition info. The point of the project was to build a RESTful API while following the Clean Architecture pattern.

For the size of this project, the Clean Architecture approach is probably adding more complexity than it is worth. That's ok, as the idea was just to practice and demonstrate the pattern.

Features:
- Basic CRUD operations
- Query options: sorting, filtering, pagination
- Output caching
- Clean Architecture pattern

Technologies:
- ASP.NET Core web API project, using controllers
- MediatR for handling commands/queries
- Dapper for implementing the food repository
- PostgreSQL database
- xUnit unit and integration tests



# Endpoints

## Create
> POST
- api/foods
> Example request
{
"name": "Apple", // cannot be null or empty
"calories":"110", // cannot be negative
"proteingrams":"2", // cannot be negative
"carbohydrategrams":"27", // cannot be negative
"fatgrams":"0" // cannot be negative
}

## Get
> GET
- api/foods/{id}

## GetAll
> GET
- api/foods
- api/foods?sortBy=name // sorting by name, use -name for descending
- api/foods?page={pageNumber}&pageSize={itemsPerPage} // pagination, max 25 per page

The default request is unsorted, page 1 with 10 items per page.
> Example optional request
{
"page": 1 // which page to fetch
"pageSize": 25 // between 1-25 items per page
"sortfield": "-name", // can be null, supports sorting by name. Prefix '-' for descending
}

## Update
> PUT
- api/foods/{id}
>Example request
{
"name": "Apple", // cannot be null or empty
"calories":"110", // cannot be negative
"proteingrams":"2", // cannot be negative
"carbohydrategrams":"27", // cannot be negative
"fatgrams":"0" // cannot be negative
}

## Delete
> DELETE
- api/foods/{id}