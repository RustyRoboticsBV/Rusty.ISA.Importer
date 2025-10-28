@tool
extends EditorImportPlugin;

const strip_name : String = "strip_editor_only_instructions";

func _get_importer_name() -> String:
	return "isa_program_importer";

func _get_visible_name() -> String:
	return "ISA Program Importer";

func _get_recognized_extensions() -> PackedStringArray:
	return ["csv"];

func _get_save_extension():
	return "tres";

func _get_resource_type():
	return "Resource";

func _get_preset_count():
	return 0;

func _get_preset_name(preset_index : int):
	return "";

# Optional import options
func _get_import_options(path : String, preset: int) -> Array:
	var strip_value : bool = true;
	
	var import_meta_path = path + ".import";
	if FileAccess.file_exists(import_meta_path):
		var cfg := ConfigFile.new()
		if cfg.load(import_meta_path) == OK:
			strip_value = cfg.get_value("params", strip_name, strip_value);
	
	return [
		{
			"name": strip_name,
			"default_value": strip_value,
			"property_hint": PROPERTY_HINT_NONE,
			"hint_string": "",
			"usage": PROPERTY_USAGE_DEFAULT
		}
	]

func _get_option_visibility(path : String, option_name : StringName, options : Dictionary):
	return true;

func _import(source_file: String, save_path: String, options: Dictionary, platform_variants: Array, gen_files: Array) -> int:
	var file : FileAccess = FileAccess.open(source_file, FileAccess.READ);
	var text : String = file.get_as_text();
	var program : Program = ProgramImporter.Import(source_file, text, options[strip_name]);
	
	var result = ResourceSaver.save(program, save_path + "." + _get_save_extension());
	if result != OK:
		push_error("Failed to save resource: %s" % save_path)
		return ERR_CANT_CREATE;

	return OK;
