# Release Notes

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

`capabilities.sharedWalls.flag: 'true' | 'false'`

### Feature: CreateSketch

`capabilities.createSketch.flag: 'true' | 'false'`

### Fix: MultipleLabels key `delimeter` updated to `delimiter`

`capabilities.multipleLabels.delimiter`

### Fix: Sketch CustomFields flag is now required

`capabilities.customFields.enabled: 'true' | 'false' `

### Fix: Sketch CustomFields `sketchCustomFields` updated to `items`

`capabilities.customFields.items`


## 1.0  (2021-11-30)

* First release