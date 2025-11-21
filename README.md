1. Introduction
2. Tools
3. When to refactor
4. What is Gilded Rose?

5. Add characterization tests

# Charaterization the system
To be able to refactor the Gilded Rose system safely, we need to have a characterization test suite in place.
Also sometimes called acceptance tests, these tests will ensure that the system behaves the same before and after refactoring.

These tests will help us to ensure that we don't change the behavior of the system while refactoring it.

If there are any bugs in the existing system, the characterization tests will lock in the buggy behavior. (Other systems might depend on the bugs continuing to "work")

## Steps to create characterization tests

### Old-school style:
1. Identify the system boundaries
2. Identify the inputs and outputs
3. Create tests for the inputs and outputs
4. Run the tests to ensure they pass

### In the age of LLms:

1. Generate tests with LLM
1. Ask LLM to review its own work

*(Yes, finally something we can use LLMs for)*

Prompt:
Read Requirements.md and GildedRose.cs and generate a set of characterization tests that cover the behavior of the system. The tests should be in [your preferred testing framework]. Ensure that the tests cover all edge cases and scenarios described in the requirements.

Second Prompt:
Review the unit tests in GildedRoseTests, and compare to the Requirements.md. Do the tests align with the requirements?


6. Check for mutants
7. Kill the mutants!
8. Finally Some Refactoring
9. Identify code smells
10. Refactor
11. Refactor
12. Refactor
13. Refactor
14. Refactor
15. Refactor