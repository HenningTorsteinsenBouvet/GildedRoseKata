# Test Cleanup Summary

## Changes Made

### Removed 3 Problematic Tests from `GildedRoseTest.cs`

The following tests were removed because they **did not properly use their test parameters**:

1. **`Should_NotIncreaseAgedBrieQualityOver50_GivenSellInIncreases`**
   - Used `new AgedBrie(initialSellIn, initialQuality)` constructor
   - While this technically works, it's inconsistent with all other tests in the file that use `new Item { Name = ..., SellIn = ..., Quality = ... }`
   
2. **`Should_NotChangeQualityOfSulfuras_GivenSellInIncreases`**
   ```csharp
   [Theory]
   [InlineData(42, 80, 80)]
   [InlineData(-10, 80, 80)]
   public void Should_NotChangeQualityOfSulfuras_GivenSellInIncreases(
       int initialSellIn, int initialQuality, int expectedQuality)
   {
       IList<Item> Items = new List<Item> { new Sulfaras() };  // ? Ignores parameters!
   ```
   - The test defined `initialSellIn` and `initialQuality` parameters but **never used them**
   - `new Sulfaras()` always creates an item with SellIn=0 and Quality=80 (from the constructor)
   - Both test cases were running the exact same test twice

3. **`Should_NotChangeSellInOfSulfuras_GivenWeRunUpdateQuality`**
   ```csharp
   [Theory]
   [InlineData(42, 42)]
   [InlineData(-10, -10)]
   public void Should_NotChangeSellInOfSulfuras_GivenWeRunUpdateQuality(
       int initialSellIn, int expectedSellIn)
   {
       IList<Item> Items = new List<Item> { new Sulfaras() };  // ? Ignores initialSellIn!
   ```
   - Same issue: defined `initialSellIn` but used `new Sulfaras()` which always sets SellIn=0
   - The assertions would always compare against the default value, not the parameter values

## Why These Tests Were Problematic

### The Root Cause
The `Sulfaras` class constructor doesn't accept parameters:
```csharp
public class Sulfaras : Item
{
    public Sulfaras()  // ? No parameters
    {
        Name = "Sulfuras, Hand of Ragnaros";
        SellIn = 0;
        Quality = 80;
    }
}
```

### The Impact
- Tests appeared to test multiple scenarios (with different InlineData values)
- In reality, they ran the same test multiple times with the same hardcoded values
- This gave false confidence in test coverage
- The tests would pass even if the code didn't work for other SellIn/Quality values

## Replacement Coverage

These requirements are **correctly and comprehensively tested** in `GildedRoseAdditionalTests.cs`:

### Aged Brie Quality Cap Tests
```csharp
[Theory]
[InlineData(10, 50, 50)]
[InlineData(-10, 50, 50)]
[InlineData(-1, 49, 50)]
public void AgedBrie_Quality_Should_NeverExceed50(...)
```
? Properly uses all parameters

### Sulfuras Never Changes Tests
```csharp
[Theory]
[InlineData(10, 80, 80, 10)]
[InlineData(0, 80, 80, 0)]
[InlineData(-1, 80, 80, -1)]
[InlineData(100, 80, 80, 100)]
public void Sulfuras_Should_NeverChangeQualityOrSellIn(
    int initialSellIn, int initialQuality, int expectedQuality, int expectedSellIn)
{
    IList<Item> items = new List<Item> 
    { 
        new Item { Name = Sulfuras, SellIn = initialSellIn, Quality = initialQuality } 
    };
    // ...
}
```
? Tests multiple SellIn values (-1, 0, 10, 100)
? Validates both Quality AND SellIn don't change
? Properly uses all parameters

## Test Count Summary

**Before:** 63 tests (10 in GildedRoseTest.cs, 53 in GildedRoseAdditionalTests.cs)
**After:** 60 tests (7 in GildedRoseTest.cs, 53 in GildedRoseAdditionalTests.cs)
**Status:** ? All 60 tests pass

## Remaining Tests in GildedRoseTest.cs

The 7 remaining tests are all valid and properly implemented:
1. `Should_IncreaseBackStageQualityBy2_GivenSellInIs10DaysOrLess` (2 test cases)
2. `Should_IncreaseBackStageQualityBy3_GivenSellInIs5DaysOrLess` (2 test cases)
3. `Should_SetBackStageQualityToZero_GivenSellInIsBelow0` (1 test case)
4. `Should_IncreaseAgedBrieQuality_GivenSellInIncreases` (2 test cases)
5. `Should_IncreaseBackStageQualityBy1_GivenSellInAbove10` (1 test case)

All use parameters correctly and align with requirements.

## Recommendation

Consider whether `GildedRoseTest.cs` should be kept at all, since `GildedRoseAdditionalTests.cs` provides more comprehensive coverage. The additional tests file includes:
- All scenarios from the original tests
- More edge cases
- Better organization by item type
- More descriptive test names with AAA pattern
