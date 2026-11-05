# Release Notes

## 2.2 (2026-11-04)

### Feature: Sketch Markup
 - Note: This feature requires a minimum SDS version of 2.2

Markup
```js
capabilities.markups: {
  flag: `enabled`
  types: ['line', 'label']
}
```

## 2.1 (2026-07-15)

### Feature: Allow styling of dimension label on lookups through config
 - Note: This feature requires a minimum SDS version of 2.1

Visibility
`lookupCollection.*.*.*.styles.dimensionLabel.visibility: 'hidden' | 'shown'`

Placement
`lookupCollection.*.*.*.styles.dimensionLabel.placement: 'inside' | 'outside'`

Padding
`lookupCollection.*.*.*.styles.dimensionLabel.padding: number`

### Feature: Set the furthest acceptable sds version to roll back to 

- Note: If the initial version is higher than the specified version sdsSaveVersion is ignored

`sdsSaveVersion: '{major}.{minor}' | 'latest'`

## 2.0 (2025-12-04)

### Feature: SharedWalls

`capabilities.sharedWalls.flag: 'enabled' | undefined`

### Feature: CreateSketch

`capabilities.createSketch.flag: 'enabled' | undefined`

### Fix: MultipleLabels key `delimeter` updated to `delimiter`

`capabilities.multipleLabels.delimiter`

### Fix: Sketch CustomFields flag is now required to be enabled

`capabilities.customFields.enabled: 'enabled' | undefined`

### Fix: Sketch CustomFields `sketchCustomFields` updated to `items`

`capabilities.customFields.items`


## 1.0  (2021-11-30)

* First release