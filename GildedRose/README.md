1. Introduction
2. Tools
3. When to refactor
4. What is Gilded Rose?
5. Add characterization tests

6. Check for mutants

# Mutation testing

## What is mutation testing?

Mutation testing is a technique used to evaluate the quality of your test suite. It involves making small changes (mutations) to your code and then running your tests to see if they catch the changes. If a test fails due to a mutation, the mutant is considered "killed." If the tests pass despite the mutation, the mutant "survives," indicating that your tests may not be thorough enough.

## Getting started with mutation testing

`dotnet tool install -g dotnet-stryker`

Run it in the directory of the sln file:

`dotnet stryker -o`
(use test-case-filter parameter on larger code bases!)

Review the report generated in the StrykerOutput folder.

7. Kill the mutants!
8. Finally Some Refactoring
9. Identify code smells
10. Refactor
11. Refactor
12. Refactor
13. Refactor
14. Refactor
15. Refactor