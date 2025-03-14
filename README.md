# Rusty.CutsceneImporter
A set of cutscene resource importer plugins for the Godot game engine.

It provides import plugins, serializers and deserializers for several resource types from the [cutscenes module](https://github.com/RustyRoboticsBV/Rusty.Cutscenes):
- Cutscene programs, which use a CSV file format.
- Instruction sets, which use a ZIP file format.
- Instruction definitions, which use an XML file format.

## Parsing Notes
Color values are encoded as hexadecimal numbers, and may contain alpha. Specific color name strings are also accepted. See ColorTable.md for a list.

If the parser cannot interpret a value, it uses the following default values:
- `false` for booleans.
- `0` for integers.
- `0.0` for floats.
- `'0'` for characters.
- `""` for strings.
- `#00000000` for colors.

## Instruction Definitions
This section outlines the XML file structure for instruction definitions. Unless otherwise specified, all XML elements are optional.
See the [main module documentation](TODO) for an explanation about what each element means.


    <!-- Root element. Required. -->
    <definition>
     
     <!-- The opcode. Required element.
          Should be unique within the instruction set! -->
     <opcode>str</opcode>
     
     
     <!-- The parameters. Each parameter requires an id attribute.
          Each ID should be unique within this file! -->
     
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
      <default>str\nwith\nline-breaks</default>
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
     
     
     
     <!-- The implementation.
          See the documentation of the cutscened module for syntax details. -->
     <implementation>
      <members>member_definition_code;</members>
      <initialize>initialize_code;</initialize>
      <execute>execute_code;</execute>
     </implementation>
     
     
     
     <!-- The editor node info. -->
     <editor_node>
      <priority>0</priority>
      <min_width>0</min_width>
      <min_height>0</min_height>
      <main_color>#FFFFFF</main_color>
      <text_color>#FFFFFF</text_color>
     </editor_node>
     
     
     
     <!-- The preview terms. he <hide_if> elements should contain one of the following
          values: never, prev_is_empty, next_is_empty, either_is_empty or both_are_empty. -->
     
     <!-- A text preview term. -->
     <text_term>
      <text>str</text>
      <hide_if>never</hide_if>
     </text_term>
     
     <!-- An argument preview term.
          The parameter value should match a parameter ID. -->
     <argument_term>
      <parameter>str</parameter>
      <hide_if>never</hide_if>
     </argument_term>
     
     <!-- A compile rule term.
          The rule value should match a pre-instruction or post-instruction rule ID. -->
     <rule_term>
      <rule>str</rule>
      <hide_if>never</hide_if>
     </rule_term>
     
     
     
     <!-- The compile rules. Each rule should contain an unique ID. Nested rules
          only need an unique ID within that rule. -->
     
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
     </post>
     
    </definition>