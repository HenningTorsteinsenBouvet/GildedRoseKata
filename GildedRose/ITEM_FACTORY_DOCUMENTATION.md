# Item Factory Method Documentation

## Overview

A static factory method `Item.Create()` has been added to the `Item` class to create the correct subtype based on the item name. This provides a clean, centralized way to instantiate items without needing to know the specific subtype classes.

## Usage

### Basic Syntax

```csharp
Item item = Item.Create(string name, int sellIn, int quality);
```

### Parameters

- **name** (string): The name of the item
- **sellIn** (int): The number of days to sell the item
- **quality** (int): The quality value of the item

### Returns

An instance of the appropriate `Item` subtype:
- `Sulfaras` for "Sulfuras, Hand of Ragnaros"
- `AgedBrie` for "Aged Brie"
- `BackstagePass` for "Backstage passes to a TAFKAL80ETC concert"
- Base `Item` for all other items

## Examples

### Creating Sulfuras (Legendary Item)

```csharp
var sulfuras = Item.Create("Sulfuras, Hand of Ragnaros", 10, 50);
// Returns: Sulfaras instance with Name="Sulfuras, Hand of Ragnaros", SellIn=0, Quality=80
// Note: sellIn and quality parameters are ignored for Sulfuras
```

**Important:** Sulfuras always has `SellIn=0` and `Quality=80` regardless of the parameters passed.

### Creating Aged Brie

```csharp
var agedBrie = Item.Create("Aged Brie", 10, 20);
// Returns: AgedBrie instance with Name="Aged Brie", SellIn=10, Quality=20
```

### Creating Backstage Passes

```csharp
var backstagePass = Item.Create("Backstage passes to a TAFKAL80ETC concert", 15, 30);
// Returns: BackstagePass instance with the specified values
```

### Creating Normal Items

```csharp
var normalItem = Item.Create("+5 Dexterity Vest", 10, 20);
// Returns: Base Item instance with the specified values

var conjuredItem = Item.Create("Conjured Mana Cake", 3, 6);
// Returns: Base Item instance with the specified values
```

### Creating Multiple Items

```csharp
using GildedRoseKata;
using System.Collections.Generic;

IList<Item> items = new List<Item>
{
    Item.Create("+5 Dexterity Vest", 10, 20),
    Item.Create("Aged Brie", 2, 0),
    Item.Create("Elixir of the Mongoose", 5, 7),
    Item.Create("Sulfuras, Hand of Ragnaros", 0, 80),
    Item.Create("Backstage passes to a TAFKAL80ETC concert", 15, 20),
    Item.Create("Conjured Mana Cake", 3, 6)
};

var app = new GildedRose(items);
app.UpdateQuality();
```

## Benefits

1. **Type Safety**: Returns the correct subtype automatically
2. **Encapsulation**: Hides the complexity of subtype instantiation
3. **Maintainability**: Single point of modification if new item types are added
4. **Consistency**: Ensures items are created correctly (e.g., Sulfuras always has correct defaults)
5. **Testability**: Easy to mock and test item creation

## Comparison with Direct Instantiation

### Before (Direct Instantiation)

```csharp
// Inconsistent - different patterns for different types
var item1 = new Item { Name = "Normal Item", SellIn = 10, Quality = 10 };
var item2 = new AgedBrie(10, 20);
var item3 = new Sulfaras(); // Can't set custom values!
var item4 = new BackstagePass(15, 30);
```

### After (Factory Method)

```csharp
// Consistent - same pattern for all types
var item1 = Item.Create("Normal Item", 10, 10);
var item2 = Item.Create("Aged Brie", 10, 20);
var item3 = Item.Create("Sulfuras, Hand of Ragnaros", 0, 80);
var item4 = Item.Create("Backstage passes to a TAFKAL80ETC concert", 15, 30);
```

## Implementation Details

The factory method uses a switch expression to determine the correct type:

```csharp
public static Item Create(string name, int sellIn, int quality)
{
    return name switch
    {
        "Sulfuras, Hand of Ragnaros" => new Sulfaras(),
        "Aged Brie" => new AgedBrie(sellIn, quality),
        "Backstage passes to a TAFKAL80ETC concert" => new BackstagePass(sellIn, quality),
        _ => new Item { Name = name, SellIn = sellIn, Quality = quality }
    };
}
```

## Special Cases

### Sulfuras (Legendary Item)

Sulfuras is a special case:
- The `sellIn` and `quality` parameters are **ignored**
- Always created with `SellIn=0` and `Quality=80`
- This is enforced by the `Sulfaras` constructor

```csharp
var sulfuras = Item.Create("Sulfuras, Hand of Ragnaros", 999, 1);
// Still results in: SellIn=0, Quality=80
```

## Testing

See `ItemFactoryTests.cs` for comprehensive tests of the factory method, including:
- Correct subtype creation for each special item
- Normal item creation
- Integration with GildedRose update logic

## Future Enhancements

When adding new item types (e.g., Conjured items), simply:
1. Create the new subtype class
2. Add a case to the factory method switch expression
3. Add tests for the new type

Example for future Conjured items:

```csharp
public static Item Create(string name, int sellIn, int quality)
{
    return name switch
    {
        "Sulfuras, Hand of Ragnaros" => new Sulfaras(),
        "Aged Brie" => new AgedBrie(sellIn, quality),
        "Backstage passes to a TAFKAL80ETC concert" => new BackstagePass(sellIn, quality),
        _ when name.StartsWith("Conjured") => new ConjuredItem(name, sellIn, quality),
        _ => new Item { Name = name, SellIn = sellIn, Quality = quality }
    };
}
```
