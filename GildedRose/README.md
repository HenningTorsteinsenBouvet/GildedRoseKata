1. Introduction
2. Tools
3. When to refactor
4. What is Gilded Rose?
5. Add characterization tests
6. Check for mutants

7. Kill the mutants!

Review the mutation testing report to see which mutants have survived. Focus on the following survived mutants:
   - Mutant #36 (Line 37): Change `<` to `<=` in the quality check for Backstage passes when SellIn < 11.
   - Mutant #45 (Line 45): Change `<` to `<=` in the quality check for Backstage passes when SellIn < 6.
   - Mutants #63, #64, #65 (Lines 65-66): Change `-` to `+` in the quality degradation logic for normal items after SellIn date has passed.

Or... let the LLM do it for you!

Prompt:
`Review the Stryker mutation testing report, and create a plan for more tests.`

Documents created:
- `test-plan.md`: A detailed plan outlining the survived mutants, their locations, root causes, and specific test cases needed to kill them.
- `MutationKillerTests.cs`: A new test class containing the necessary test methods and cases to kill all survived mutants.
- `SUMMERY.md`: A summary of the improvements made, including the initial mutation score, survived mutants, and what was created to address them.


8. Finally Some Refactoring
9. Identify code smells
10. Refactor
11. Refactor
12. Refactor
13. Refactor
14. Refactor
15. Refactor