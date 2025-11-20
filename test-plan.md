# Mutation Testing - Additional Test Cases Plan

## Summary
Based on Stryker mutation testing report analysis, we have **4 survived mutants** that need to be killed with additional tests.

## Current Mutation Score
- **Killed**: 51 mutants
- **Survived**: 4 mutants
- **No Coverage**: 1 mutant
- **Total Score**: 92.73% (51/55 detected)

---

## Survived Mutants Analysis

### 1. Mutant #36 (Line 37) - PRIORITY HIGH
**Location**: `GildedRose.cs:37`
```csharp
// Original: if (Items[i].Quality < 50)
// Mutant:   if (Items[i].Quality <= 50)
```

**Context**: Backstage pass quality increase when SellIn < 11
- This mutant survived because we don't test quality at exactly 50 with SellIn between 6-10
- Current tests use quality 10 or 49, never 50

**Required Test**:
```csharp
[Theory]
[InlineData(10, 50, 50)]  // Quality at 50 should stay at 50, not increase
[InlineData(8, 50, 50)]
[InlineData(6, 50, 50)]
public void Should_NotIncreaseBackstagePassQualityOver50_WhenSellInBetween6And10()
```

---

### 2. Mutant #45 (Line 45) - PRIORITY HIGH
**Location**: `GildedRose.cs:45`
```csharp
// Original: if (Items[i].Quality < 50)
// Mutant:   if (Items[i].Quality <= 50)
```

**Context**: Backstage pass quality increase when SellIn < 6
- Same issue as Mutant #36, but for the SellIn < 6 range
- Tests use quality 49, which becomes 50 after the update, but never start at 50

**Required Test**:
```csharp
[Theory]
[InlineData(5, 50, 50)]  // Quality at 50 should stay at 50, not increase
[InlineData(3, 50, 50)]
[InlineData(1, 50, 50)]
public void Should_NotIncreaseBackstagePassQualityOver50_WhenSellInBetween1And5()
```

---

### 3. Mutants #63, #64, #65 (Lines 65-66) - PRIORITY MEDIUM
**Location**: `GildedRose.cs:65-66`
```csharp
// Original: if (Items[i].Quality > 0)
// Mutants:  
//   #63: if (Items[i].Quality < 0)
//   #64: if (Items[i].Quality >= 0)
//   #65: if (!(Items[i].Quality > 0))
```

**Context**: Quality degradation for expired regular items (not Aged Brie, not Backstage pass)
- This code path executes when SellIn < 0 and item is NOT Sulfuras
- Current tests don't cover quality boundary when item is expired

**Required Tests**:
```csharp
[Theory]
[InlineData(-1, 2, 0)]    // Expired item with quality 2 should degrade by 2
[InlineData(-1, 1, 0)]    // Expired item with quality 1 should degrade by 1 (but can't go negative)
[InlineData(-5, 10, 8)]   // Long expired item degrades by 2
public void Should_DegradeQualityTwiceAsFast_WhenSellInIsNegative()
```

---

### 4. Mutant #70 (Line 69) - NO COVERAGE
**Location**: `GildedRose.cs:69`
```csharp
// Original: Items[i].Quality = Items[i].Quality + 1;
// Mutant:   Items[i].Quality = Items[i].Quality - 1;
```

**Context**: This appears to be in unreachable/dead code
- Line 69 is inside a nested condition that may never execute
- Located in expired item logic for items that are NOT backstage passes and NOT Sulfuras
- **Action**: Verify if this code path is reachable; if not, consider refactoring

---

## New Test Methods to Add

### Test 1: Backstage Pass Quality Cap (SellIn 6-10)
```csharp
[Theory]
[InlineData(10, 50, 50)]
[InlineData(8, 50, 50)]
[InlineData(6, 50, 50)]
public void Should_NotIncreaseBackstagePassQualityOver50_WhenSellInBetween6And10(
    int initialSellIn, int initialQuality, int expectedQuality)
{
    IList<Item> Items = new List<Item> 
    { 
        new Item { Name = _backStagePassName, SellIn = initialSellIn, Quality = initialQuality } 
    };

    GildedRose app = new(Items);
    app.UpdateQuality();
    
    Assert.Equal(expectedQuality, Items[0].Quality);
}
```
**Kills**: Mutant #36

