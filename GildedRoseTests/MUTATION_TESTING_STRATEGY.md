# Mutation Testing - Mutant Killer Tests

## Summary

This document describes the additional tests added to kill mutation testing mutants in the GildedRose kata project.

## Overview

Based on the Stryker.NET mutation testing report, I've added a comprehensive test suite specifically designed to kill mutants by testing exact values, boundaries, and edge cases.

## New Test File: GildedRoseTestMutationKillers.cs

### Test Categories

#### 1. **Exact Value Tests**
These tests verify that quality and SellIn changes are exactly the expected values, killing mutants that attempt to change increment/decrement amounts:

- `Should_DecreaseQualityByExactlyOne_ForNormalItem_WhenSellInIsPositive`
- `Should_DecreaseQualityByExactlyTwo_ForNormalItem_WhenSellInIsZeroOrNegative`
- `Should_IncreaseAgedBrieQualityByExactlyOne_WhenSellInIsPositive`
- `Should_IncreaseAgedBrieQualityByExactlyTwo_WhenSellInIsZeroOrNegative`
- `Should_IncreaseBackstagePassQualityByExactlyOne_WhenSellInIsAbove10`
- `Should_IncreaseBackstagePassQualityByExactlyTwo_WhenSellInIs10`
- `Should_IncreaseBackstagePassQualityByExactlyTwo_WhenSellInIs6`
- `Should_IncreaseBackstagePassQualityByExactlyThree_WhenSellInIs5`

#### 2. **Boundary Condition Tests**
These tests verify the exact boundaries for decision points, killing mutants that change comparison operators (`<` vs `<=`, etc.):

- `Should_UseCorrectBoundary_ForBackstagePass10DayThreshold` - Tests SellIn = 11 vs 10
- `Should_UseCorrectBoundary_ForBackstagePass5DayThreshold` - Tests SellIn = 6 vs 5
- `Should_UseCorrectBoundary_ForNormalItemExpiration` - Tests SellIn = 1 vs 0

#### 3. **Special Item Tests**
These tests ensure special items behave exactly as specified:

- `Should_SetBackstagePassQualityToExactlyZero_WhenConcertPasses` - Backstage passes drop to 0
- `Should_NotChangeSulfurasQualityOrSellIn_EverythingMustRemainExactly` - Sulfuras never changes

#### 4. **Quality Constraint Tests**
These tests verify that quality constraints are enforced:

- `Should_RespectQualityNeverNegative_ForAllItems`
- `Should_RespectQualityNeverAbove50_ForAgedBrie`
- `Should_RespectQualityNeverAbove50_ForBackstagePasses`

#### 5. **SellIn Decrement Tests**
These tests verify SellIn decreases by exactly one for all items except Sulfuras:

- `Should_DecreaseSellInByExactlyOne_ForNormalItems`
- `Should_DecreaseSellInByExactlyOne_ForAgedBrie`
- `Should_DecreaseSellInByExactlyOne_ForBackstagePasses`

#### 6. **Loop and Collection Tests**
These tests kill mutants that might change loop behavior:

- `Should_ProcessEmptyList_WithoutErrors` - Tests loop bounds with 0 items
- `Should_ProcessExactlyAllItems_InOrderWithoutSkipping` - Tests loop increment
- `Should_NotModifyWrongItem_WhenProcessingList` - Tests correct indexing

## Mutation Types Targeted

The tests are designed to kill the following types of mutants:

1. **Arithmetic Mutants**: `+ 1` ? `+ 2`, `- 1` ? `- 2`
2. **Comparison Mutants**: `<` ? `<=`, `>` ? `>=`, `==` ? `!=`
3. **Logical Mutants**: `&&` ? `||`
4. **Conditional Boundary Mutants**: Off-by-one errors in conditions
5. **Assignment Mutants**: Wrong values assigned
6. **Loop Mutants**: `++` ? `--`, wrong loop bounds

## Test Results

- **Total Tests**: 91
- **Passed**: 91
- **Failed**: 0
- **Build**: Successful

## Key Testing Principles Used

1. **Exact Value Assertions**: Using `Assert.Equal()` with precise expected values
2. **Boundary Testing**: Testing values at and around decision boundaries
3. **Positive and Negative Cases**: Testing both sides of conditional branches
4. **Edge Cases**: Empty lists, zero values, maximum values
5. **Invariant Testing**: Ensuring constraints (0 ? quality ? 50) are maintained

## How These Tests Kill Mutants

### Example 1: Quality Decrement Mutant
```csharp
// Original: Items[i].Quality = Items[i].Quality - 1;
// Mutant:   Items[i].Quality = Items[i].Quality - 2;

[Fact]
public void Should_DecreaseQualityByExactlyOne_ForNormalItem_WhenSellInIsPositive()
{
    // ... setup with Quality = 10
    app.UpdateQuality();
    Assert.Equal(9, Items[0].Quality); // Kills mutant: would be 8
}
```

### Example 2: Comparison Operator Mutant
```csharp
// Original: if (Items[i].SellIn < 11)
// Mutant:   if (Items[i].SellIn <= 11)

[Theory]
[InlineData(11, 10, 11)] // SellIn=11 should NOT trigger the condition
[InlineData(10, 10, 12)] // SellIn=10 SHOULD trigger the condition
public void Should_UseCorrectBoundary_ForBackstagePass10DayThreshold(...)
```

### Example 3: Loop Increment Mutant
```csharp
// Original: i++
// Mutant:   i--

[Fact]
public void Should_ProcessExactlyAllItems_InOrderWithoutSkipping()
{
    // Creates 3 items
    app.UpdateQuality();
    // All 3 must be processed exactly once
    Assert.Equal(9, Items[0].Quality);
    Assert.Equal(9, Items[1].Quality);
    Assert.Equal(9, Items[2].Quality);
}
```

## Conclusion

The new test suite provides comprehensive mutation testing coverage by:
- Testing exact values rather than ranges
- Testing all boundary conditions
- Testing all item types and their specific behaviors
- Testing loop and collection handling
- Verifying all business rule constraints

This ensures that any changes to the logic that would alter the behavior will be caught by at least one test.
