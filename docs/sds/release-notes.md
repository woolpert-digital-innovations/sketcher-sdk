# [2.0](https://schemas.opencamadata.org/2.0/data.schema.json)

## 2025-11-10

#### ⚠️ BREAKING CHANGES

- renamed sketch `customfields` -> `customFields`
- changed sketch `customfields` property type `object` -> `array`
- renamed `labelValue` property `floorfrom` -> `floorFrom`
- renamed `labelValue` property `floorto` -> `floorTo`
- changed `labelValue` property type `number` -> `string`
- changed `labelValue` property type `number` -> `string`

#### ✨ Features

- added optional `sections` to `sketch`
- added optional `sectionId` to `labelValue`
- added optional `propertyType` to `sketch`
- added optional `propertyTypes` to `lookup`
- added optional `attributes` to `styledLine` (command for lines)

# [0.10](https://schemas.opencamadata.org/0.10/data.schema.json)

## 2022-01-26

Add unique identifier to vector.

# [0.9](https://schemas.opencamadata.org/0.9/data.schema.json)

## 2021-11-30

First release.
