@tool
extends EditorImportPlugin;
class_name InstructionSetImportPlugin;

enum Preset { Default, NUM };

func _get_importer_name() -> String:
	return "rusty.cutscenes.instruction_set_importer";

func _get_visible_name() -> String:
	return "Cutscene Instruction Set";

func _get_recognized_extensions() -> PackedStringArray:
	return [ "zip", "iset" ];

func _get_save_extension() -> String:
	return "tres";

func _get_resource_type() -> String:
	return "Resource";

func _get_preset_count() -> int:
	return Preset.NUM;

func _get_preset_name(preset_index: int) -> String:
	match preset_index:
		Preset.Default:
			return "Default";
		_:
			return "???";

func _get_import_options(path: String, preset_index: int) -> Array[Dictionary]:
	return [];

func _get_option_visibility(path, option_name, options):
	return true;
	
func _get_import_order() -> int:
	return 100;
	
func _get_priority() -> float:
	return 2.0;

func _import(source_file: String, save_path: String, options: Dictionary, platform_variants: Array[String], gen_files: Array[String]) -> Error:
	
	# Get importer.
	var loader : InstructionSetImporter = InstructionSetImporter.new();
	
	# Load resource.
	var resource = loader.Import(source_file, options);
	if resource == null:
		printerr("Could not load ZIP instruction set at '" + source_file + "'!");
		return FAILED;
	
	# Save to file.
	return ResourceSaver.save(resource, save_path + "." + _get_save_extension());
