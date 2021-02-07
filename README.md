# COMP2001-PROJECTS

This repository consists of two applications for the COMP2001 specification. Part 1 is an ASP.NET authentication application, using SQL Server stored procedures for CRUD operations, triggers, views and more. Part 2 is a data linked application based on a dataset about school in Plymouth, and needed to provide it in both a human and machine readable format. It converts the original geo-JSON dataset into a JSON-LD dataset using PHP.

## Part 1 - ASP.NET Authentication Application

### Product Vision
For applications that need to authorise users, the Authenticator is a RESTful API authentication
system that providing simple and robust access via the HTTP infrastructure.

### Technologies
- ASP.NET Core Web API
- Hashing - PBKDF2 (RFC 2898) algorithm using SHA256
- Authenication Mechanism - JWT (Bearer Tokens)

### Credits
- JSON Web Tokens : https://jwt.io/# 

## Part 2 - Linked Data Application

### Project Vision
Plymouth Schools is for parents interested in identifying school types around specific locations in Plymouth, this would enable adults to visualize schools where they could potentially send their children and its surroundings. It is a semantic web application that presents data in two types, human and machine-readable format. It also provides a machine-to-machine formatted file for consumption if required.

### Technologies
- HTML, CSS and JavaScript
- PHP
- LeafletJS Maps
- Geodesy Converting - OSGB 1936 to WGS 84

### Credits
- BootStrap : https://getbootstrap.com/
- LeafletJS : https://leafletjs.com/index.html
- Schools Dataset : https://plymouth.thedata.place/dataset/schools
- Geodesy Converting - OSGB 1936 to WGS 84 : https://www.movable-type.co.uk/scripts/geodesy-library.html

