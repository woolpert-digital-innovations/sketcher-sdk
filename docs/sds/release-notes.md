# Release Notes

## [2.0](https://schemas.opencamadata.org/2.0/data.schema.json) (2025-11-10)

### ⚠️ BREAKING CHANGES

* Rename sketch `customfields` -> `customFields`
* Change sketch `customfields` property type `object` -> `array`
* Rename `labelValue` property `floorfrom` -> `floorFrom`
* Rename `labelValue` property `floorto` -> `floorTo`
* Change `labelValue` property type `number` -> `string`
* Change `labelValue` property type `number` -> `string`

### ✨ Features

* Add optional `sections` to `sketch`
* Add optional `sectionId` to `labelValue`
* Add optional `propertyType` to `sketch`
* Add optional `propertyTypes` to `lookup`
* Add optional `attributes` to `styledLine` (command for lines)

## [0.10](https://schemas.opencamadata.org/0.10/data.schema.json) (2022-01-26)

* Add unique identifier to vector

## [0.9](https://schemas.opencamadata.org/0.9/data.schema.json) (2021-11-30)

* First release
