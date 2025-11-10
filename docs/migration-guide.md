# 0.9 -> 0.10

This update requires SDS 0.10 documents.  To migrate from SDS 0.9 to 0.10 documents, vectors must be assigned a unique identifier.

Below is an example migration.  It assumes you have a `document` that represents an SDS 0.9 document object.  The `documentConverted` is the result of converting the SDS 0.9 document object to an SDS 0.10 document object.

```js
let nextVectorId = 1;

const documentConverted = {
  ...document,
  $schema: document.$schema.replace('/0.9/data.schema.json', '/0.10/data.schema.json'),
  sketches: document.sketches.map(sketch => ({
    ...sketch,
    segments: sketch.segments.map(segment => ({
      ...segment,
      vectors: segment.vectors?.map(vector => ({
        ...vector,
        id: nextVectorId++,
      })),
    })),
  })),
};
```
