# Rusty.CutsceneImporter
A set of cutscene resource importer plugins for the Godot game engine.

It provides import plugins, serializers and deserializers for several resource types from the [cutscenes module](https://github.com/RustyRoboticsBV/Rusty.Cutscenes):
- Cutscene programs, which use a CSV file format.
- Instruction sets, which use a ZIP file format.
- Instruction definitions, which use an XML file format.

#### Parsing Notes
Color values are encoded as hexadecimal numbers, and may contain alpha. Specific color name strings are also accepted. See [here](/ColorTable.md) for a list.

If the parser cannot interpret a value, it uses the following default values:
- `false` for booleans.
- `0` for integers.
- `0.0` for floating-point numbers.
- `'0'` for characters.
- `""` for strings.
- `#000000FF` for colors.

## Cutscene Programs
Cutscene programs follow the CSV file format. Rows are separated by linebreaks and cells are separated by commas. Rows are allowed to have different number of cells.

If a cell contains a comma or double-quote, it must be enclosed with double-quotes. Within enclosed cells, the string `""` is interpreted as a single double-quote character. For example, the cell `"A,""B"""` is interpreted as `A,"B"`.

Furthermore, the following format constraints are in place:
- Every row represents exactly one instruction. This means that cells may not contain line-break character. Instead, line-breaks are represented with the string `\n`.
  - Consequently, the string `\n` is represented as `\\n`.
- The first cell of a row must contain the instruction's opcode. The cells that come after are interpreted as arguments.


So the structure of each program is as follows:

    opcode_1,arg_1,...,arg_n
    ...
    opcode_n,arg_1,...,arg_n

## Instruction Sets
Instruction sets are stored in ZIP files. The file should have the following internal structure:

    ROOT
     ├index.txt.
     ├<category_name_1>
     │ ├<opcode_1>
     │ │ ├def.xml
     │ │ └icon.png
     │ ├...
     │ └<opcode_n>
     │   └...
     ├...
     └<category_name_n>
       └...

So the path to a file is always is category/opcode/file. The serializer places instructions without a category in a folder called "undefined".

The `index.txt` file contains a list of folder paths, and is used for easy parsing, like so:

    category_1/opcode_1
    ...
    category_1/opcode_n
    ...
    category_n/opcode_1
    ...
    category_n/opcode_n

## Instruction Definitions
This section outlines the XML file structure for instruction definitions. Unless otherwise specified, all XML elements are optional.
See the [main module documentation](TODO) for an explanation about what each element means.


    <!-- Root element. Required. -->
    <definition>
     
     <!-- The opcode. Required element. Should be unique within the instruction set! -->
     <opcode>str</opcode>
     
     
     <!-- The parameters. Each parameter requires an id attribute. Each ID should be unique within this file! -->
     
     <!-- A boolean parameter. -->
     <bool id="str">
      <name>str</name>
      <desc>str</desc>
      <default>false</default>
     </bool>
     
     <!-- An integer parameter. -->
     <int id="str">
      <name>str</name>
      <desc>str</desc>
      <default>0</default>
     </int>

     <!-- An integer slider parameter. -->
     <islider id="str">
      <name>str</name>
      <desc>str</desc>
      <default>0</default>
      <min>0</min>
      <max>0</max>
     </islider>
     
     <!-- A floating-point parameter. -->
     <float id="str">
      <name>str</name>
      <desc>str</desc>
      <default>0.0</default>
     </float>

     <!-- A floating-point slider parameter. -->
     <fslider id="str">
      <name>str</name>
      <desc>str</desc>
      <default>0.0</default>
      <min>0.0</min>
      <max>0.0</max>
     </fslider>

     <!-- A character parameter. -->
     <char id="str">
      <name>str</name>
      <desc>str</desc>
      <default>c</default>
     </char>

     <!-- A single-line text parameter. -->
     <text id="str">
      <name>str</name>
      <desc>str</desc>
      <default>str</default>
     </text>

     <!-- A multi-line text parameter. -->
     <multiline id="str">
      <name>str</name>
      <desc>str</desc>
      <default>str</default>
     </multiline>

     <!-- A color parameter. -->
     <color id="str">
      <name>str</name>
      <desc>str</desc>
      <default>#FFFFFFFF</default>
     </color>

     <!-- An output parameter. -->
     <output id="str">
      <name>str</name>
      <desc>str</desc>
      <remove_default>true</remove_default>
      <use_argument_as_preview>str</use_argument_as_preview>
     </output>
     
     
     
     <!-- The implementation. See the documentation of the cutscened module for syntax details. -->
     <implementation>
      <members>str</members>
      <initialize>str</initialize>
      <execute>str</execute>
     </implementation>
     
     
     
     <!-- The editor node info. -->
     <editor_node>
      <priority>0</priority>
      <min_width>0</min_width>
      <min_height>0</min_height>
      <main_color>#FFFFFF</main_color>
      <text_color>#FFFFFF</text_color>
     </editor_node>
     
     
     
     <!-- The preview terms. The <hide_if> elements should contain one of the following values: never, prev_is_empty,
          next_is_empty, either_is_empty or both_are_empty. -->
     
     <!-- A text preview term. -->
     <text_term>
      <text>str</text>
      <hide_if>never</hide_if>
     </text_term>
     
     <!-- An argument preview term. The parameter value should match a parameter ID. -->
     <argument_term>
      <parameter>str</parameter>
      <hide_if>never</hide_if>
     </argument_term>
     
     <!-- A compile rule term. The rule value should match a pre-instruction or post-instruction rule ID. -->
     <rule_term>
      <rule>str</rule>
      <hide_if>never</hide_if>
     </rule_term>
     
     
     
     <!-- The compile rules. Each rule should contain an unique ID. Nested rules only need an ID that is unique within their
          parent rule. -->
     
     <!-- The pre-instruction block. May contain any number of compile rule elements. -->
     <pre>
      
      <!-- An option rule. -->
      <option id="str">
       <name>str</name>
       <desc>str</desc>
       <enabled>true</enabled>
       (nested rule)
      </option>
      
      <!-- A choice rule. -->
      <choice id="str">
       <name>str</name>
       <desc>str</desc>
       <selected>0</selected>
       (nested rule(s))
      </choice>
      
      <!-- A tuple rule. -->
      <tuple id="str">
       <name>str</name>
       <desc>str</desc>
       <separator>str</separator>
       (nested rule(s))
      </tuple>
      
      <!-- A list rule. -->
      <list id="str">
       <name>str</name>
       <desc>str</desc>
       <button_text>true</button_text>
       <separator>str</separator>
       (nested rule)
      </list>
      
      <!-- An instruction rule. -->
      <instruction id="str">
       <name>str</name>
       <desc>str</desc>
       <opcode>str</opcode>
      </instruction>
      
     </pre>
     
     <!-- The post-instruction block. The contents are identical to the pre-instruction block. -->
     <post>

      (rules)

     </post>
     
    </definition>
