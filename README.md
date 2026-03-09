# 📚 Library Management System

A robust, modular Library Management System built with **C#** and **.NET 10**. This project demonstrates the application of **Clean Architecture** principles and **Object-Oriented Programming (OOP)** to create a scalable and maintainable software solution.

## 🏗️ Architecture & Design
The solution is organized into distinct layers to ensure a strict separation of concerns:

* **LibrarySystem.Core:** The heart of the system. Contains domain entities (Books, Authors, Users) and core business rules, free from external dependencies.
* **LibrarySystem.Application:** Implements the business logic and service interfaces. It acts as the bridge between the UI and the data layer.
* **LibrarySystem.ConsoleUI:** A clean Command Line Interface (CLI) providing an intuitive way for users to interact with the system.

## 🛠️ Tech Stack & Patterns
* **Language:** C#
* **Framework:** .NET 10
* **Design Patterns:** Result Pattern (for functional error handling), Repository Pattern.
* **Development Principles:** SOLID, DRY (Don't Repeat Yourself), and Clean Code.

## ✨ Key Features
* **Book & Member Management:** Efficiently track the library's catalog and users.
* **Loan Logic:** Handles borrowing and returning with validation.
* **Error Handling:** Uses the **Result Pattern** to ensure predictable and safe application flow without relying on heavy exceptions.
