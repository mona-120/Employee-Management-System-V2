# Employee Management System – V2

A C# console application for managing employees and departments while practicing C# Collections, OOP, Generics, Delegates, Lambda Expressions, and Events.

## About the Project

This project is an enhanced version of my previous [Employee Management System ](https://github.com/mona-120/Employee-Management-System/tree/master).

The first version mainly focused on practicing C# Collections such as `List`, `Dictionary`, `Queue`, `Stack`, and `HashSet`.

Version 2 extends the system by introducing more advanced C# concepts, including `Generics`, `Delegates`, and `Events`, while continuing to use the collections from the previous version.

---

## Features

- Add a new employee to the Onboarding Queue with an automatically generated ID.
- Process employees from the Onboarding Queue in FIFO order and activate them.
- Add new departments with automatically generated unique IDs.
- Promote an existing employee to a `Manager` by replacing the employee object in the Active Employees List.
- Add multiple skills to a specific employee and store them in the employee's own skills list.
- Store all unique skills using `HashSet` to prevent duplicates.
- Search for an employee by ID or name using a manual loop.
- Display all active employees of a specific department.
- Filter employees using a Delegate with different Lambda Expressions.
- Calculate the average salary using a manual loop.
- Generate a report showing the number of employees in each department without using LINQ.
- Track major actions using a `Stack` and display them from newest to oldest.
- Raise events when an employee is activated or promoted.
- Return results using a generic `Result<T>` class instead of throwing exceptions for every expected invalid operation.
- Validate numeric user input using `TryParse` to avoid system craching when invalid inputs.
- Sort Employees using a Delegate Ascending by Salary.
- Use another method to Search about employee and department by implement Generic Interface `IHasId`.
-  Raise events when a skill added.

---

## What's New in V2?

### 1. Manager Class

When an employee is promoted, a `Manager` object is created and replaces the original employee in the Active Employees List while keeping the same employee ID and information.

This allows the promoted employee to be treated as a `Manager` while remaining in the Active Employees List.

---

### 2. Generic Result<T>

A generic `Result<T>` class was used to display a message for each operation and its status(success , failed).

---

### 3. Multiple Skills per Employee

The `AddSkills` method was enhanced to accept multiple skills at once.

The skills are stored in the employee's own `Skills` list, while a `HashSet` is used to maintain a collection of unique skills across the system.

---

### 4. Delegates and Lambda Expressions

Employee filtering was implemented using a custom Delegate.

Create a Delegate to sort employees ascending by Salary using Bubble sort.

Instead of creating a separate method for every filtering condition, the filtering method receives the condition as a Delegate.

---

### 5. Events

Events were introduced to notify other parts of the program when important employee actions occur.

The system raises events when:

- An employee is activated from the Onboarding Queue.
- An employee is promoted to a Manager.
- Add a new skill in the system.

The `Company` class acts as the Publisher and raises the events, while the subscribed Event Handler methods in `Program` respond to them.

This allows the `Company` to notify other parts of the application without knowing exactly which method will handle the notification.

Try Subscribe (+=) and Subscribe(-=) events in program.cs to demonstrate when method is running.  

--- 

### 6. Generic IHasId<T>

A generic `Result<T>` interface used to search about employee and Department  using another way.





