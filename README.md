## Updates

- Created classes for each client name and implemented their own CalculateCredit method, so when a new type of customer is needed to add to the project, we'll just create new client name class and implement its own CalculateCredit method.
- Injected UserCreditService to these client classes to make classes testable.
- Injected ClientRepostitory to the constructor with constructor chaining.
- Created extension methods for general validation methods for reusebality.
- Wrapped static DataAccess class with repository class to make it testable.



# Refactoring test in C#

## Description

You are asked to refactor the UserService class and, more specifically, its AddUser method. 
Assume that the code is sound in terms of business logic and only focuses on applying clean code principles. Keep in mind acronyms such as SOLID, KISS, DRY and YAGNI.

Try to keep this exercise below 3 hours. If you still have things you can improve after the 3-hour mark, please write them down, and we will take them into account.

## Limitations
The Program.cs class in the LegacyApp.Consumer shall NOT CHANGE AT ALL. This includes using statements. Assume that this codebase is part of a greater system, and any non-backward compatible change will break the system.

You can change anything in the LegacyApp project except for the UserDataAccess class and its AddUser method. Both the class and the method NEED to stay static.
