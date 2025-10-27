# InventoryHub

InventoryHub is a full-stack inventory management application built using **Blazor** for the front-end and **.NET Minimal API** for the back-end. This project emphasizes **secure coding**, **authentication & authorization**, and **robust debugging** practices.

---

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Security Measures](#security-measures)
- [Setup & Deployment](#setup--deployment)
- [Testing](#testing)
- [Reflection](#reflection)
- [Repository](#repository)

---

## Overview
InventoryHub helps users manage inventory items with categorized data, secure login, and role-based access. Microsoft Copilot was used throughout the development to generate integration code, handle debugging, and optimize performance.

---

## Features
- **Secure Input Handling**: Input validation and SQL injection prevention.
- **Authentication & Authorization**: Role-Based Access Control (RBAC) implemented.
- **API Integration**: Blazor front-end consumes Minimal API endpoints.
- **JSON Structuring**: Nested JSON responses for categories and items.
- **Caching & Optimization**: Reduced redundant API calls for better performance.
- **Testing**: Security and functionality verified using automated tests.

---

## Technologies Used
- **Front-End**: Blazor WebAssembly  
- **Back-End**: .NET 9 Minimal API  
- **Database**: SQL Server / MSSQL  
- **Tools**: Microsoft Copilot, Postman  
- **Version Control**: Git & GitHub  

---

## Security Measures
- Input validation on all forms.
- Protection against SQL Injection.
- Prevention of XSS vulnerabilities.
- Role-based authentication and authorization for user access.
- Debugging and vulnerability fixes assisted by Copilot.

---

### Testing

- Automated tests verify input validation, authentication, and API responses.
- Security tests confirm prevention of SQL Injection and XSS attacks.
- Copilot assisted in generating unit and integration tests.

### Reflection

- Throughout this project, Microsoft Copilot was used as a coding collaborator:
- **Integration**: Suggested HTTP client syntax and API integration patterns.
- **Debugging**: Helped identify CORS issues, malformed JSON, and routing errors.
- **JSON Structuring**: Assisted in formatting nested category data.
- **Optimization**: Recommended caching and code refactoring.
- **Key takeaway**: Copilot accelerates development but should be used critically, not blindly.