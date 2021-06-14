# TaxCalculator
## Overview
This project contains a Tax Calculator as put forth in the assessment brief. It handles the three different types of tax calculation based on the postal code given as well as the annual income.

The project structure, especially the database, is designed towards having multiple entries per tax/calculation type i.e one can have multiple Flat Value or Flat Rates entries. etc. The foundations has been added to the project, but not stricly implemented, yet left for possible expansion options. Note that what has well been added is the ability to create more **Postal Codes** and link this up to a tax type. 

Furthermore, as security wasn't a prerequisite for the assessment, user creation and API security wasn't implemented. 

Lastly, a custom Repository management wrapper was written to handle calls to the database. This goes along with a custom Unit of Work that handles the actual calls and disposes
as needed.

## Tools Used
This project made use of the following:
- .NET Core 5
- EF Core
- Automapper
- Newtonsoft.Json
- NUnit
- Moq
- SQL LocalDB
- Postman
- GitHub

## Removed Projects and Classes
Note that there was a project *removed* after the initial upload to GitHub. This project is just **TaxCalculator**. This can be excluded or removed as see fit. All other excluded classes can just be ignored.

## Solution Structure
The solution consists of the following projects:
- **TaxCalculator.API** : this is the API set up to handle the incoming calls. This contains the calls to the calculation engine, as well as the Postal Code CRUDs.
- **TaxCalculator.MVC** : this is a basic MVC front-end that uses JQuery to make calls to the API.
- **TaxCalculator.Core** : this contains most of the Models and business logic, as well as DTOs and Helper classes.
- **TaxCalculator.Repo** : this is the project that handles the repository work to and from the DB, wrapped in a Unit of Work.
- **TaxCalculator.Test** : this contains a few basic Unit Tests for the calculations objects.

## Startup
A few key notes on starting up the project:
- There is a migration set up with seeding info, so running `update-database` once the connection string is set up, should be adequate.
- There are two changes that will have to be made to get it working propertly. Firstly, change the connection string found in *DependencyInjection.cs* on the **Repo** layer.
Secondly, in the need to sidestep CORS failures, change the allowable URL in the **Configure** method in the *Startup.cs* of the API.

A zip file was added with the **localDB** database as requested. Note that this localdb is the working database I used and as such contains testing info etc.

## GitHub
There are a few branches on GitHub, but `main` should be the final version. `dev` was used during development and `main-final` was used as prepatory placeholder.

## Postman Export

https://www.getpostman.com/collections/969ddde7e3e1723c7cd3



