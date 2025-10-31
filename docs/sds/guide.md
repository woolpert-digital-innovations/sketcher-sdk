# Sketch Data Schema

## Purpose

Sketches are used in the computer-assisted mass appraisal (CAMA) industry to record information collected by a property appraiser so that a person or computer system can assess the market or taxable value of the property.

The purpose of the SDS is to encode such a sketch in both a human- and machine-readable plain text format known as **an SDS document**.

With the ability to use a standard format to exchange sketches, the hope is that sketch tool and CAMA vendors can spend more time innovating on differentiating features with user value, and less time on lossy sketch data conversion utilities that have little value to the appraisal and assessment industry.

### Typical uses of SDS documents

An SDS document can be transmitted between people or systems such that the receiver can use the data encoded in that document to perform a set of common activities.

1. **Visually show structures** in a sketch for viewing purposes.
  For example, someone with knowledge of the SDS can write a viewer that shows a picture of a structure along with color-coded lines and areas, and labels describing dimensions and areas.
   A typical example might be to render sketches in a web page.

1. **Edit and save a sketch.** Applications can use SDS documents as a way to save and reopen entire sketches with a set of common features.

1. **Transmit sketch data between systems.** Two systems that understand a specific version of SDS can exchange an SDS document without losing the information necessary to support the key assessment and appraisal activities, including:

   1. Graphical representation of the outline of structures.
   1. The area and perimeter of structures.
   1. Other attributes relevant to the assessment of value of structures, e.g., what the space is being used for by the owner or occupant.

### Non-goals of SDS

1. **Not differentiating sketching applications.**
  The SDS DOES NOT attempt to describe how a sketch is created.
  That is the purpose of CAMA sketching tools and applications available commercially.
  CAMA sketching tools do--and should--compete on usability, unique features, and commercial terms.

## Specification

### Schema documents

1. The SDS schema shall be described as a [JSON Schema 2020-12](https://json-schema.org/specification-links.html#2020-12) document.

1. Where there is disagreement between this specification document and the current JSON schema, the JSON schema shall take precedence.

1. [Release notes](./release-notes.md) MUST be published for each minor release.

1. The SDS schema ('schema') describes CAMA sketches and the data needed to transmit them without data loss.

   1. The schema is used to create Sketch Data Schema documents (SDS documents).

   1. The schema MUST contain at least enough information to draw the sketch as it was last seen by the end user.

## Sketch documents

1. Coordinate system.
   1. All coordinates MUST be defined in two dimensions on a Cartesian grid.
   1. The origin of the coordinate grid MUST be `[0,0]` with the x-axis value coming first and the y-axis value coming second.
   1. The X and Y values MUST be either a positive or a negative floating point number.

      ```js
      [
         // Valid
         {"x": 23, "y": -32.5},
         {"x": 0, "y": 2222222222}

         // Invalid
         { "x": null, "y": 2 },
         { "y": 2 },
         { "x": NaN, "y": -1}
      ]
      ```

1. Rings.
   In SDS a ring is a set of drawing commands that define a shape as a list of contiguous line or arc segments.  A ring is complete if its has at least two distinct points and its start point and end point are coincident.  Incomplete rings are rendered for display.

   1. Rings can be in any order.
   1. Rings can be in any direction.
   1. Rings cannot be self-intersecting.
   1. Rings must contain their inner rings.
   1. Sibling rings cannot overlap.
   1. Rings are complete if they contain more than two points; otherwise, the ring is considered incomplete.
   1. Rings may be complete and collinear i.e. the shape effectively creates a line.

### Pages

1. Pages are logically in the hierarchy _Sketch > **Page** > Segment_.
1. Segments optionally MAY exist on a page but MUST live in a Sketch.
1. Pages are defined by using the `page` property on `sketches[].segments[].page` and on `sketches[].notes[].page`.
1. Page count is determined by counting up the unique instances of `page` in `sketches[].segments[].page`.

### Styles

A style is a set of properties that define how a component should be rendered. Components inherit the style of their ancestor component(s). A style property may not apply to the component its directly associated with or at all if none of the descendant components use that particular property.

A component's effective style property value is determined from most to least specific style:

1. Style directly on component (inline)
1. Style associated with component (component style, e.g. a lookup's `labelStyle` associated with a label)
1. Effective style on parent

#### Fill and Fill Patterns

A style allows both `fill` and `fillPattern` to be defined; however, `fill` does not apply unless the effective value of `fillPattern` is not defined or `'none'`.

## Drawing Commands

### Arcs

An arc is a segment of a circle or ellipse.
The SDS `arc` drawing command supports both types of curve.

<svg width="320" height="320" xmlns="http://www.w3.org/2000/svg">
<defs>
    <marker id="arrowhead" markerWidth="10" markerHeight="7" 
    refX="0" refY="3.5" orient="auto">
      <polygon points="0 0, 10 3.5, 0 7" />
    </marker>
  </defs>
  <path d="M 10 315
           L 110 215
           A 30 50 -45 0 1 215.1 109.9
           L 315 10" stroke="black" stroke-width="2" fill-opacity="0.2" marker-end="url(#arrowhead)"/>
</svg>

The `direction` parameter of the arc command is interpreted with respect to the direction of drawing of the previous line vector.
For example in the diagram above, the line is being drawn from bottom left to top right (see the arrow).
The arc is therefore being drawn as a `clockwise` curve with respect to the line.