---

### Test 2: Backstage Pass Quality Cap (SellIn 1-5)
```csharp
[Theory]
[InlineData(5, 50, 50)]
[InlineData(3, 50, 50)]
[InlineData(1, 50, 50)]
public void Should_NotIncreaseBackstagePassQualityOver50_WhenSellInBetween1And5(
    int initialSellIn, int initialQuality, int expectedQuality)
{
    IList<Item> Items = new List<Item> 
    { 
        new Item { Name = _backStagePassName, SellIn = initialSellIn, Quality = initialQuality } 
    };

    GildedRose app = new(Items);
    app.UpdateQuality();
    
    Assert.Equal(expectedQuality, Items[0].Quality);
}
```
**Kills**: Mutant #45

---

### Test 3: Double Quality Degradation After Expiry
```csharp
[Theory]
[InlineData(-1, 10, 8)]   // Quality degrades by 2 when expired
[InlineData(-1, 2, 0)]    // Quality degrades by 2 but stops at 0
[InlineData(-1, 1, 0)]    // Quality degrades but can't go negative
[InlineData(-5, 20, 18)]  // Multiple days expired still degrades by 2
public void Should_DegradeQualityTwiceAsFast_WhenItemIsExpired(
    int initialSellIn, int initialQuality, int expectedQuality)
{
    IList<Item> Items = new List<Item> 
    { 
        new Item { Name = "Regular Item", SellIn = initialSellIn, Quality = initialQuality } 
    };

    GildedRose app = new(Items);
    app.UpdateQuality();
    
    Assert.Equal(expectedQuality, Items[0].Quality);
}
```
**Kills**: Mutants #63, #64, #65

---

## Additional Recommendations

### 1. Test Backstage Pass at Quality 49
Verify that backstage passes at quality 49 correctly increase to 50 but not beyond:
```csharp
[Theory]
[InlineData(10, 49, 50)]  // Increases to 50 (by 2, capped)
[InlineData(5, 49, 50)]   // Increases to 50 (by 3, capped)
public void Should_CapBackstagePassQualityAt50_WhenIncreasing(
    int initialSellIn, int initialQuality, int expectedQuality)
```

### 2. Edge Case: Multiple Updates
Test that quality stays at 50 after multiple updates:
```csharp
[Fact]
public void Should_KeepBackstagePassQualityAt50_AfterMultipleUpdates()
{
    IList<Item> Items = new List<Item> 
    { 
        new Item { Name = _backStagePassName, SellIn = 10, Quality = 49 } 
    };
    
    GildedRose app = new(Items);
    
    app.UpdateQuality();  // Should reach 50
    Assert.Equal(50, Items[0].Quality);
    
    app.UpdateQuality();  // Should stay at 50
    Assert.Equal(50, Items[0].Quality);
}
```

---

## Implementation Priority

1. **HIGH**: Test 1 & 2 (Backstage pass quality cap) - Kills mutants #36 and #45
2. **MEDIUM**: Test 3 (Double degradation) - Kills mutants #63, #64, #65
3. **LOW**: Investigate mutant #70 (potentially dead code)

---

## Expected Results After Implementation

- **New Mutation Score**: ~100% (55/55 or 54/54 if #70 is dead code)
- **Survived Mutants**: 0
- **Test Coverage**: Complete boundary condition coverage for quality limits

---

## Notes

- All survived mutants relate to **boundary conditions** at quality = 50
- The mutation testing tool effectively caught these gaps
- These tests ensure the business rule "quality never exceeds 50" is properly enforced
- Consider adding property-based tests for comprehensive quality boundary verification
